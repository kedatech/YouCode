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
        // Obtener todos los elementos con la clase .ButtonDeleteComment
        var deleteButtons = comments.querySelectorAll('.ButtonDeleteComment');

       

        deleteButtons.forEach(function (deleteButton) {
            let UserComment = deleteButton.querySelector('#UserCommentIdValue');
            validateOwner(UserComment.value, deleteButton);
        });
        // console.log(deleteButtons)

        comments.classList.toggle('hidden');
        
        
        
    };
});