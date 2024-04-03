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
    CommentBL commentBL = new CommentBL();
    
    [ValidateAntiForgeryToken]
    [JwtAuthentication]
    [HttpPost]
    [Route("api/Post/CreatePost")]
    public async Task<IActionResult> CreatePost(Post post)
    {
        var res = await postBL.CreateAsync(post);

        return RedirectToAction("Index", "Home");
    }
    
}