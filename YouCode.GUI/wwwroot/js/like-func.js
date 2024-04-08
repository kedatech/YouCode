// Inicializar cuando el contenido esté cargado completamente
document.addEventListener('DOMContentLoaded', function () {
    initializePostsLikes();
});

function initializePostsLikes() {
    var userId = getUserId();
    //if (!userId) {
    //    alert('No se ha iniciado sesión');
    //    return;
    //}

    var postItems = document.querySelectorAll('.post-item-render');
    postItems.forEach(function (postItem) {
        setupPostItemLikes(postItem, userId);
    });
}

function getUserId() {
    var stringId = localStorage.getItem('YCuserId');
    return stringId ? parseInt(stringId) : null;
}

function setupPostItemLikes(postItem, userId) {
    var reactions = JSON.parse(postItem.querySelector('.reactions').value);
    var likeGreen = postItem.querySelector('#like-green');
    var likeGray = postItem.querySelector('#like-gray');

    var userReaction = reactions.find(function (reaction) { return reaction.idUser === userId; });
    if (userReaction) {
        likeGreen.classList.remove('hidden');
        likeGray.classList.add('hidden');
    } else {
        likeGreen.classList.add('hidden');
        likeGray.classList.remove('hidden');
    }

    var likeBtn = postItem.querySelector('.btn-like');
    likeBtn.onclick = function () { setLike(postItem, userId, reactions); };
}

let requestInProgress = false;
function setLike(postItem, userId, reactions) {
    if (requestInProgress) return;
    requestInProgress = true;
    var userReaction = reactions.find(function (reaction) { return reaction.idUser === userId; });
    var postId = postItem.getAttribute('id').split('-')[1]; // Obtener el ID del post desde el atributo 'id'

    if (userReaction) {
        removeLike(postItem, reactions, userReaction.id);
    } else {
        addLike(postItem, reactions, userId, postId);
    };
    requestInProgress = false;
}


function addLike(postItem, reactions, userId, postId) {
    var url = 'https://youcode.onrender.com/api/reaction/';
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ idUser: userId, idPost: postId })
    })
        .then(function (response) {
            if (!response.ok) throw new Error('Falló la petición');
            return response.json();
        })
        .then(function (data) {
            reactions.push({ id: data.id, idUser: userId, idPost: postId }); // Asumiendo que la respuesta incluye un 'id' para la reacción
            updateUIForLike(postItem, reactions);
        })
        .catch(function (error) {
            console.error('Error:', error);
        });
}

function removeLike(postItem, reactions, reactionId) {
    var url = `https://youcode.onrender.com/api/reaction/${reactionId}`;
    fetch(url, { method: 'DELETE' })
        .then(function (response) {
            if (!response.ok) throw new Error('Falló la petición');
            var index = reactions.findIndex(function (reaction) { return reaction.id === reactionId; });
            reactions.splice(index, 1);
            updateUIForLike(postItem, reactions);
        })
        .catch(function (error) {
            console.error('Error:', error);
        });
}

function updateUIForLike(postItem, reactions) {
    var likesCount = postItem.querySelector('.likes');
    likesCount.innerText = `${reactions.length} Likes`;
    var likeGreen = postItem.querySelector('#like-green');
    var likeGray = postItem.querySelector('#like-gray');

    var userId = getUserId();
    var userReaction = reactions.find(function (reaction) { return reaction.idUser == userId; });
    console.log(userReaction);
    likeGreen.classList.toggle('hidden', userReaction == undefined);
    likeGray.classList.toggle('hidden', userReaction !== undefined);
    postItem.querySelector('.reactions').value = JSON.stringify(reactions);
}