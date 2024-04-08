using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Services.Auth;
using YouCode.GUI.Services;
using Microsoft.AspNetCore.Http;
using YouCode.GUI.Models.DTOs;

namespace YouCode.GUI.Controllers;
public class PostController : Controller
{
    private readonly PostBL postBL = new PostBL();
    private readonly UserBL userBL = new UserBL();
    private readonly ImageBL imageBL = new ImageBL();
    private readonly CommentBL commentBL = new CommentBL();
    private readonly ReactionBL reactionBL = new ReactionBL();
    
    // [ValidateAntiForgeryToken]
    [JwtAuthentication]
    [HttpPost]
    [Route("api/Post/CreatePost")]
    public async Task<IActionResult> CreatePost(PostFormData postFormData)
    {
        var user = await userBL.GetByUsernameAsync(HttpContext.Session.GetString("UserName"));
        if (user != null)
        {
            var postResponse = await postBL.GetCreateAsync(new Post
            {
                Title = postFormData.Title ?? "",
                Content = postFormData.Content ?? "",
                IdUser = user.Id
            });

            if (postFormData.Files != null && postFormData.Files.Count != 0)
            {
                var counter = 0;
                foreach (IFormFile foto in postFormData.Files)
                {
                    var path = await ImageService.SubirArchivo(foto.OpenReadStream(), user.Username + "POST_IMAGE_" + counter.ToString());
                    await imageBL.CreateAsync(new Image { Path = path ?? "https://via.placeholder.com/300x200.png?text=Imagen+no+disponible", IdPost = postResponse.Id });
                    counter++;
                }
            }
        }
        else
        {
            return BadRequest();
        }

        return Ok();
    }


    [JwtAuthentication]
    [HttpDelete("api/Post/DeletePost/{IdPost}")]
    public async Task<IActionResult> DeletePost(int IdPost)
    {
        try
        {
            await Task.WhenAll(
                imageBL.DeleteOnAllPostAsync(IdPost),
                reactionBL.DeleteOnAllPostAsync(IdPost),
                // commentBL.DeleteOnAllPostAsync(IdPost),
                postBL.DeleteAsync(new Post { Id = IdPost })
            );

            return Ok();
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción y devolver un error
            return StatusCode(500, $"Error al eliminar el post: {ex.Message}");
        }
    }



    [JwtAuthentication]
    [HttpPost("api/Post/UpdatePost")]
    public async Task<IActionResult> UpdatePost(PostFormData postFormData)
    {
        var userName = HttpContext.Session.GetString("UserName") ?? "";
        var user = await userBL.GetByUsernameAsync(userName);

        if (user == null)
        {
            return BadRequest();
        }

        var updatedPost = new Post
        {
            Id = postFormData.Id,
            Title = postFormData.Title ?? "",
            Content = postFormData.Content ?? "",
            IdUser = user.Id
        };

        var updatedPostResponse = await postBL.UpdateAsync(updatedPost);

        if (updatedPostResponse == null)
        {
            return BadRequest();
        }

        if (postFormData.Files != null && postFormData.Files.Count > 0)
        {
            await imageBL.DeleteOnAllPostAsync(updatedPostResponse.Id);
            
            var counter = 0;
            foreach (IFormFile foto in postFormData.Files)
            {
                var path = await ImageService.SubirArchivo(foto.OpenReadStream(), user.Username + "POST_IMAGE_" + counter.ToString());
                await imageBL.CreateAsync(new Image { Path = path ?? "https://via.placeholder.com/300x200.png?text=Imagen+no+disponible", IdPost = updatedPostResponse.Id });
                counter++;
            }
        }

        return Json(new { success = true});
    }

    public class PostFormData
    {
        public int Id {get; set; }
        public int IdUser {get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
    
}