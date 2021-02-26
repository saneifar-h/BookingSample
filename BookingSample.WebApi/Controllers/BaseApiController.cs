using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingSample.WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult CreateErrorResponseFromException(Exception ex)
        {
            var error = new HttpError(ex, true);
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, error));
        }

        protected IHttpActionResult NotAuthorizedResponse()
        {
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                new UnauthorizedAccessException()));
        }
    }
}