using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;
using Microsoft.AspNetCore.Authorization;
using YouCode.GUI.Services.Auth;

namespace YouCode.GUI.Controllers;
public class UserController : Controller
{
    UserBL userBL = new UserBL();
    ProfileBL profileBL = new ProfileBL();
    PostBL postBL = new PostBL();
    
    [Route("User/Profile/{username}")]
    [JwtAuthentication]
    public async Task<IActionResult> Profile(string username)
    {
        var user = await userBL.GetByUsernameAsync(username);
            if(user != null)
            {
                var profile = await profileBL.GetByIdAsync(new Profile { Id = user.Id });
                if(profile != null)
                {
                    return View(profile);
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
        var posts = await postBL.GetAllAsync();
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
