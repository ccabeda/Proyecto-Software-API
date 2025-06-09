using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;

namespace Proyecto_Software_Individual.Infraestructure.UnitOfWork
{
    public class UnitOfWorkCatalogs : IUnitOfWorkProjectTypeQuery, IUnitOfWorkAreaQuery, IUnitOfWorkApprovalStatusQuery, IUnitOfWorkApproverRoleQuery, IUnitOfWorkApprovalRuleQuery
    {

        public IRepositoryProjectTypeQuery _repositoryProjectTypeQuery { get; }
        public IRepositoryAreaQuery _repositoryAreaQuery { get; }
        public IRepositoryApprovalStatusQuery _repositoryApprovalStatusQuery { get; }
        public IRepositoryApproverRoleQuery _repositoryApproverRoleQuery { get; }
        public IRepositoryApprovalRuleQuery _repositoryApprovalRuleQuery { get; }

        public UnitOfWorkCatalogs(
            IRepositoryApprovalRuleQuery repositoryApprovalRuleQuery,
            IRepositoryProjectTypeQuery projectTypeQuery,
            IRepositoryAreaQuery areaQuery,
            IRepositoryApprovalStatusQuery approvalStatusQuery,
            IRepositoryApproverRoleQuery approverRoleQuery)
        {
            _repositoryApprovalRuleQuery = repositoryApprovalRuleQuery;
            _repositoryProjectTypeQuery = projectTypeQuery;
            _repositoryAreaQuery = areaQuery;
            _repositoryApprovalStatusQuery = approvalStatusQuery;
            _repositoryApproverRoleQuery = approverRoleQuery;
        }
    }
}
