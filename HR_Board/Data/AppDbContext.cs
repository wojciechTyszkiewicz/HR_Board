﻿using Microsoft.EntityFrameworkCore;

namespace HR_Board.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja BaseEntity
            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Modified);

            foreach (var entity in entities)
            {
               baseEntity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
