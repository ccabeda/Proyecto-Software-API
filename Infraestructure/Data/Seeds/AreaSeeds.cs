using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Infraestructure.Data.Seeds
{
    public static class AreaSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().HasData(
            new Area { Id = 1, Name = "Finanzas" },
            new Area { Id = 2, Name = "Tecnología" },
            new Area { Id = 3, Name = "Recursos Humanos" },
            new Area { Id = 4, Name = "Operaciones" }
            );
        }
    }
}
