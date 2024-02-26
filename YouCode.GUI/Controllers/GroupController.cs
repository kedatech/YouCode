using YouCode;
using Microsoft.AspNetCore.Mvc;
using YouCode.DAL;
using YouCode.BE;
using YouCode.BL;

namespace YouCode.GUI.Controllers;
public class GroupController : Controller
{
    GroupBL groupBL = new GroupBL();
    
    public async Task<IActionResult> Index(Group group)
    {
        if(group.Top_Aux == 0)
            group.Top_Aux = 10;
        else if(group.Top_Aux < 0)
            group.Top_Aux = 0;

        var groups = await groupBL.SearchAsync(group);
        ViewBag.Top = group.Top_Aux;
        return View(groups);
    }
    public IActionResult Create()
    {
        ViewBag.Error = "";
        return View();
    }
    public async Task<ActionResult> Details(int id)
    {
        var group = await groupBL.GetByIdAsync(new Group { Id = id});
        return View(group);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var group = await groupBL.GetByIdAsync(new Group{Id =  id});
        return View(group);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Group group)
    {
        try
        {
            int res = await groupBL.CreateAsync(group);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception e)
        {
            ViewBag.Error = e.Message;
            return View(group);
        }
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Group group)
    {
        try
        {
            int res = await groupBL.UpdateAsync(group);
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
        var res = groupBL.DeleteAsync(new Group{Id = id});
        return RedirectToAction(nameof(Index));
    }
}
