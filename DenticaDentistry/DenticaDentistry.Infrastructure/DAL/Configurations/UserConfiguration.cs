using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DenticaDentistry.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x));
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Username)
            .HasConversion(x => x.Value, x => new Username(x))
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, x => new Password(x))
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x => x.Fullname)
            .HasConversion(x => x.Value, x => new Fullname(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x.Value, x=> new PhoneNumber(x))
            .IsRequired()
            .HasMaxLength(12);
        builder.Property(x => x.Role)
            .HasConversion(x => x.Value, x => new Role(x))
            .IsRequired()
            .HasMaxLength(30);
    }
}