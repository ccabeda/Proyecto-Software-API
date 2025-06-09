using Proyecto_Software_Individual.Aplication.DTOs.ProjectProposalDTOs;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.Service.ServiceProjectProposal
{
    public class ProjectFilters
    {
        private readonly IUnitOfWorkProjectApprovalStepQuery _unitOfWorkProjectApprovalStepQuery;

        public ProjectFilters(IUnitOfWorkProjectApprovalStepQuery unitOfWorkProjectApprovalStepQuery)
        {
            _unitOfWorkProjectApprovalStepQuery = unitOfWorkProjectApprovalStepQuery;
        }

        public async Task<IQueryable<ProjectProposal>> ApplyFilters(IQueryable<ProjectProposal> queryProjectProposal, ProjectProposalFilterDTO filtersDTO)
        {
            if (!string.IsNullOrWhiteSpace(filtersDTO.title))
            {
                queryProjectProposal = queryProjectProposal.Where(p => p.Title.Contains(filtersDTO.title, StringComparison.OrdinalIgnoreCase));
            }
            if (filtersDTO.status.HasValue)
            {
                queryProjectProposal = queryProjectProposal.Where(p => p.Status == filtersDTO.status.Value);
            }
            if (filtersDTO.applicant.HasValue)
            {
                queryProjectProposal = queryProjectProposal.Where(p => p.CreateBy == filtersDTO.applicant.Value);
            }
            if (filtersDTO.approvalUser.HasValue)
            {
                var steps = await _unitOfWorkProjectApprovalStepQuery._repositoryProjectApprovalStepQuery.GetAll();
                var projectIds = steps
                    .Where(s => s.ApproverUserId == filtersDTO.approvalUser)
                    .Select(s => s.ProjectProposalId)
                    .Distinct()
                    .ToHashSet();
                queryProjectProposal = queryProjectProposal.Where(p => projectIds.Contains(p.Id));
            }
            return queryProjectProposal;
        }
    }
}
