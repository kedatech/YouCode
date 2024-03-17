using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using YouCode.BL;

namespace YouCode.GUI.Controllers
{
    public class ReactionController : Controller
    {
        // creación de objetos de acceso a la capa BL
        ReactionBL reactionBL = new ReactionBL();
        UserBL userBL = new UserBL();
        PostBL postBL = new PostBL();
        CommentBL commentBL = new CommentBL();

        // Acción que muestra la lista de reacciones
        public async Task<IActionResult> Index()
        {
            var reactions = await reactionBL.GetAllAsync();
            return View(reactions);
        }

        // Acción que muestra los detalles de una reacción
        public async Task<IActionResult> Details(int id)
        {
            var reaction = await reactionBL.GetByIdAsync(new Reaction { Id = id });
            reaction.User = await UserBL.GetByIdAsync(new User { Id = reaction.IdUser });
           reaction.Post = await PostBL.SearchAsync(new BE.Post  { IdReaction = reaction.Id });
            reaction.comment = await commentBL.SearchAsync(new BE.Comment { IdComment = reaction.Id });
            return View(reaction);
           
        }

        // Acción que muestra el formulario para crear una nueva reacción
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await userBL.GetAllAsync();
            ViewBag.Posts = await postBL.GetAllAsync();
            ViewBag.Comments = await commentBL.GetAllAsync();
            return View();
        }

        // Acción que recibe los datos del formulario y los envía a la base de datos para crear una nueva reacción
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reaction reaction)
        {
            try
            {
                await reactionBL.CreateAsync(reaction);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(reaction);
            }
        }


        // Acción que muestra el formulario para editar una reacción existente
        public async Task<IActionResult> Edit(int id)
        {
            var reaction = await reactionBL.GetByIdAsync(id);
            ViewBag.Users = await userBL.GetAllAsync();
            ViewBag.Posts = await postBL.GetAllAsync();
            ViewBag.Comments = await commentBL.GetAllAsync();
            return View(reaction);
        }

        // Acción que recibe los datos del formulario y los envía a la base de datos para editar una reacción existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reaction reaction)
        {
            try
            {
                await reactionBL.UpdateAsync(reaction);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(reaction);
            }
        }

        // Acción que muestra los datos de una reacción para confirmar su eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var reaction = await reactionBL.GetByIdAsync(id);
            return View(reaction);
        }

        // Acción que recibe la confirmación para eliminar una reacción
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await reactionBL.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var reaction = await reactionBL.GetByIdAsync(id);
                return View(reaction);
            }
        }
    }
}
