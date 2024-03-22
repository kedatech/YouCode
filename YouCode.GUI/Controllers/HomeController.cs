using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Diagnostics;
using System.Security.Claims;
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
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            Console.WriteLine("token: "+token);
            return View();
        }

        public string Protected(ClaimsPrincipal user)
        {
            return user.Identity?.Name;
        }

        
        public async Task<IActionResult> Account(string code)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _configuration["GithubClientId"] ?? "none"},
                { "client_secret", _configuration["GithubClientSecret"] ?? "none"},
                { "code", code },
                { "redirect_uri", _configuration["RedirectUri"] ?? "none" }
            };

            var content = new FormUrlEncodedContent(parameters);
            var response = await httpClient.PostAsync("https://github.com/login/oauth/access_token", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var values = System.Web.HttpUtility.ParseQueryString(responseContent);
            var accessToken = values["access_token"];

            var githubClient = new GitHubClient(new ProductHeaderValue("eliseoApp"));
            githubClient.Credentials = new Credentials(accessToken);
            var user = await githubClient.User.Current();

            return View(user);
        }

        public IActionResult Privacy(string u)
        {
            return View(u);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
