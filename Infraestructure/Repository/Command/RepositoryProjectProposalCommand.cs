using Proyecto_Software_Individual.Aplication.IRepository.ICommand;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Command
{
    public class RepositoryProjectProposalCommand : IRepositoryProjectProposalCommand
    {
        private readonly AplicationDbContext _context;
        public RepositoryProjectProposalCommand(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(ProjectProposal projectProposal) => await _context.ProjectProposals.AddAsync(projectProposal);
        
        public async Task Update(ProjectProposal projectProposal) =>  _context.ProjectProposals.Update(projectProposal);
    }
}
