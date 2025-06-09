using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Query
{
    public class RepositoryAreaQuery : IRepositoryAreaQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryAreaQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> GetAll() => await _context.Area.AsNoTracking().ToListAsync();
        public async Task<Area?> GetById(int id) => await _context.Area.FindAsync(id);
    }
}
