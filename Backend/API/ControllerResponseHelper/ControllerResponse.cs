using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Proyecto_Software_Individual.API.ControllerHelper
{
    public static class ControllerResponse
    {
        public static ActionResult FromStatus(HttpStatusCode statusCode, object responseBody)
        {
            return statusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(responseBody),
                HttpStatusCode.Conflict => new ConflictObjectResult(responseBody),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(responseBody),
                HttpStatusCode.NotFound => new NotFoundObjectResult(responseBody),
                HttpStatusCode.InternalServerError => new ObjectResult(responseBody) { StatusCode = 500 },
                _ => new ObjectResult(responseBody) { StatusCode = (int)statusCode }
            };
        }
    }
}
