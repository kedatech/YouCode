using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore;

namespace YouCode.GUI.Services.Auth
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var encryptedToken = filterContext.HttpContext.Request.Cookies["_TojiBestoProta"];
            var token = AuthenticationService.DecryptToken(encryptedToken);

            // Console.WriteLine("Valor del token: "+token);
            if (!string.IsNullOrEmpty(token))
            {
                var userName = AuthenticationService.ValidateToken(token);
                // Console.WriteLine(userName);
                if (string.IsNullOrEmpty(userName))
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
               httpContext.Session.SetString("UserName", userName); 
            }
            else
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
