using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Crm)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.Specialty)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(d => d.Availabilities)
                .WithOne()
                .HasForeignKey("DoctorId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}