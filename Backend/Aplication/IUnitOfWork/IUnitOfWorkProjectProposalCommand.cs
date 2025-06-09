using Proyecto_Software_Individual.Aplication.IRepository.ICommand;

namespace Proyecto_Software_Individual.Aplication.IUnitOfWork
{
    public interface IUnitOfWorkProjectProposalCommand
    {
        IRepositoryProjectProposalCommand _repositoryProjectProposalCommand { get; }
        Task Save();
    }
}
