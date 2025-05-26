using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectApprovalStepDTOs;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectApprovalStep
{
    public class StepValidator //funciones auxiliares
    {
        private readonly IUnitOfWorkProjects _unitOfWork;
        private readonly IUnitOfWorkUsers _unitOfWorkUsers;
        private readonly IMapper _mapper;

        public StepValidator(IUnitOfWorkProjects unitOfWork, IUnitOfWorkUsers unitOfWorkUsers, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkUsers = unitOfWorkUsers;
            _mapper = mapper;
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
            var project = await _unitOfWork._repositoryProjectProposalQuery.GetById(projectId);
            if (project == null)
                throw new KeyNotFoundException("Proyecto no encontrado");
            return project;
        }

        public async Task<ProjectApprovalStep> GetStepByIdAndValidate(Guid projectId, long stepId)
        {
            var step = await _unitOfWork._repositoryProjectApprovalStepQuery.GetById(stepId);
            if (step == null || step.ProjectProposalId != projectId)
                throw new KeyNotFoundException("Paso no encontrado o no corresponde al proyecto.");
            return step;
        }

        public async Task<User> GetUserAndValidate(int userId, ProjectApprovalStep step)
        {
            var user = await _unitOfWorkUsers._repositoryUserQuery.GetById(userId);
            if (user == null)
            {
                throw new ArgumentException("Datos de decisión inválidos");
            }
            if (user.Role != step.ApproverRoleId)
            {
                throw new ArgumentException("Usuario intentó aprobar el paso sin tener el rol apropiado ");
            }
            if (step.Status == (int)StepStatus.Approved || step.Status == (int)StepStatus.Rejected)
            {
                throw new InvalidOperationException("No se puede modificar un paso que ya fue aprobado o rechazado.");
            }
            return user;
        }

        public async Task ValidateStepOrderRules(Guid projectId, ProjectApprovalStep step)
        {
            var steps = await _unitOfWork._repositoryProjectApprovalStepQuery.GetByProjectId(projectId);

            if (steps.Any(s => s.StepOrder < step.StepOrder && (s.Status == (int)StepStatus.Rejected || s.Status == (int)StepStatus.Observed)))
                throw new InvalidOperationException("No se puede modificar el paso porque ya hay pasos previos rechazados u observados.");

            if (steps.Any(s => s.StepOrder < step.StepOrder && s.Status == (int)StepStatus.Pending))
                throw new InvalidOperationException("No se puede procesar el paso porque hay pasos anteriores sin aprobar.");
        }

        public async Task<ProjectProposalCompleteGetDTO> MapProjectWithSteps(ProjectProposal project)
        {
            var dto = _mapper.Map<ProjectProposalCompleteGetDTO>(project);
            var steps = await _unitOfWork._repositoryProjectApprovalStepQuery.GetByProjectId(project.Id);
            dto.Steps = _mapper.Map<List<ProjectApprovalStepGetDTO>>(steps);
            return dto;
        }
    }
}
