using Proyecto_Software_Individual.Aplication.IRepository.ICommand;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;

namespace Proyecto_Software_Individual.Aplication.IUnitOfWork
{
    public interface IUnitOfWorkProjects
    {
        IRepositoryProjectProposalCommand _repositoryProjectProposalCommand { get; }
        IRepositoryProjectProposalQuery _repositoryProjectProposalQuery { get; }
        IRepositoryProjectApprovalStepCommand _repositoryProjectApprovalStepCommand { get; }
        IRepositoryProjectApprovalStepQuery _repositoryProjectApprovalStepQuery { get; }
        Task Save();
    }
}
