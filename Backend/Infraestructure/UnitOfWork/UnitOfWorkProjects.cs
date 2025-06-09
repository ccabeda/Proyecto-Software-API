using Proyecto_Software_Individual.Aplication.IRepository.ICommand;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.UnitOfWork
{
    public class UnitOfWorkProjects : IUnitOfWorkProjectProposalCommand, IUnitOfWorkProjectApprovalStepCommand, IUnitOfWorkProjectApprovalStepQuery, IUnitOfWorkProjectProposalQuery
    {
        private readonly AplicationDbContext _context;

        public IRepositoryProjectProposalCommand _repositoryProjectProposalCommand { get; }
        public IRepositoryProjectProposalQuery _repositoryProjectProposalQuery { get; }
        public IRepositoryProjectApprovalStepCommand _repositoryProjectApprovalStepCommand { get; }
        public IRepositoryProjectApprovalStepQuery _repositoryProjectApprovalStepQuery { get; }

        public UnitOfWorkProjects(
            AplicationDbContext context,
            IRepositoryProjectProposalCommand projectProposalCommand,
            IRepositoryProjectProposalQuery projectProposalQuery,
            IRepositoryProjectApprovalStepCommand approvalStepCommand,
            IRepositoryProjectApprovalStepQuery approvalStepQuery)
        {
            _context = context;
            _repositoryProjectProposalCommand = projectProposalCommand;
            _repositoryProjectProposalQuery = projectProposalQuery;
            _repositoryProjectApprovalStepCommand = approvalStepCommand;
            _repositoryProjectApprovalStepQuery = approvalStepQuery;
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
