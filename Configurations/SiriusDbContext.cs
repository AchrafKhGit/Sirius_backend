
using Microsoft.AspNetCore.Identity;
using sirius.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace sirius.Configurations;

public class SiriusDbContext : IdentityDbContext
{
    public SiriusDbContext(DbContextOptions<SiriusDbContext> options) : base(options){ }
    
    public DbSet<Livrable> Livrables { get; set; }
    public DbSet<HypothesisCategory> HypothesisCategories { get; set; }
    public DbSet<OperationalPrioritization> OperationalPrioritizations { get; set; }
    public DbSet<Hypothesis> Hypothesis { get; set; }
    public DbSet<HypothesisHistory> HypothesisHistories { get; set; }
    public DbSet<MigrationHistory> MigrationHistories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Livrable>().ToTable("Livrables");
        builder.Entity<HypothesisCategory>().ToTable("HypothesisCategories");
        builder.Entity<OperationalPrioritization>().ToTable("OperationalPrioritizations");
        builder.Entity<Hypothesis>().ToTable("Hypothesis");
        builder.Entity<HypothesisHistory>().ToTable("HypothesisHistories");
        builder.Entity<MigrationHistory>().ToTable("MigrationHistories");

        builder.Entity<Livrable>().HasKey(x => x.Id);
        builder.Entity<Hypothesis>()
            .HasOne(h => h.Category)
            .WithMany(c => c.Hypothesis)
            .HasForeignKey(h => h.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<HypothesisCategory>().HasKey(x => x.Id);
        builder.Entity<OperationalPrioritization>().HasKey(x => x.Id);
        builder.Entity<Hypothesis>().HasKey(x => x.Id);
        builder.Entity<HypothesisHistory>().HasOne(x => x.Hypothesis).WithMany(y => y.History).HasForeignKey(x => x.HypothesisId);
        

        base.OnModelCreating(builder);
        
    }
   
}

