using System;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSample.Domain;
using BookingSample.WebApi.Models.AuthSrv;
using BookingSample.WebApi.Models.AuthSrv.Dto;

namespace BookingSample.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly ILogAdapter _logAdapter;

        public UserController(IAuthService authService, ILogAdapter logAdapter)
        {
            _authService = authService;
            _logAdapter = logAdapter;
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(LoginDto loginInfo)
        {
            try
            {
                var isValid = _authService.IsValidUser(loginInfo.Username, loginInfo.Password);
                return isValid
                    ? Ok(_authService.CreateTokenFor(loginInfo.Username, loginInfo.Password))
                    : NotAuthorizedResponse();
            }
            catch (Exception ex)
            {
                _logAdapter.Error(ex);
                return CreateErrorResponseFromException(ex);
            }
        }
    }
}