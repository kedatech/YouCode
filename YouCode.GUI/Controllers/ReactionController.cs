using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using YouCode.BL;
using YouCode.DAL;
using System.Threading.Tasks;
using YouCode.GUI.Services.Auth;

namespace YouCode.GUI.Controllers
{ 
    [Route("api/reaction")] // Define la ruta base para el controlador API
    [ApiController] // Indica que este controlador es un controlador API
    [JwtAuthentication]
    public class ReactionApiController : ControllerBase     
    {
        private readonly ReactionBL _reactionBL = new ReactionBL();

        // POST: api/ReactionApi
        [HttpPost]
        public async Task<IActionResult> PostCreate([FromBody] BE.Reaction reaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
            }
            try
            {
                await _reactionBL.CreateAsync(reaction);
                return CreatedAtAction("Get", new { id = reaction.Id }, reaction); // Devuelve 201 como confirmación de que la reacción se creó
            }
            catch (Exception ex)
            {
                // Manejo de la excepción en caso de error
                return StatusCode(500, ex.Message); // Devuelve 500 en caso de error interno del servidor
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reactions = await _reactionBL.GetAllAsync();
                return Ok(reactions); // Devuelve 200 con la lista de reacciones
            }
            catch (Exception ex)
            {
                // Manejo de la excepción en caso de error
                return StatusCode(500, ex.Message); // Devuelve 500 en caso de error interno del servidor
            }
        }

        // DELETE: api/ReactionApi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                BE.Reaction reactionQuery = new BE.Reaction { Id = id };
                var reaction = await _reactionBL.GetByIdAsync(reactionQuery);
                if (reaction == null)
                {
                    return NotFound(); // Devuelve 404 si no se encuentra la reacción
                }

                await _reactionBL.DeleteAsync(reaction);
                return NoContent(); // Devuelve 204 como confirmación de que la reacción se eliminó
            }
            catch (Exception ex)
            {
                // Manejo de la excepción en caso de error
                return StatusCode(500, ex.Message); // Devuelve 500 en caso de error interno del servidor
            }
        }
    }
}
