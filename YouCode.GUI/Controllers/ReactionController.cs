using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using YouCode.BL;
using YouCode.DAL;

namespace YouCode.GUI.Controllers
{ 
    public class ReactionController : Controller     
    {
        // creación de objetos de acceso a la capa BL
        ReactionBL reactionBL = new ReactionBL();
        UserBL userBL = new UserBL();
        PostBL postBL = new PostBL();
        CommentBL commentBL = new CommentBL();


        // Acción que muestra los detalles de una reacción existente
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reaction = await reactionBL.GetByIdAsync(id);
                if (reaction == null)
                {
                    return NotFound(); // Devuelve una respuesta 404 si no se encuentra la reacción
                }
                return View(reaction);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index)); // Redirige a la página de índice si hay un error
            }
        }

        // Acción que muestra el formulario para crear una nueva reacción
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Users = await userBL.GetAllAsync();
                ViewBag.Posts = await postBL.GetAllAsync();
                ViewBag.Comments = await commentBL.GetAllAsync();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index)); // Redirigir a la página de índice si hay un error
            }
        }

        // Acción que recibe los datos del formulario y los envía a la base de datos para crear una nueva reacción
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BE.Reaction reaction)
        {
            try
            {
                await reactionBL.CreateAsync(reaction);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                // Recargar la lista de usuarios, posts y comentarios en caso de error
                ViewBag.Users = await userBL.GetAllAsync();
                ViewBag.Posts = await postBL.GetAllAsync();
                ViewBag.Comments = await commentBL.GetAllAsync();
                return View(reaction);
            }
        }

        // Acción que muestra los datos de una reacción para confirmar su eliminación
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reaction = await reactionBL.GetByIdAsync(id);
                return View(reaction);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index)); // Redirigir a la página de índice si hay un error
            }
        }

        // Acción que recibe la confirmación para eliminar una reacción
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BE.Reaction reaction)
        {
            try
            {
                await reactionBL.DeleteAsync(reaction);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Delete), new { reactionObj = reaction }); // Redirigir a la página de detalles de la reacción en caso de error
            }
        }
    }
}
