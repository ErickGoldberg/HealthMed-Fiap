using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Configuration
{
    public class DoctorAvailabilityConfiguration : IEntityTypeConfiguration<DoctorAvailability>
    {
        public void Configure(EntityTypeBuilder<DoctorAvailability> builder)
        {
            builder.HasKey(da => da.Id);

            builder.Property(da => da.StartTime)
                .IsRequired();

            builder.Property(da => da.EndTime)
                .IsRequired();

            builder.Property<Guid>("DoctorId");

            builder.HasOne<Doctor>()
                .WithMany(d => d.Availabilities)
                .HasForeignKey("DoctorId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
