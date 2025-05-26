using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Infraestructure.Data.Seeds
{
    public static class ApprovalStatusSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalStatus>().HasData(
            new ApprovalStatus { Id = 1, Name = "Pending" },
            new ApprovalStatus { Id = 2, Name = "Approved" },
            new ApprovalStatus { Id = 3, Name = "Rejected" },
            new ApprovalStatus { Id = 4, Name = "Observed" }
            );
        }
    }
}
