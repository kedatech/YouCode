// Inicializar cuando el contenido esté cargado completamente
document.addEventListener('DOMContentLoaded', function () {
    initializePostsFavorites();
});

function initializePostsFavorites() {
    var userId = getUserId();
    //if (!userId) {
    //    alert('No se ha iniciado sesión');
    //    return;
    //}

    var postItems = document.querySelectorAll('.post-item-render');
    postItems.forEach(function (postItem) {
        setupPostItemFavorites(postItem, userId);
    });
}

function getUserId() {
    var stringId = localStorage.getItem('YCuserId');
    return stringId ? parseInt(stringId) : null;
}

function setupPostItemFavorites(postItem, userId) {
    var favorites = JSON.parse(postItem.querySelector('.favorites').value);
    var favoriteGreen = postItem.querySelector('[id^="favorite-green"]');
    var favoriteGray = postItem.querySelector('[id^="favorite-gray"]');

    var userFavorite = favorites.find(function (favorite) { return favorite.idUser === userId; });
    if (userFavorite) {
        favoriteGreen.classList.remove('hidden');
        favoriteGray.classList.add('hidden');
    } else {
        favoriteGreen.classList.add('hidden');
        favoriteGray.classList.remove('hidden');
    }

    var favoriteBtn = postItem.querySelector('.btn-favorite');
    favoriteBtn.onclick = function () { setFavorite(postItem, userId, favorites); };
}

function setFavorite(postItem, userId, favorites) {
    var userFavorite = favorites.find(function (favorite) { return favorite.idUser === userId; });
    var postId = postItem.getAttribute('id').split('-')[1];

    if (userFavorite) {
        removeFavorite(postItem, favorites, userFavorite.id);
    } else {
        addFavorite(postItem, favorites, userId, postId);
    }
}

function addFavorite(postItem, favorites, userId, postId) {
    var url = 'http://localhost:5096/api/favorite/';
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ IdUser: userId, IdPost: postId })
    })
    .then(function (response) {
        if (!response.ok) throw new Error('Falló la petición');
        return response.json();
    })
    .then(function (data) {
        favorites.push({ id: data.id, idUser: userId, idPost: postId });
        updateUIForFavorite(postItem, favorites);
    })
    .catch(function (error) {
        console.error('Error:', error);
    });
}

function removeFavorite(postItem, favorites, favoriteId) {
    var url = `http://localhost:5096/api/favorite/${favoriteId}`;
    fetch(url, { method: 'DELETE' })
    .then(function (response) {
        if (!response.ok) throw new Error('Falló la petición');
        var index = favorites.findIndex(function (favorite) { return favorite.id === favoriteId; });
        favorites.splice(index, 1);
        updateUIForFavorite(postItem, favorites);
    })
    .catch(function (error) {
        console.error('Error:', error);
    });
}

function updateUIForFavorite(postItem, favorites) {
    var savesCount = postItem.querySelector('.saves');
    savesCount.innerText = `${favorites.length} Guardados`;
    var favoriteGreen = postItem.querySelector('[id^="favorite-green"]');
    var favoriteGray = postItem.querySelector('[id^="favorite-gray"]');

    var userId = getUserId();
    var userFavorite = favorites.find(function (favorite) { return favorite.idUser === userId; });
    favoriteGreen.classList.toggle('hidden', !userFavorite);
    favoriteGray.classList.toggle('hidden', !!userFavorite);
    postItem.querySelector('.favorites').value = JSON.stringify(favorites);
}
