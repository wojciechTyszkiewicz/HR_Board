using HR_Board.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Board.Data.DataBaseConfig
{
    public class JobConfig : BaseEntityConfig<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> builder)
        {
            base.Configure(builder);

            builder.Property(j => j.Title).HasMaxLength(50);
            builder.Property(j => j.ShortDescription).HasMaxLength(15);
            builder.Property(j => j.LongDescription).HasMaxLength(250);
            builder.Property(j => j.CompanyName).HasMaxLength(50);

            builder.HasOne<ApiUser>()
           .WithMany()
           .HasForeignKey(j => j.CreatedBy);
        }
    }

}
