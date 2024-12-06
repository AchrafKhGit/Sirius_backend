
using Microsoft.AspNetCore.Identity;
using sirius.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Activity = sirius.Entities.Activity;
using Task = sirius.Entities.Task;

namespace sirius.Configurations;

public class SiriusDbContext : IdentityDbContext
{
    public SiriusDbContext(DbContextOptions<SiriusDbContext> options) : base(options){ }
    
    public DbSet<Livrable> Livrables { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<HypothesisCategory> HypothesisCategories { get; set; }
    public DbSet<OperationalPrioritization> OperationalPrioritizations { get; set; }
    public DbSet<Hypothesis> Hypothesis { get; set; }
    public DbSet<HypothesisHistory> HypothesisHistories { get; set; }
    public DbSet<MigrationHistory> MigrationHistories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Livrable>().ToTable("Livrables");
        builder.Entity<Expense>().ToTable("Expenses");
        builder.Entity<Task>().ToTable("Tasks");
        builder.Entity<Activity>().ToTable("Activities");
        builder.Entity<Lot>().ToTable("Lots");
        builder.Entity<HypothesisCategory>().ToTable("HypothesisCategories");
        builder.Entity<OperationalPrioritization>().ToTable("OperationalPrioritizations");
        builder.Entity<Hypothesis>().ToTable("Hypothesis");
        builder.Entity<HypothesisHistory>().ToTable("HypothesisHistories");
        builder.Entity<MigrationHistory>().ToTable("MigrationHistories");

        builder.Entity<Livrable>().HasKey(x => x.Id);
        builder.Entity<Expense>().HasKey(x => x.Id);
        builder.Entity<Task>()
            .HasOne(x => x.Activity)
            .WithMany(y => y.Tasks)
            .HasForeignKey(x => x.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Activity>().HasOne(x => x.Lot).WithMany(y => y.Activities).HasForeignKey(x => x.LotId);
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

