using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository
{
    public class RepositoryProjectProposalQuery : IRepositoryProjectProposalQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryProjectProposalQuery(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(ProjectProposal projectProposal) => await _context.ProjectProposals.AddAsync(projectProposal);
        public async Task<List<ProjectProposal>> GetAll() => await _context.ProjectProposals.Include(p => p.Area)
                                                                                            .Include(p => p.ProjectType)
                                                                                            .Include(p => p.ApprovalStatus)
                                                                                            .Include(p => p.User)
                                                                                               .ThenInclude(s => s.ApproverRole).ToListAsync();

        public async Task<ProjectProposal?> GetById(Guid id) => await _context.ProjectProposals.Include(p => p.Area)
                                                                                                .Include(p => p.ProjectType)
                                                                                                .Include(p => p.ApprovalStatus)
                                                                                                .Include(p => p.User)
                                                                                                    .ThenInclude(s => s.ApproverRole).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ProjectProposal?> GetByName(string name) => await _context.ProjectProposals.FirstOrDefaultAsync(p => p.Title.ToLower() == name.ToLower());
    }

}
