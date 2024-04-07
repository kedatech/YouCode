using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Services.Auth;

namespace YouCode.GUI.Controllers;
[Route("api/comment")] // Define la ruta base para el controlador API de comentarios
[ApiController] // Indica que este controlador es un controlador API
[JwtAuthentication] // Asegura que se aplique la autenticación JWT
public class CommentApiController : ControllerBase
{
    private readonly CommentBL _commentBL = new CommentBL(); // Inyecta la lógica de negocios de los comentarios

    // POST: api/Comment
    [HttpPost]
    public async Task<IActionResult> PostCreate([FromBody] Comment comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Devuelve 400 si el modelo no es válido
        }
        try
        {
            await _commentBL.CreateAsync(comment);
            return CreatedAtAction("Get", new { id = comment.Id }, comment); // Devuelve 201 como confirmación de que el comentario se creó
        }
        catch (Exception ex)
        {
            // Manejo de la excepción en caso de error
            return StatusCode(500, ex.Message); // Devuelve 500 en caso de error interno del servidor
        }
    }

    // GET: api/Comment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var comments = await _commentBL.GetAllAsync();
            return Ok(comments); // Devuelve 200 con la lista de comentarios
        }
        catch (Exception ex)
        {
            // Manejo de la excepción en caso de error
            return StatusCode(500, ex.Message); // Devuelve 500 en caso de error interno del servidor
        }
    }

    // DELETE: api/Comment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Comment commentQuery = new Comment { Id = id };
            var comment = await _commentBL.GetByIdAsync(commentQuery);
            if (comment == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el comentario
            }

            await _commentBL.DeleteAsync(comment);
            return NoContent(); // Devuelve 204 como confirmación de que el comentario se eliminó
        }
        catch (Exception ex)
        {
            // Manejo de la excepción en caso de error
            return StatusCode(500, ex.Message); // Devuelve 500 en caso de error interno del servidor
        }
    }
}

