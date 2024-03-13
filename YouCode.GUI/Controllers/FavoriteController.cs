using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class FavoriteController : Controller
{
    FavoriteBL favoriteBL = new FavoriteBL();
    public IActionResult Create()
    {
        ViewBag.Error = "";
        return View();
    }
    public async Task<ActionResult> Details(int id)
    {
        var favorite = await favoriteBL.GetByIdAsync(new Favorite { Id = id});
        return View(favorite);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var favorite = await favoriteBL.GetByIdAsync(new Favorite{Id =  id});
        return View(favorite);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Favorite favorite)
    {
        try
        {
            int res = await favoriteBL.CreateAsync(favorite);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View(favorite);
        }
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Favorite favorite)
    {
        try
        {
            int res = await favoriteBL.UpdateAsync(favorite);
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
        var res = favoriteBL.DeleteAsync(new Favorite{Id = id});
        return RedirectToAction(nameof(Index));
    }
}
