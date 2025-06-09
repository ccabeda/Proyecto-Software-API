using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectApprovalStep
{
    public class StepValidator //funciones auxiliares
    {
        private readonly IUnitOfWorkProjectProposalQuery _unitOfWorkProjectProposalQuery;
        private readonly IUnitOfWorkProjectApprovalStepQuery _unitOfWorkProjectApprovalStepQuery;
        private readonly IUnitOfWorkUserQuery _unitOfWorkUserQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<StepValidator> _logger;

        public StepValidator(IUnitOfWorkProjectProposalQuery unitOfWorkProjectProposalQuery, IUnitOfWorkUserQuery unitOfWorkUserQuery, IMapper mapper, IUnitOfWorkProjectApprovalStepQuery unitOfWorkProjectApprovalStepQuery, ILogger<StepValidator> logger)
        {
            _unitOfWorkProjectProposalQuery = unitOfWorkProjectProposalQuery;
            _unitOfWorkUserQuery = unitOfWorkUserQuery;
            _mapper = mapper;
            _unitOfWorkProjectApprovalStepQuery = unitOfWorkProjectApprovalStepQuery;
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

        public async Task<ProjectProposal> GetProjectByIdOrThrow(Guid projectId)
        {
            var project = await _unitOfWorkProjectProposalQuery._repositoryProjectProposalQuery.GetById(projectId);
            if (project == null)
                throw new KeyNotFoundException("Proyecto no encontrado");
            return project;
        }

        public async Task<ProjectApprovalStep> GetStepByIdAndValidate(Guid projectId, long stepId)
        {
            var step = await _unitOfWorkProjectApprovalStepQuery._repositoryProjectApprovalStepQuery.GetById(stepId);
            if (step == null || step.ProjectProposalId != projectId)
                throw new KeyNotFoundException("Paso no encontrado o no corresponde al proyecto.");
            return step;
        }

        public async Task<User> GetUserAndValidate(int userId, ProjectApprovalStep step)
        {
            var user = await _unitOfWorkUserQuery._repositoryUserQuery.GetById(userId);
            if (user == null)
            {
                _logger.LogInformation("Datos de decisión inválidos");
            }
            if (user.Role != step.ApproverRoleId)
            {
                _logger.LogInformation("Usuario intentó aprobar el paso sin tener el rol apropiado ");
            }
            if (step.Status == (int)StepStatus.Approved || step.Status == (int)StepStatus.Rejected)
            {
                _logger.LogInformation("No se puede modificar un paso que ya fue aprobado o rechazado.");
            }
            return user;
        }

        public async Task ValidateStepOrderRules(Guid projectId, ProjectApprovalStep step)
        {
            var steps = await _unitOfWorkProjectApprovalStepQuery._repositoryProjectApprovalStepQuery.GetByProjectId(projectId);

            if (steps.Any(s => s.StepOrder < step.StepOrder && (s.Status == (int)StepStatus.Rejected || s.Status == (int)StepStatus.Observed)))
            {
                _logger.LogInformation("No se puede modificar el paso porque ya hay pasos previos rechazados u observados.");
                throw new InvalidOperationException();
            }
            if (steps.Any(s => s.StepOrder < step.StepOrder && s.Status == (int)StepStatus.Pending))
            {
                _logger.LogInformation("No se puede procesar el paso porque hay pasos anteriores sin aprobar.");
                throw new InvalidOperationException();
            }            
        }

        public async Task<ProjectProposalCompleteGetDTO> MapProjectWithSteps(ProjectProposal project)
        {
            var dto = _mapper.Map<ProjectProposalCompleteGetDTO>(project);
            var steps = await _unitOfWorkProjectApprovalStepQuery._repositoryProjectApprovalStepQuery.GetByProjectId(project.Id);
            dto.Steps = _mapper.Map<List<ProjectApprovalStepGetDTO>>(steps);
            return dto;
        }
    }
}
