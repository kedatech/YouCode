using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Diagnostics;
using YouCode.GUI.Models;

namespace YouCode.GUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }

        // Register.cshtml
        public IActionResult Register(AuthModel auth)
        {
            ViewData["ClientId"] = _configuration["GithubClientId"];
            ViewData["RedirectUrl"] = _configuration["RedirectUri"];
            return View();
        }

        [HttpPost]
        public IActionResult PostRegister(AuthModel auth)
        {
            // error no implementation
            return View();
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
