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
            var username = AuthenticationService.getCurrentUsername(HttpContext);
            // Console.WriteLine(string.IsNullOrEmpty(username));
            if(!string.IsNullOrEmpty(username))
            {
                var user = await userBL.GetByUsernameAsync(username);
                var profile = await profileBL.GetByIdAsync(new Profile{Id = user.Id});
                ViewBag.AvatarUrl = profile.AvatarUrl;
                ViewBag.IsLoggued = true;
                ViewBag.IsLogguedString = "true";
            }
            else
            {
                ViewBag.AvatarUrl = "";
                ViewBag.IsLoggued = false;
                ViewBag.IsLogguedString = "false";
            }
            
            var posts = await postService.GetAllAsync(null);
            var users = await userBL.GetAllAsync();
            var postsHtml = new List<dynamic>();

            foreach (var post in posts)
            {
                var contentHtml = Markdown.ToHtml(post.Content);
                postsHtml.Add(new { Id = post.Id, ContentHtml = contentHtml });
            }

            List<BE.User> usersToFollow = new List<BE.User>();
            var count = 0;
            users.Shuffle();
            foreach(var u in users)
            {
                if(count == 3)
                {
                    break;
                }
                if(u.Username != username)
                {
                    usersToFollow.Add(u);
                    count ++;
                }
            }
            // en base a la lista de usuario, obtener el perfil de cada uno y devolver en otro viewbag
  
            foreach(var u in usersToFollow)
            {
                var profile = await profileBL.GetByIdAsync(new Profile{Id = u.Id});
                u.Profile = profile;
            }

            ViewBag.PostsHtml = postsHtml;
            ViewBag.UsersToFollow = usersToFollow;
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

    public static class Extensions
    {
        private static Random rng = new Random();

        // Método de extensión para desordenar una lista
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
