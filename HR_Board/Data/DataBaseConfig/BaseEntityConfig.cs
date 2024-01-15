using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Board.Data.DataBaseConfig
{
    public abstract class BaseEntityConfig<TModel> : IEntityTypeConfiguration<TModel> where TModel : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }

}
