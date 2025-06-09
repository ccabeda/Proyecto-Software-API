using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Aplication.Utils;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ProjectCreator
    {
        private readonly IUnitOfWorkAreaQuery _unitOfWorkAreaQuery;
        private readonly IUnitOfWorkProjectTypeQuery _unitOfWorkProjectTypeQuery;
        private readonly IUnitOfWorkUserQuery _unitOfWorkUserQuery;
        private readonly IUnitOfWorkProjectProposalQuery _unitOfWorkProjectProposalQuery;
        private readonly IUnitOfWorkApprovalRuleQuery _unitOfWorkApprovalRuleQuery;
        private readonly ILogger<ServiceProjectProposalCreate> _logger;

        public ProjectCreator(IUnitOfWorkAreaQuery unitOfWorkAreaQuery, IUnitOfWorkProjectTypeQuery unitOfWorkProjectTypeQuery, IUnitOfWorkProjectProposalQuery unitOfWorkProjectProposalQuery, IUnitOfWorkApprovalRuleQuery unitOfWorkApprovalRuleQuery,
                              IUnitOfWorkUserQuery unitOfWorkUserQuery, ILogger<ServiceProjectProposalCreate> logger)
        {
            _unitOfWorkAreaQuery = unitOfWorkAreaQuery;
            _unitOfWorkProjectTypeQuery = unitOfWorkProjectTypeQuery;
            _unitOfWorkUserQuery = unitOfWorkUserQuery;
            _unitOfWorkProjectProposalQuery = unitOfWorkProjectProposalQuery;
            _unitOfWorkApprovalRuleQuery = unitOfWorkApprovalRuleQuery;
            _logger = logger;
        }

        public async Task<bool> ValidateProjectProposal(ProjectProposalCreateDTO projectDTO)
        {
            var area = await _unitOfWorkAreaQuery._repositoryAreaQuery.GetById(projectDTO.Area);
            var type = await _unitOfWorkProjectTypeQuery._repositoryProjectTypeQuery.GetById(projectDTO.Type);
            var user = await _unitOfWorkUserQuery._repositoryUserQuery.GetById(projectDTO.User);
            var existing = await _unitOfWorkProjectProposalQuery._repositoryProjectProposalQuery.GetByName(projectDTO.Title!);

            if (!Helper.CheckIfNull(existing))
            {
                _logger.LogInformation("Ya existe un proyecto con ese título.");
                return false;
            }
            if (Helper.CheckIfNull(area))
            {
                _logger.LogInformation("El ID de área ingresado no existe.");
                return false;
            }
            if (Helper.CheckIfNull(type))
            {
                _logger.LogInformation("El ID del tipo de proyecto ingresado no existe.");
                return false;
            }
            if (Helper.CheckIfNull(user))
            {
                _logger.LogInformation("El ID del usuario ingresado no existe.");
                return false;
            }
            return true;
        }

        public async Task<List<ProjectApprovalStep>> GenerateSteps(ProjectProposal projectProposal)
        {
            var rules = await _unitOfWorkApprovalRuleQuery._repositoryApprovalRuleQuery.GetAll();

            var leakedRules = rules
                .Where(r => r.MinAmount <= projectProposal.EstimatedAmount && (r.MaxAmount == 0 || projectProposal.EstimatedAmount <= r.MaxAmount))
                .ToList();

            return leakedRules
                .GroupBy(r => r.StepOrder)
                .Select(grupo =>
                {
                    var matchAmbos = grupo.FirstOrDefault(r => r.Area == projectProposal.Area && r.Type == projectProposal.Type);
                    if (matchAmbos != null) return matchAmbos;

                    var matchUno = grupo.FirstOrDefault(r => r.Area == projectProposal.Area || r.Type == projectProposal.Type);
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
