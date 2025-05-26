using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.Aplication.DTOs.AreaDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/Area")]
    [ApiController]
    [Tags("Information")]
    public class AreaController : ControllerBase
    {
        private readonly IServiceAreaGetAll _service;
        public AreaController(IServiceAreaGetAll service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listado de Areas")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(List<AreaGetDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAreas()
        {
            var result = await _service.GetAllAreas();
            return ControllerHelper.FromStatus(HttpStatusCode.OK, result ?? new List<AreaGetDTO>());
        }
    }
}
