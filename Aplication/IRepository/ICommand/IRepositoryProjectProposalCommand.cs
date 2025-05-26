using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.ICommand
{
    public interface IRepositoryProjectProposalCommand
    {
        Task Create(ProjectProposal projectProposal);

        Task Update(ProjectProposal projectProposal);
    }
}
