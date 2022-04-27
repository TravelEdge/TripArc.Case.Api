using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripArc.Case.Domain.Action.Entities;

namespace TripArc.Case.Data.Action.Entities
{
    public class ActionMapping : IEntityTypeConfiguration<Actions>
    {
        public void Configure(EntityTypeBuilder<Actions> builder)
        {
            builder.ToTable("Actions");

            builder.HasKey(x => x.ActionId);            
        }
    }
}