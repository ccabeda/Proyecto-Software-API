using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Query
{
    public class RepositoryUserQuery : IRepositoryUserQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryUserQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll() => await _context.Users.AsNoTracking().ToListAsync(); 
        public async Task<User?> GetById(int? id) => await _context.Users.FindAsync(id);
        public async Task<User?> GetUser(string email) => await _context.Users.Include(u => u.ApproverRole).FirstOrDefaultAsync(u => u.Email.ToLower() == email);
    }
}
