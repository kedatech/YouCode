using Microsoft.AspNetCore.Mvc;
using YouCode.BL;
using YouCode.BE;
using System.Threading.Tasks;

namespace YouCode.GUI.Controllers
{
    [Route("api/favorite")] // Define la ruta base para el controlador API
    [ApiController] // Indica que este controlador es un controlador API
    public class FavoriteApiController : ControllerBase
    {
        private readonly FavoriteBL _favoriteBL = new FavoriteBL();

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Favorite favorite)
        {
            try
            {
                int res = await _favoriteBL.CreateAsync(favorite);
                return CreatedAtAction("Get", new { id = favorite.Id }, favorite); 
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); // Devuelve 500 en caso de error interno del servidor
            }
        }

        // DELETE: api/Favorite/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _favoriteBL.DeleteAsync(new Favorite { Id = id });
                return NoContent(); // Devuelve 204 como confirmación de que el favorito se eliminó
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); // Devuelve 500 en caso de error interno del servidor
            }
        }
    }
}
