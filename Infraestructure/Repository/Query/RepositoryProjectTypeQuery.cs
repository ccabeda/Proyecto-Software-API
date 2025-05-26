using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data;

namespace Proyecto_Software_Individual.Infraestructure.Repository.Query
{
    public class RepositoryProjectTypeQuery : IRepositoryProjectTypeQuery
    {
        private readonly AplicationDbContext _context;
        public RepositoryProjectTypeQuery(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectType>> GetAll() => await _context.ProjectTypes.AsNoTracking().ToListAsync();
        public async Task<ProjectType?> GetById(int id) => await _context.ProjectTypes.FindAsync(id);
    }
}
