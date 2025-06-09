using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Aplication.IRepository.ICommand
{
    public interface IRepositoryProjectApprovalStepCommand
    {
        Task Create(ProjectApprovalStep projectApprovalStep);
        Task Update(ProjectApprovalStep projectApprovalStep);

    }
}
