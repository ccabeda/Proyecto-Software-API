using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.API.ControllerHelper;
using Proyecto_Software_Individual.Aplication.DTOs.UserDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Tags("Information")]
    public class UserController : ControllerBase
    {
        private readonly IServiceUserGetAll _service;
        public UserController(IServiceUserGetAll service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listado de usuarios")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(List<UserGetDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await _service.GetAllUsers();
            return ControllerResponse.FromStatus(HttpStatusCode.OK, result ?? new List<UserGetDTO>());
        }
    }
}
