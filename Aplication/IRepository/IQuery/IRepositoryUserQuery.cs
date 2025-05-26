using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryUserQuery
    {
        Task<User?> GetUser(string mail);
        Task<User?> GetById(int? id);
        Task<List<User>> GetAll();
    }
}
