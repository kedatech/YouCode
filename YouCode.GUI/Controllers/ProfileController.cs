using Microsoft.AspNetCore.Mvc;
using YouCode.GUI.Services.Auth;
using static YouCode.GUI.Services.ProfileService;


namespace YouCode.GUI.Controllers
{
    public class ProfileController : Controller
    {
        [HttpPost]
        [JwtAuthentication]
        [Route("Profile/EditProfile")]
        public async Task<IActionResult> EditProfile(ProfileDto profileDto)
        {
            var sesion_username = HttpContext.Session.GetString("UserName");
            var res = await UpdateProfile(profileDto, sesion_username);
            Console.WriteLine("Result on controller:");
            Console.WriteLine(res);
            if(res)
            {
                return Json(new { success = true, redirectTo = Url.Action("Profile", "User", new{username = sesion_username}) });
            }
            else
            {
                return Json(new { success = false, redirectTo = Url.Action("Index", "Home") });  
            }          
        }

        [HttpGet]
        [JwtAuthentication]
        [Route("Profile/GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var username = HttpContext.Session.GetString("UserName");
            if(string.IsNullOrEmpty(username))
            {
                return Json(new { success = false});
            }
            var res_dto = await GetProfileDto(username);
            
            if(res_dto != null)
            {
                return Json(new { success = true, profileDto = res_dto});
            }
            else
            {
                return Json(new { success = false, profileDto = res_dto});  
            }          
        }
    }
}
