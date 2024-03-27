using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Diagnostics;
using System.Security.Claims;
using YouCode.BL;
using YouCode.GUI.Models;

namespace YouCode.GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;

        }
        [AllowAnonymous]
        public IActionResult Index()
        {
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
