using Proyecto_Software_Individual.Aplication.IRepository.ICommand;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Command
{
    public class RepositoryProjectApprovalStepCommand : IRepositoryProjectApprovalStepCommand
    {
        private readonly AplicationDbContext _context;
        public RepositoryProjectApprovalStepCommand(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(ProjectApprovalStep projectApprovalStep) => await _context.ProjectApprovalStep.AddAsync(projectApprovalStep);

        public async Task Update(ProjectApprovalStep projectApprovalStep) => _context.Update(projectApprovalStep);
    }
}
