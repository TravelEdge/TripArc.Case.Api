namespace TripArc.Case.Data.Itinerary.EntityMappings;

public class ItineraryMapping : IEntityTypeConfiguration<Domain.Itinerary.Entities.Itinerary>
{
    public void Configure(EntityTypeBuilder<Domain.Itinerary.Entities.Itinerary> builder)
    {
        builder.ToTable("Itinerary");
        
        builder.Property(e => e.Name)
            .HasMaxLength(150);

        builder.HasKey(x => x.ItineraryId);
    }
}