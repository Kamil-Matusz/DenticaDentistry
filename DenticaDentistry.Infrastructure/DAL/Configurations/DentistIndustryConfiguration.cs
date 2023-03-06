using Dentica_Dentistry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentica_Dentistry.Infrastructure.DAL.Configurations;

internal sealed class DentistIndustryConfiguration : IEntityTypeConfiguration<DentistIndustry>
{
    public void Configure(EntityTypeBuilder<DentistIndustry> builder)
    {
        builder.HasKey(x => x.DentistIndustryId);
    }
}