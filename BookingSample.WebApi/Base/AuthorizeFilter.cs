using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using BookingSample.WebApi.Models.AuthSrv;

namespace BookingSample.WebApi.Base
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly AuthService _authService = new AuthService();


        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (Authorize(filterContext))
                return;
            HandleUnauthorizedRequest(filterContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            try
            {
                var encodedString = actionContext.Request.Headers.GetValues("Authorization").First();
                var principal = _authService.GetPrincipal(encodedString);
                var userId = principal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
                var userName = principal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value ?? "";
                if (userId != "100" || userName != "TestUser")
                    return false;
                Thread.CurrentPrincipal = principal;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}