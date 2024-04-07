using YouCode.BE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Octokit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YouCode.BL;
using YouCode.GUI.Models;
using YouCode.GUI.Services.Auth;

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

        /// <summary>
        /// Valida si el usuario 'username' es el propietario logueado en la Sesión
        /// </summary>
        /// <param name="username"></param>
        /// <returns>IActionResult</returns>
        [JwtAuthentication]
        [HttpGet]
        public IActionResult ValidateUserOwner(string username)
        {
            var currentUsernameResult = GetCurrentUsername();
            return Json(new { is_owner = username == currentUsernameResult });
        }

        [JwtAuthentication]
        [HttpGet]
        public string GetCurrentUsername()
        {
            string currentUsername = "";
            var owner_username = HttpContext.Session.GetString("UserName");
            if(!string.IsNullOrEmpty(owner_username))
            {
                var encryptedToken = HttpContext.Request.Cookies["_TojiBestoProta"];
                var token = AuthenticationService.DecryptToken(encryptedToken);

                if(!string.IsNullOrEmpty(token))
                {
                    var userName = AuthenticationService.ValidateToken(token);
                    if(!string.IsNullOrEmpty(userName))
                    {
                        currentUsername = userName;
                    }
                }
            }

            return currentUsername;
        }

        /// <summary>
        /// Valida si el usuario está logueado. Retorna el username de la sesión.
        /// </summary>
        /// <returns>Json response</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Auth/ValidateLoggedUser")]
        public IActionResult ValidateLoggedUser()
        {
            var _current_username = AuthenticationService.ValidateUserLogged(HttpContext);
            ViewBag.Username = _current_username;

            return Json(new { currentUsername = _current_username });
        }
        [AllowAnonymous]
        public IActionResult Redirect()
        {
            var clientId = _configuration["GithubClientId"];
            var redirectUrl = _configuration["RedirectUri"];
            var githubOAuthUrl = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUrl}&scope=user:email";
            return Redirect(githubOAuthUrl);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string code)
        {
            try
            {
                var accessToken = await GetAccessTokenAsync(code);
                var userGithub = await GetUserFromGitHubAsync(accessToken);
                var userDB = await _userBL.GetByUsernameAsync(userGithub.Login);
                bool is_new_user = userDB == null;
                
                if (is_new_user)
                {
                    
                    userDB = new BE.User
                    {
                        Name = userGithub.Name == null ? userGithub.Login : userGithub.Name,
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
                
                // Si es un nuevo usuario crea el perfil

                if (is_new_user)
                {
                    await _profileBL.CreateAsync(profileDB);
                }

                //----🚧 Create Jwt section

                var jwtToken = AuthenticationService.GenerateJwtToken(userDB.Username);
                var validUserName = AuthenticationService.ValidateToken(jwtToken);

                if (string.IsNullOrEmpty(validUserName))
                {
                    return RedirectToAction("Privacy", "Home"); //🚨 Debe retornar debido a un error
                }
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.Now.AddMonths(1)
                };
                var encryptedToken = AuthenticationService.EncryptToken(jwtToken);
                Response.Cookies.Append("_TojiBestoProta", encryptedToken, cookieOptions);

                //----🚧 Create Jwt section
                return RedirectToAction("Profile", "User", new { username = userDB.Username });
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
