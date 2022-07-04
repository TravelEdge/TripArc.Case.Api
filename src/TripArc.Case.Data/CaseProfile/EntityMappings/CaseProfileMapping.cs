using Entities = TripArc.Case.Domain.CaseProfile.Entities;

namespace TripArc.Case.Data.CaseProfile.EntityMappings;

public class CaseProfileMapping : IEntityTypeConfiguration<Entities.CaseProfile>
{
    public void Configure(EntityTypeBuilder<Entities.CaseProfile> builder)
    {
        builder.ToTable("CaseProfiles");

        builder.HasKey(x => x.CaseProfileId);                        
    }
}