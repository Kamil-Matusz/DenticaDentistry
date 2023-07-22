using DenticaDentistry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DenticaDentistry.Infrastructure.DAL.Configurations;

internal sealed class DentistIndustryConfiguration : IEntityTypeConfiguration<DentistIndustry>
{
    public void Configure(EntityTypeBuilder<DentistIndustry> builder)
    {
        builder.HasKey(x => x.DentistIndustryId);
        builder.HasOne<ServiceType>().WithMany().HasForeignKey(x => x.ServiceTypeId);
        builder.Property(p => p.ServiceTypeId).IsRequired();
    }
}