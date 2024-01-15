﻿using HR_Board.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace HR_Board.Data
{
    public class AppDbContext : IdentityDbContext<ApiUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            SavingChanges += AppDbContext_SavingChanges;
            SavingChanges += AppDbContext_SavedChanges;
            SaveChangesFailed += AppDbContext_SaveChangesFailed;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}