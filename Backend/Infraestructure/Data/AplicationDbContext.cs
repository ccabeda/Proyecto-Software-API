using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Domain.Models;
using Proyecto_Software_Individual.Infraestructure.Data.Seeds;

namespace Proyecto_Software_Individual.Infraestructure.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApprovalRule> ApprovalRule { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatus { get; set; }
        public DbSet<ApproverRole> ApproverRole { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<ProjectApprovalStep> ProjectApprovalStep { get; set; }
        public DbSet<ProjectProposal> ProjectProposal { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // datos iniciales
            ApprovalRuleSeeds.Seed(modelBuilder);
            ApprovalStatusSeeds.Seed(modelBuilder);
            ApproverRuleSeeds.Seed(modelBuilder);
            AreaSeeds.Seed(modelBuilder);
            ProjectTypeSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);

            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(p => p.ProjectProposal)
                .WithMany()
                .HasForeignKey(p => p.ProjectProposalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.ApproverUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(p => p.ApproverRole)
                .WithMany()
                .HasForeignKey(p => p.ApproverRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(p => p.ApprovalStatus)
                .WithMany()
                .HasForeignKey(p => p.Status)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<ApprovalRule>()
                .Property(p => p.MinAmount)
                .HasPrecision(18, 2);  

            modelBuilder.Entity<ApprovalRule>()
                .Property(p => p.MaxAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.EstimatedAmount)
                .HasPrecision(18, 2);
        }
    }
}
