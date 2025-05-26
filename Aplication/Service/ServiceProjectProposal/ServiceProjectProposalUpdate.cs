using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ServiceProjectProposalUpdate : IServiceProjectProposalUpdate
    {
        private readonly ILogger<ServiceProjectProposalCreate> _logger;
        private readonly MapProjectWithStep _mapProjectWithStep;
        private readonly ProjectUpdater _projectValidator;

        public ServiceProjectProposalUpdate(ILogger<ServiceProjectProposalCreate> logger, IUnitOfWorkCatalogs unitOfWorkCatalogs, ProjectUpdater projectValidator, MapProjectWithStep mapProjectWithStep)
        {
            _logger = logger;
            _projectValidator = projectValidator;
            _mapProjectWithStep = mapProjectWithStep;
        }

        public async Task<ProjectProposalCompleteGetDTO?> UpdateProjectProposal(Guid id, ProjectProposalUpdateDTO ProjectProposalDTO)
        {
            try
            {
                var project = await _projectValidator.GetProjectOrThrow(id);
                await _projectValidator.ValidateUpdateData(ProjectProposalDTO, id);
                await _projectValidator.SaveProject(ProjectProposalDTO, project!);
                var view = await _mapProjectWithStep.MapProjectWithSteps(project!);
                return view;
            }
            catch (Exception)
            {
                _logger.LogError("Error inesperado al actualizar proyecto {ProjectId}", id);
                throw;
            }
        }
    }
}
