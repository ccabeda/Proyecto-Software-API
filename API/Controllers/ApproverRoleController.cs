using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.Aplication.DTOs.ApproverRoleDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/Role")]
    [ApiController]
    [Tags("Information")]
    public class ApproverRoleController : ControllerBase
    {
        private readonly IserviceApproverRoleGetAll _service;
        public ApproverRoleController(IserviceApproverRoleGetAll service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listado de roles de usuarios")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(List<ApproverRoleGetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllApproverRoles()
        {
            var result = await _service.GetAllApproverRoles();
            return ControllerHelper.FromStatus(HttpStatusCode.OK, result ?? new List<ApproverRoleGetDTO>());
        }
    }
}
