using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class ProfileController : Controller
{
    UserBL userBL = new UserBL();
    PostBL postBL = new PostBL();
    
    public IActionResult Index()
    {
        return View(); //falta de ver como madres traerme la info del usuario actual
    }
    public IActionResult Create()
    {
        ViewBag.Error = "";
        return RedirectToAction("Index", "Profile");
    }
    public async Task<ActionResult> Details(int id)
    {
        var user = await userBL.GetByIdAsync(new User { Id = id});
        return RedirectToAction(nameof(Index), user);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var user = await userBL.GetByIdAsync(new User{Id =  id});
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        try
        {
            int res = await userBL.CreateAsync(user);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View(user);
        }
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
