using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class CommentController : Controller
{
    CommentBL commentBL = new CommentBL();
    public IActionResult Create()
    {
        ViewBag.Error = "";
        return View();
    }

    public async Task<IActionResult> Replies(int id)
    {
        var comments = await commentBL.SearchAsync(new Comment { Id = id});
        return View(comments);
    }
    public async Task<ActionResult> Details(int id)
    {
        var comment = await commentBL.GetByIdAsync(new Comment { Id = id});
        return View(comment);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var comment = await commentBL.GetByIdAsync(new Comment{Id =  id});
        return View(comment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Comment comment)
    {
        try
        {
            int res = await commentBL.CreateAsync(comment);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View(comment);
        }
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Comment comment)
    {
        try
        {
            int res = await commentBL.UpdateAsync(comment);
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
        var res = commentBL.DeleteAsync(new Comment{Id = id});
        return RedirectToAction(nameof(Index));
    }
}
