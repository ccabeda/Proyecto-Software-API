using Proyecto_Software_Individual.Aplication.IRepository.IQuery;

namespace Proyecto_Software_Individual.Aplication.IUnitOfWork
{
    public interface IUnitOfWorkCatalogs
    {
        IRepositoryProjectTypeQuery _repositoryProjectTypeQuery { get; }
        IRepositoryAreaQuery _repositoryAreaQuery { get; }
        IRepositoryApprovalStatusQuery _repositoryApprovalStatusQuery { get; }
        IRepositoryApproverRoleQuery _repositoryApproverRoleQuery { get; }
        IRepositoryApprovalRuleQuery _repositoryApprovalRuleQuery { get; }
    }
}
