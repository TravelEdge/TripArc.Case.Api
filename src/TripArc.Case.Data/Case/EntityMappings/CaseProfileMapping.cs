using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripArc.Case.Domain.Case.Entities;

namespace TripArc.Case.Data.Case.EntityMappings
{
    public class CaseProfileMapping : IEntityTypeConfiguration<CaseProfile>
    {
        public void Configure(EntityTypeBuilder<CaseProfile> builder)
        {
            builder.ToTable("CaseProfiles");

            builder.HasKey(x => x.CaseProfileId);                        
        }
    }
}