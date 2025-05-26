using AutoMapper;
using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Shared;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ProjectCreator
    {
        private readonly IUnitOfWorkProjects _unitOfWorkProjects;
        private readonly IUnitOfWorkCatalogs _unitOfWorkCatalogs;
        private readonly IUnitOfWorkUsers _unitOfWorkUsers;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;

        public ProjectCreator(IUnitOfWorkProjects unitOfWorkProjects, ILogger<ServiceProjectProposalCreate> logger, IUnitOfWorkCatalogs unitOfWorkCatalogs, IUnitOfWorkUsers unitOfWorkUsers)
        {
            _unitOfWorkProjects = unitOfWorkProjects;
            _logger = logger;
            _unitOfWorkCatalogs = unitOfWorkCatalogs;
            _unitOfWorkUsers = unitOfWorkUsers;
        }

        public async Task<bool> ValidateProjectProposal(ProjectProposalCreateDTO projectDTO)
        {
            var area = await _unitOfWorkCatalogs._repositoryAreaQuery.GetById(projectDTO.Area);
            var type = await _unitOfWorkCatalogs._repositoryProjectTypeQuery.GetById(projectDTO.Type);
            var user = await _unitOfWorkUsers._repositoryUserQuery.GetById(projectDTO.User);
            var existing = await _unitOfWorkProjects._repositoryProjectProposalQuery.GetByName(projectDTO.Title!);

            if (!ControllerHelper.CheckIfNull(existing))
            {
                _logger.LogInformation("Ya existe un proyecto con ese título.");
                return false;
            }
            if (ControllerHelper.CheckIfNull(area))
            {
                _logger.LogInformation("El ID de área ingresado no existe.");
                return false;
            }
            if (ControllerHelper.CheckIfNull(type))
            {
                _logger.LogInformation("El ID del tipo de proyecto ingresado no existe.");
                return false;
            }
            if (ControllerHelper.CheckIfNull(user))
            {
                _logger.LogInformation("El ID del usuario ingresado no existe.");
                return false;
            }
            return true;
        }

        public async Task<List<ProjectApprovalStep>> GenerateSteps(ProjectProposal projectProposal)
        {
            var rules = await _unitOfWorkCatalogs._repositoryApprovalRuleQuery.GetAll();

            var leakedRules = rules
                .Where(r => r.MinAmount <= projectProposal.EstimatedAmount && (r.MaxAmount == 0 || projectProposal.EstimatedAmount <= r.MaxAmount))
                .ToList();

            return leakedRules
                .GroupBy(r => r.StepOrder)
                .Select(grupo =>
                {
                    var matchAmbos = grupo.FirstOrDefault(r => r.AreaId == projectProposal.Area && r.Type == projectProposal.Type);
                    if (matchAmbos != null) return matchAmbos;

                    var matchUno = grupo.FirstOrDefault(r => r.AreaId == projectProposal.Area || r.Type == projectProposal.Type);
                    if (matchUno != null) return matchUno;

                    return grupo.FirstOrDefault();
                })
                .Where(r => r != null)
                .Select(r => new ProjectApprovalStep
                {
                    ProjectProposalId = projectProposal.Id,
                    StepOrder = r!.StepOrder,
                    ApproverRoleId = r.ApproverRoleId,
                    Status = 1,
                    ApproverUserId = null,
                    DesicionDate = null,
                    Observations = ""
                })
                .ToList();
        }
    }
}
