using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryApprovalRuleQuery
    {
        Task<List<ApprovalRule>> GetAll();
    }
}
