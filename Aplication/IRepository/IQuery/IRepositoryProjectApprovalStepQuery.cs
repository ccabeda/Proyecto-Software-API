using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryProjectApprovalStepQuery
    {
        Task<List<ProjectApprovalStep>> GetAll();
        Task<List<ProjectApprovalStep>> GetPendingByRol(int approverRoleId);
        Task<ProjectApprovalStep?> GetById(long id);
        Task<List<ProjectApprovalStep>> GetByProjectId(Guid projectProposalId);
    }
}
