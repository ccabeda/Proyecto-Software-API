using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryProjectTypeQuery
    {
        Task<List<ProjectType>> GetAll();
        Task<ProjectType?> GetById(int id);
    }
}
