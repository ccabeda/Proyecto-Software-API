using Microsoft.AspNetCore.Mvc;
using Proyecto_Software_Individual.API.ControllerHelper;
using Proyecto_Software_Individual.Aplication.DTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Proyecto_Software_Individual.API.Controllers
{
    [Route("api/Project")]
    [ApiController]
    [Tags("Project")]
    public class ProjectProposalController : ControllerBase
    {
        private readonly IServiceProjectProposalCreate _serviceCreate;
        private readonly IServiceProjectProposalUpdate _serviceUpdate;
        private readonly IServiceProjectProposalGetFiltered _serviceGetFiltered;
        private readonly IServiceProjectProposalGetById _serviceGetById;
        public ProjectProposalController(IServiceProjectProposalCreate service, IServiceProjectProposalUpdate serviceUpdate, IServiceProjectProposalGetFiltered serviceGetFiltered, IServiceProjectProposalGetById serviceGetById)
        {
            _serviceCreate = service;
            _serviceUpdate = serviceUpdate;
            _serviceGetFiltered = serviceGetFiltered;
            _serviceGetById = serviceGetById;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Created")]
        [ProducesResponseType(typeof(ProjectProposalCompleteGetDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProyectInvalidResponse), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear propuesta de proyecto")]
        public async Task<ActionResult> CreateProjectProposal([FromBody] ProjectProposalCreateDTO projectPropsal)
        {
            var result = await _serviceCreate.CreateProjectProposal(projectPropsal);
            return result != null
            ? CreatedAtAction(nameof(GetProjectProposalById), new { id = result.Id }, result) : ControllerResponse.FromStatus(HttpStatusCode.BadRequest, new ProyectInvalidResponse());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(ProjectProposalCompleteGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectNotExist), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Obtener una propuesta de proyecto por ID")]
        public async Task<ActionResult> GetProjectProposalById([FromRoute, SwaggerParameter("ID único del proyecto, Example =123e4567-e89b-12d3-a456-426614174000")] Guid id)
        {
            var result = await _serviceGetById.GetProjectProposalById(id);
            return result != null
            ? ControllerResponse.FromStatus(HttpStatusCode.OK, result) : ControllerResponse.FromStatus(HttpStatusCode.BadRequest, new ProjectNotExist());
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(List<ProjectProposalGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProjectNotFound), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Filtrar propuestas de proyecto")]
        public async Task<ActionResult> GetFilteredProposals([FromQuery] ProjectProposalFilterDTO projectProposal)
        {
            var result = await _serviceGetFiltered.GetProjectProposalFiltered(projectProposal);
            return result != null
            ? ControllerResponse.FromStatus(HttpStatusCode.OK, result) : ControllerResponse.FromStatus(HttpStatusCode.BadRequest, new ProjectNotFound());
        }

        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [ProducesResponseType(typeof(ProjectProposalCompleteGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProyectUpdateInvalidResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProjectNotExist), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectNotModification), StatusCodes.Status409Conflict)]
        [SwaggerOperation(Summary = "Actualiza un proyecto en estado observado")]
        public async Task<IActionResult> UpdateProject([FromRoute, SwaggerParameter("ID único del proyecto, Example =123e4567-e89b-12d3-a456-426614174000")] Guid id, [FromBody] ProjectProposalUpdateDTO projectProposal)
        {
            try
            {
                var result = await _serviceUpdate.UpdateProjectProposal(id, projectProposal);
                return result != null
                ? ControllerResponse.FromStatus(HttpStatusCode.OK, result) : ControllerResponse.FromStatus(HttpStatusCode.NotFound, new ProjectNotExist());
            }
            catch (InvalidOperationException)
            {
                return ControllerResponse.FromStatus(HttpStatusCode.Conflict, new ProjectNotModification());
            }
            catch (ArgumentException)
            {
                return ControllerResponse.FromStatus(HttpStatusCode.BadRequest, new ProyectUpdateInvalidResponse());
            }
        }
    }
}
