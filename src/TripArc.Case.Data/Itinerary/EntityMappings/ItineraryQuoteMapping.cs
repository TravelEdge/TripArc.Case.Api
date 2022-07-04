using TripArc.Case.Domain.ItineraryQuote.Entities;

namespace TripArc.Case.Data.Itinerary.EntityMappings;

public class ItineraryQuoteMapping : IEntityTypeConfiguration<ItineraryQuote>
{
    public void Configure(EntityTypeBuilder<ItineraryQuote> builder)
    {
        builder.ToTable("ItineraryQuote");
        
        builder.HasKey(x => x.ItineraryQuoteId)
            .HasName("PK_ItineraryQuote_1")
            .IsClustered();

        builder.Property(x => x.QuotedPrice)
            .HasDefaultValueSql("((0))");
        
        builder.Property(x => x.DateCreated)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");
        
        builder.Property(x => x.LastModified)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.HasOne(iq => iq.Itinerary)
            .WithOne(i => i.ItineraryQuote)
            .HasForeignKey<ItineraryQuote>(iq => iq.ItineraryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ItineraryQuote_Itinerary");        
    }
}