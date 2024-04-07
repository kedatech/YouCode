// seleccionar todos los elementos con la clase btn-comment
// y asignarles el evento click

let btnComments = document.querySelectorAll('.btn-comment');
btnComments.forEach(function (btnComment) {
    btnComment.onclick = function () {

        let postItem = btnComment.closest('.post-item-render');
        // cambiar estilos al boton
        let btnCommentGreen = postItem.querySelector('#comment-green');
        let btnCommentGray = postItem.querySelector('#comment-gray');
        btnCommentGreen.classList.toggle('hidden');
        btnCommentGray.classList.toggle('hidden');
        
        let comments = postItem.querySelector('.comment-component');
        comments.classList.toggle('hidden');
    };
});