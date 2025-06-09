using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IService;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;


namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectApprovalStep
{
    public class ServiceProjectApprovalStepUpdate : IServiceProjectApprovalStepUpdate
    {
        
        private readonly StepUpdater _stepUpdater;
        private readonly StepValidator _stepValidator;
        private readonly IUnitOfWorkProjectProposalQuery _unitOfWork;
        private readonly ILogger<ServiceProjectApprovalStepUpdate> _logger;

        public ServiceProjectApprovalStepUpdate(StepValidator stepValidator, StepUpdater stepUpdater, ILogger<ServiceProjectApprovalStepUpdate> logger, IUnitOfWorkProjectProposalQuery unitOfWork)
        {
            _stepValidator = stepValidator;
            _stepUpdater = stepUpdater;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectProposalCompleteGetDTO> UpdateSteps(Guid projectId, ProjectApprovalStepDesitionDTO stepDTO)
        {
            try
            {
                var project = await _stepValidator.GetProjectByIdOrThrow(projectId);
                var step = await _stepValidator.GetStepByIdAndValidate(projectId, stepDTO.Id);
                var user = await _stepValidator.GetUserAndValidate(stepDTO.User, step);
                await _stepValidator.ValidateStepOrderRules(projectId, step);
                await _stepUpdater.UpdateStep(step, stepDTO);
                await _stepUpdater.UpdateProjectStatus(projectId, project);
                var updatedProject = await _unitOfWork._repositoryProjectProposalQuery.GetById(projectId);
                var view = await _stepValidator.MapProjectWithSteps(updatedProject!);
                return view;
            }
            catch (Exception)
            {
                _logger.LogError("Error inesperado para proyecto {ProjectId}", projectId);
                throw;
            }
        }
    }
}