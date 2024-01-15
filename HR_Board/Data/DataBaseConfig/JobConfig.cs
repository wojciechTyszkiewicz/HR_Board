using HR_Board.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Board.Data.DataBaseConfig
{
    public class JobConfig : BaseEntityConfig<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);

        }
    }

}
