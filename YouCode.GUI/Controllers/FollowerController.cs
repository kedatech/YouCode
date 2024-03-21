using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class FollowerController : Controller
{
    FollowerBL followerBL = new FollowerBL();
    
    public async Task<IActionResult> Index(Follower follower)
    {
        return View();
    }
    public IActionResult Create()
    {
        ViewBag.Error = "";
        return View();
    }
    public async Task<ActionResult> Details(int id)
    {
        var follower = await followerBL.GetByIdAsync(new Follower { Id = id});
        return View(follower);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var follower = await followerBL.GetByIdAsync(new Follower{Id =  id});
        return View(follower);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Follower follower)
    {
        try
        {
            int res = await followerBL.CreateAsync(follower);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View(follower);
        }
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Follower follower)
    {
        try
        {
            int res = await followerBL.UpdateAsync(follower);
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
        var res = followerBL.DeleteAsync(new Follower{Id = id});
        return RedirectToAction(nameof(Index));
    }
}
