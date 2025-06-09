using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryAreaQuery
    {
        Task<Area?> GetById(int id);
        Task<List<Area>> GetAll();
    }
}
