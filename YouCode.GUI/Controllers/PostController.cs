using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class PostController : Controller
{
    PostBL postBL = new PostBL();
    CommentBL commentBL = new CommentBL();
    
    public IActionResult Create()
    {
        ViewBag.Error = "";
        return View();
    }

    public async Task<IActionResult> Comments(int idPost)
    {
        var comments = await commentBL.SearchAsync(new Comment{IdPost = idPost});
        return View(comments);
    }
    public async Task<ActionResult> Details(int id)
    {
        var post = await postBL.GetByIdAsync(new Post { Id = id});
        return View(post);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var post = await postBL.GetByIdAsync(new Post{Id =  id});
        return View(post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        try
        {
            int res = await postBL.CreateAsync(post);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View(post);
        }
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Post post)
    {
        try
        {
            int res = await postBL.UpdateAsync(post);
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
        var res = postBL.DeleteAsync(new Post{Id = id});
        return RedirectToAction(nameof(Index));
    }
}
