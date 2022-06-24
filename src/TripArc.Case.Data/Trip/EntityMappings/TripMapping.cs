using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TripArc.Case.Data.Trip.EntityMappings;

public class TripMapping : IEntityTypeConfiguration<Domain.Trip.Entities.Trip>
{
    public void Configure(EntityTypeBuilder<Domain.Trip.Entities.Trip> builder)
    {
        builder.ToTable("Trip");
        
        builder.Property(x => x.TripName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.TripReference)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.TramsresCardNum)
            .HasColumnName("TRAMSResCardNum");

        builder.HasKey(x => x.TripId);            
    }
}