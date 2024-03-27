using HR_Board.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Board.Data.DataBaseConfig
{
    public abstract class BaseEntityConfig<IBaseEntity> : IEntityTypeConfiguration<IBaseEntity> where IBaseEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<IBaseEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }

}
