using HR_Board.Data.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace HR_Board.Data.Entities
{
    public class AppDbContext : IdentityDbContext<ApiUser, IdentityRole<Guid>, Guid>
    {

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Meeting> Meetings { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            SavingChanges += AppDbContext_SavingChanges;
            SavingChanges += AppDbContext_SavedChanges;
            SaveChangesFailed += AppDbContext_SaveChangesFailed;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        private void AppDbContext_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        private void AppDbContext_SavedChanges(object sender, SavingChangesEventArgs e)
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        private void AppDbContext_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            ChangeTracker.DetectChanges();

            /*            var entities = ChangeTracker.Entries()
                            .Where(x => x.State == EntityState.Modified);*/


            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(IBaseEntity.CreatedAt)] = DateTime.UtcNow;
                        entry.State = EntityState.Added;
                        break;
                    case EntityState.Deleted:
                        entry.CurrentValues[nameof(IBaseEntity.IsDeleted)] = true;
                        entry.State = EntityState.Modified;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues[nameof(IBaseEntity.UpdatedAt)] = DateTime.UtcNow;
                        break;
                }
            }
        }

    }
}