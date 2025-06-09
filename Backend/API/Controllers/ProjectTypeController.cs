using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.API.ControllerHelper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectTypeDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/ProjectType")]
    [ApiController]
    [Tags("Information")]
    public class ProjectTypeController : ControllerBase
    {
        private readonly IServiceProjectTypeGetAll _service;
        public ProjectTypeController(IServiceProjectTypeGetAll service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listado de tipos de proyecto")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(List<ProjectTypeGetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjectTypes()
        {
            var result = await _service.GetAllProjectTypes();
            return ControllerResponse.FromStatus(HttpStatusCode.OK, result ?? new List<ProjectTypeGetDTO>());
        }
    }
}
