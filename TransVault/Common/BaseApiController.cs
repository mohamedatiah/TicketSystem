using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TransVault.Common
{
    public class BaseApiController : ControllerBase
    {
        protected IActionResult OKResponse<T>(T result, HttpStatusCode statusCode = HttpStatusCode.OK, string message = null) where T : class
        {
            return StatusCode((int)statusCode, new TransVaultResponse<T> { HttpStatusCode = statusCode, Result = result, Message = message });
        }

        protected IActionResult OKResponse(HttpStatusCode statusCode = HttpStatusCode.OK, string message = null)
        {
            return StatusCode((int)statusCode, new TransVaultResponse<object> { HttpStatusCode = statusCode, Message = message });
        }

        protected IActionResult ErrorResponse<T>(T result, HttpStatusCode statusCode, string message) where T : class
        {
            return StatusCode((int)statusCode, new TransVaultResponse<T> { HttpStatusCode = statusCode, Result = result, Message = message });
        }

        protected IActionResult ErrorResponse(HttpStatusCode statusCode, string message)
        {
            return StatusCode((int)statusCode, new TransVaultResponse<object> { HttpStatusCode = statusCode, Message = message });
        }
    }
}
