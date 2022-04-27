using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripArc.Case.Data.Case.EntityMappings
{
    public class CaseActionMapping : IEntityTypeConfiguration<Domain.Action.Entities.CaseActions>
    {
        public void Configure(EntityTypeBuilder<Domain.Action.Entities.CaseActions> builder)
        {
            builder.ToTable("CaseActions");

            builder.HasKey(x => x.CaseActionId);            
        }
    }
}