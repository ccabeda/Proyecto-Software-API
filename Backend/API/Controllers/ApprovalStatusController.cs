using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.Aplication.DTOs.ApprovalStatusDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Proyecto_Software_Individual.API.ControllerHelper;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/ApprovalStatus")]
    [ApiController]
    [Tags("Information")]
    public class ApprovalStatusController : ControllerBase
    {
        private readonly IServiceApprovalStatusGetAll _service;
        public ApprovalStatusController(IServiceApprovalStatusGetAll service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listado de estados para una solicitud de proyecto y pasos de aprobación")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(List<ApprovalStatusGetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllApprovalStatus()
        {
            var result = await _service.GetAllApprovalStatuses();
            return ControllerResponse.FromStatus(HttpStatusCode.OK, result ?? new List<ApprovalStatusGetDTO>());
        }
    }
}
