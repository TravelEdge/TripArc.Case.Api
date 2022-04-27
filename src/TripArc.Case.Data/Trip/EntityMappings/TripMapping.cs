using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripArc.Case.Data.Trip.EntityMappings
{
    public class TripMapping : IEntityTypeConfiguration<Domain.Trip.Entities.Trip>
    {
        public void Configure(EntityTypeBuilder<Domain.Trip.Entities.Trip> builder)
        {
            builder.ToTable("Trip");

            builder.HasKey(x => x.TripId);            
        }
    }
}