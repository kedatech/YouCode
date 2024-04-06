using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Services.Auth;
using YouCode.GUI.Services;
using Microsoft.AspNetCore.Http;

namespace YouCode.GUI.Controllers;
public class PostController : Controller
{
    PostBL postBL = new PostBL();
    UserBL userBL = new UserBL();
    ImageBL imageBL = new ImageBL();
    CommentBL commentBL = new CommentBL();
    
    // [ValidateAntiForgeryToken]
    [JwtAuthentication]
    [HttpPost]
    [Route("api/Post/CreatePost")]
    public async Task<IActionResult> CreatePost(PostFormData postFormData)
    {
        var user = await userBL.GetByUsernameAsync(HttpContext.Session.GetString("UserName"));
        if(user != null)
        {
            var postResponse = await postBL.GetCreateAsync(new Post{
                Title = postFormData.Title,
                Content = postFormData.Content,
                IdUser = user.Id});
            
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
        else{
            return BadRequest();
        }

        return Ok();
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