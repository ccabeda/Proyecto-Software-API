using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Query
{
    public class RepositoryApproverRoleQuery : IRepositoryApproverRoleQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryApproverRoleQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApproverRole>> GetAll() => await _context.ApproverRoles.AsNoTracking().ToListAsync();

        public async Task<ApproverRole?> GetById(int id) => await _context.ApproverRoles.FindAsync(id);
    }
}
