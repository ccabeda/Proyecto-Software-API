using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryApproverRoleQuery
    {
        Task<ApproverRole?> GetById(int id);
        Task<List<ApproverRole>> GetAll();
    }
}
