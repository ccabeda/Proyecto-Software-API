using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository
{
    public class RepositoryProjectApprovalStepQuery : IRepositoryProjectApprovalStepQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryProjectApprovalStepQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectApprovalStep?> GetById(long id) => await _context.ProjectApprovalSteps.FindAsync(id);
        public async Task<List<ProjectApprovalStep>> GetPendingByRol(int approverRoleId) => await _context.ProjectApprovalSteps.Include(p => p.ProjectProposal)
                                                                                                                               .Where(p => p.Status == 1 && p.ApproverRoleId == approverRoleId)
                                                                                                                               .OrderBy(p => p.StepOrder)
                                                                                                                               .ThenBy(p => p.ProjectProposal.CreateAt).ToListAsync();
        public async Task<List<ProjectApprovalStep>> GetByProjectId(Guid projectProposalId) => await _context.ProjectApprovalSteps.Where(p => p.ProjectProposalId == projectProposalId)
                                                                                                                                  .OrderBy(p => p.StepOrder)
                                                                                                                                  .Include(p => p.User)
                                                                                                                                  .Include(p => p.ApproverRole)
                                                                                                                                  .Include(p => p.ApprovalStatus).ToListAsync();
            

        public async Task<List<ProjectApprovalStep>> GetAll() => await _context.ProjectApprovalSteps.AsNoTracking().ToListAsync();
    }
}
