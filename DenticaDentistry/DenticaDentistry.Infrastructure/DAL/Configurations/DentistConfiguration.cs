using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DenticaDentistry.Infrastructure.DAL.Configurations;

internal sealed class DentistConfiguration : IEntityTypeConfiguration<Dentist>
{
    public void Configure(EntityTypeBuilder<Dentist> builder)
    {
        builder.HasKey(x => x.DentistId);
        builder.Property(x => x.DentistId)
            .HasConversion(x => x.Value, x => new DentistId(x));
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new UserId(x));
        builder.Property(x => x.LicenseNumber)
            .IsRequired()
            .HasMaxLength(15);
        builder.HasIndex(x => x.LicenseNumber).IsUnique();
    }
}