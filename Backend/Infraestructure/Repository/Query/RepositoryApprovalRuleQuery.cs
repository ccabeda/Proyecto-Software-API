using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Query
{
    public class RepositoryApprovalRuleQuery : IRepositoryApprovalRuleQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryApprovalRuleQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalRule>> GetAll() => await _context.ApprovalRule.AsNoTracking().ToListAsync();
    }
}
