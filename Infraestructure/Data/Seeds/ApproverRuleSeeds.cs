using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Infraestructure.Data.Seeds
{
    public static class ApproverRuleSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApproverRole>().HasData(
                new ApproverRole { Id = 1, Name = "Líder de Área" },
                new ApproverRole { Id = 2, Name = "Gerente" },
                new ApproverRole { Id = 3, Name = "Director" },
                new ApproverRole { Id = 4, Name = "Comité Técnico" }
            );
        }
    }
}
