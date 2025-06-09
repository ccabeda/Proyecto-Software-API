using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.IQuery
{
    public interface IRepositoryProjectProposalQuery
    {
        Task<ProjectProposal?> GetById(Guid id);
        Task<ProjectProposal?> GetByName(string name);
        Task<List<ProjectProposal>> GetAll();
    }
}
