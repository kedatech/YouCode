using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class UserController : Controller
{
    UserBL userBL = new UserBL();
    ProfileBL profileBL = new ProfileBL();
    PostBL postBL = new PostBL();
    
    public async Task<IActionResult> Profile(int id)
    {
        var profile = await profileBL.GetByIdAsync(new Profile { Id = id });
        return View(profile);
    }

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
