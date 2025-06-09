using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryApprovalStatusQuery
    {
        Task<ApprovalStatus?> GetById(int id);
        Task<List<ApprovalStatus>> GetAll();
    }
}
