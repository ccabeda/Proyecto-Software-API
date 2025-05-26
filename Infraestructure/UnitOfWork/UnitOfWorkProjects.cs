using Proyecto_Software_Individual.Aplication.IRepository.ICommand;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.UnitOfWork
{
    public class UnitOfWorkProjects : IUnitOfWorkProjects
    {
        private readonly AplicationDbContext _context;

        public IRepositoryProjectProposalCommand _repositoryProjectProposalCommand { get; }
        public IRepositoryProjectProposalQuery _repositoryProjectProposalQuery { get; }
        public IRepositoryProjectApprovalStepCommand _repositoryProjectApprovalStepCommand { get; }
        public IRepositoryProjectApprovalStepQuery _repositoryProjectApprovalStepQuery { get; }
        public IRepositoryUserQuery _repositoryUserQuery { get; }

        public UnitOfWorkProjects(
            AplicationDbContext context,
            IRepositoryUserQuery UserQuery,
            IRepositoryProjectProposalCommand projectProposalCommand,
            IRepositoryProjectProposalQuery projectProposalQuery,
            IRepositoryProjectApprovalStepCommand approvalStepCommand,
            IRepositoryProjectApprovalStepQuery approvalStepQuery,
            IRepositoryUserQuery userQuery)
        {
            _context = context;
            _repositoryProjectProposalCommand = projectProposalCommand;
            _repositoryProjectProposalQuery = projectProposalQuery;
            _repositoryProjectApprovalStepCommand = approvalStepCommand;
            _repositoryProjectApprovalStepQuery = approvalStepQuery;
            _repositoryUserQuery = userQuery;
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
