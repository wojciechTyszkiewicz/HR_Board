using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            SetCreatedAt();
            return base.SaveChanges();
        }

        private void SetCreatedAt()
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entity in entries)
            {
                if (entity.Entity.CreatedAt == DateTime.MinValue)
                {
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }


        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
