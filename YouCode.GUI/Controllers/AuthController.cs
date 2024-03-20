using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Octokit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YouCode.BE;
using YouCode.BL;
using YouCode.GUI.Models;

namespace YouCode.GUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private UserBL _userBL = new UserBL();
        private ProfileBL _profileBL = new ProfileBL();

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => View();

        public IActionResult Register()
        {
            ViewData["ClientId"] = _configuration["GithubClientId"];
            ViewData["RedirectUrl"] = _configuration["RedirectUri"];
            return View();
        }

        public IActionResult Redirect()
        {
            var clientId = _configuration["GithubClientId"];
            var redirectUrl = _configuration["RedirectUri"];
            var githubOAuthUrl = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUrl}&scope=user:email";
            return Redirect(githubOAuthUrl);
        }

        public async Task<IActionResult> Login(string code)
        {
            try
            {
                var accessToken = await GetAccessTokenAsync(code);
                var userGithub = await GetUserFromGitHubAsync(accessToken);
                var userDB = await _userBL.GetByUsernameAsync(new BE.User { Username = userGithub.Login });
                bool is_new_user = userDB == null;
                
                if (is_new_user)
                {
                    
                    userDB = new BE.User
                    {
                        Name = userGithub.Name,
                        Username = userGithub.Login,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _userBL.CreateAsync(userDB);
                }

                var profileDB = new Profile
                {
                    Id = userDB.Id,
                    Email = userGithub.Email ?? string.Empty,
                    AvatarUrl = userGithub.AvatarUrl ?? "https://picsum.photos/400",
                    Bio = userGithub.Bio ?? string.Empty,

                };

                // Si es un nuevo usuario rea el perfil
                if (is_new_user)
                {
                    await _profileBL.CreateAsync(profileDB);
                }

                return RedirectToAction("Profile", "User", new { id = profileDB.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login process.");
                return StatusCode(500);
            }
        }

        private async Task<string> GetAccessTokenAsync(string code)
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
            return values["access_token"];
        }

        private async Task<Octokit.User> GetUserFromGitHubAsync(string accessToken)
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("eliseoApp"));
            githubClient.Credentials = new Credentials(accessToken);
            return await githubClient.User.Current();
        }

        public IActionResult Error() => View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
