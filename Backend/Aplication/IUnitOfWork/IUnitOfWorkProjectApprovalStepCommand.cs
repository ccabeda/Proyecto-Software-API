using Proyecto_Software_Individual.Aplication.IRepository.ICommand;

namespace Proyecto_Software_Individual.Aplication.IUnitOfWork
{
    public interface IUnitOfWorkProjectApprovalStepCommand
    {
        IRepositoryProjectApprovalStepCommand _repositoryProjectApprovalStepCommand { get; }
        Task Save();
    }
}
