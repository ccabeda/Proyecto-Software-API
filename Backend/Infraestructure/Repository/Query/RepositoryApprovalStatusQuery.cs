using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Query
{
    public class RepositoryApprovalStatusQuery : IRepositoryApprovalStatusQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryApprovalStatusQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalStatus>> GetAll() => await _context.ApprovalStatus.AsNoTracking().ToListAsync();

        public async Task<ApprovalStatus?> GetById(int id) => await _context.ApprovalStatus.FindAsync(id);
    }
}
