using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Board.Data.DataBaseConfig
{
    public abstract class BaseEntityConfig<IBaseEntity> : IEntityTypeConfiguration<IBaseEntity> where IBaseEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<IBaseEntity> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }

}
