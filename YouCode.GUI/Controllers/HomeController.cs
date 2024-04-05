using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Diagnostics;
using System.Security.Claims;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Models;
using YouCode.GUI.Services;
using YouCode.GUI.Services.Auth;
namespace YouCode.GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostService postService = new PostService();

        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ProfileBL profileBL = new ProfileBL();
        private readonly UserBL userBL = new UserBL();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;

        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //TODO: Mejorar esto y no hacer peticiones innecesarias
            var username = HttpContext.Session.GetString("UserName");
            // Console.WriteLine(string.IsNullOrEmpty(username));
            if(!string.IsNullOrEmpty(username))
            {
                var user = await userBL.GetByUsernameAsync(username);
                var profile = await profileBL.GetByIdAsync(new Profile{Id = user.Id});
                ViewBag.AvatarUrl = profile.AvatarUrl;
            }
            else
            {
                ViewBag.AvatarUrl = "";
            }
            
            var posts = await postService.GetAllAsync(null);
            var postsHtml = new List<dynamic>();

            foreach (var post in posts)
            {
                var contentHtml = Markdown.ToHtml(post.Content);
                postsHtml.Add(new { Id = post.Id, ContentHtml = contentHtml });
            }

            ViewBag.PostsHtml = postsHtml;
            return View(posts);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
