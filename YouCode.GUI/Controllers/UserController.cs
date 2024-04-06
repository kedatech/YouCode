    using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;
using Microsoft.AspNetCore.Authorization;
using YouCode.GUI.Services.Auth;
using YouCode.GUI.Services;
using YouCode.GUI.Models.DTOs;
using Markdig;

namespace YouCode.GUI.Controllers;
public class UserController : Controller
{
    private readonly UserBL userBL = new UserBL();
    private readonly ProfileBL profileBL = new ProfileBL();
    private readonly PostBL postBL = new PostBL();
    private readonly PostService postService = new PostService();
    
    [Route("User/Profile/{username}")]
    [JwtAuthentication]
    public async Task<IActionResult> Profile(string username)
    {
        var user = await userBL.GetByUsernameAsync(username);
            if(user != null)
            {
                var profile = await profileBL.GetByIdAsync(new Profile { Id = user.Id });
                var posts = await postService.GetAllAsync(user.Username);
                if(profile != null)
                {
                    var postsHtml = new List<dynamic>();
                    foreach (var post in posts)
                    {
                        var contentHtml = Markdown.ToHtml(post.Content);
                        postsHtml.Add(new { Id = post.Id, ContentHtml = contentHtml });
                    }
                    ViewBag.PostsHtml = postsHtml;
                    return View(new ProfileReturnDto(){
                        Profile = profile,
                        Posts = posts
                    });
                }
                else
                {
                    Console.WriteLine("Error, perfil no existe");
                    return RedirectToAction("Index", "Home"); // error
                }
            }
            else
            {
                Console.WriteLine("Error, user no existe");
                return RedirectToAction("Index", "Home"); // error
            }
        
    }
    [JwtAuthentication]
    public async Task<IActionResult> Feed()
    {
        var posts = await postService.GetAllAsync(null);
        return View(posts);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var user = await userBL.GetByIdAsync(new User{Id =  id});
        return View(user);
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(User user)
    {
        try
        {
            int res = await userBL.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View();
        }
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var res = userBL.DeleteAsync(new User{Id = id});
        return RedirectToAction(nameof(Index));
    }
}
