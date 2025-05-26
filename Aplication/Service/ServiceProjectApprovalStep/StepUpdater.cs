using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectApprovalStep
{
    public class StepUpdater //funciones auxiliares
    {
        private readonly IUnitOfWorkProjects _unitOfWork;
        private readonly ILogger<StepUpdater> _logger;

        public StepUpdater(IUnitOfWorkProjects unitOfWork, ILogger<StepUpdater> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        private enum ProjectStatus
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3,
            Observed = 4
        }
        private enum StepStatus
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3,
            Observed = 4
        }

        public async Task UpdateStep(ProjectApprovalStep step, ProjectApprovalStepDesitionDTO stepDTO)
        {
            step.Status = stepDTO.Status;
            step.ApproverUserId = stepDTO.User;
            step.DesicionDate = DateTime.Now;
            step.Observations = stepDTO.Observation ?? string.Empty;

            await _unitOfWork._repositoryProjectApprovalStepCommand.Update(step);
        }

        public async Task UpdateProjectStatus(Guid projectId, ProjectProposal project)
        {
            var steps = await _unitOfWork._repositoryProjectApprovalStepQuery.GetByProjectId(projectId);

            if (steps.Any(s => s.Status == (int)StepStatus.Rejected))
            {
                project.Status = (int)ProjectStatus.Rejected;
                _logger.LogInformation("Proyecto {ProjectId} marcado como rechazado por un paso rechazado.", projectId);
            }
            else if (steps.Any(s => s.Status == (int)StepStatus.Observed))
            {
                bool todosAprobadosOObservados = steps.All(s => s.Status == (int)StepStatus.Approved || s.Status == (int)StepStatus.Observed);
                project.Status = todosAprobadosOObservados ? (int)ProjectStatus.Pending : (int)ProjectStatus.Observed;
                _logger.LogInformation("Proyecto {ProjectId} marcado como observado por pasos en ese estado.", projectId);
            }
            else if (steps.All(s => s.Status == (int)StepStatus.Approved))
            {
                project.Status = (int)ProjectStatus.Approved;
                _logger.LogInformation("Proyecto {ProjectId} aprobado porque todos los pasos están aprobados.", projectId);
            }
            else
            {
                project.Status = (int)ProjectStatus.Pending;
                _logger.LogInformation("Proyecto {ProjectId} en estado pendiente.", projectId);
            }

            await _unitOfWork._repositoryProjectProposalCommand.Update(project);
            await _unitOfWork.Save();
        }
    }
}
