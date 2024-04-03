using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Services.Auth;

namespace YouCode.GUI.Controllers;
public class PostController : Controller
{
    PostBL postBL = new PostBL();
    UserBL userBL = new UserBL();
    CommentBL commentBL = new CommentBL();
    
    // [ValidateAntiForgeryToken]
    [JwtAuthentication]
    [HttpPost]
    [Route("api/Post/CreatePost")]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        var user = await userBL.GetByUsernameAsync(HttpContext.Session.GetString("UserName"));
        if(user != null)
        {
            post.IdUser = user.Id ;
            var res = await postBL.CreateAsync(post);
        }
        else{
            return BadRequest();
        }
        

        return Ok();
    }
    
}