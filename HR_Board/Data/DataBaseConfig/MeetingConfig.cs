using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HR_Board.Data.DataBaseConfig
{
    public class MeetingConfig : BaseEntityConfig<Meeting>
    {
        public override void Configure(EntityTypeBuilder<Meeting> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);

            builder.Property(x => x.MeetingType)
                .HasConversion(new EnumToStringConverter<MeetingType>());

            builder.HasOne(e => e.Job)

            builder.HasOne(e => e.Candidate)
                .WithMany(e => e.Meeting)

                
        }
    }

}
