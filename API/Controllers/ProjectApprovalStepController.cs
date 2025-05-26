using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.Aplication.DTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Shared;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/Project")]
    [ApiController]
    [Tags("Project")]
    public class ProjectApprovalStepController : ControllerBase
    {
        private readonly IServiceProjectApprovalStepUpdate _service;
        public ProjectApprovalStepController(IServiceProjectApprovalStepUpdate service)
        {
            _service = service;
        }


        [HttpPut("{id}/decision")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(ProjectProposalCompleteGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InvalidDates), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectNotExist), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectNotModification), StatusCodes.Status409Conflict)]
        [SwaggerOperation(Summary = "Actualizar estado de un paso de un proyecto")]
        public async Task<ActionResult> MakeDecisionStep([FromRoute, SwaggerParameter("ID único del proyecto, Example =123e4567-e89b-12d3-a456-426614174000")] Guid id, [FromBody] ProjectApprovalStepDesitionDTO projectApprovalStepDto)
        {
            try
            {
                var result = await _service.UpdateSteps(id, projectApprovalStepDto);
                return ControllerHelper.FromStatus(HttpStatusCode.OK, result);
            }
            catch (KeyNotFoundException)
            {
                return ControllerHelper.FromStatus(HttpStatusCode.NotFound, new ProjectNotExist());
            }
            catch (InvalidOperationException)
            {
                return ControllerHelper.FromStatus(HttpStatusCode.Conflict, new ProjectNotModification());
            }
            catch (ArgumentException)
            {
                return ControllerHelper.FromStatus(HttpStatusCode.BadRequest, new InvalidDates());
            }
        }
    }
}
