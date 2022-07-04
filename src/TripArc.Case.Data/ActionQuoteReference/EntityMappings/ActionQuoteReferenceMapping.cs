namespace TripArc.Case.Data.ActionQuoteReference.EntityMappings;

public class ActionQuoteReferenceMapping : IEntityTypeConfiguration<Domain.ActionQuoteReference.Entities.ActionQuoteReference>
{
    public void Configure(EntityTypeBuilder<Domain.ActionQuoteReference.Entities.ActionQuoteReference> builder)
    {
        builder.ToTable("ActionQuoteReferences");

        builder.HasKey(x => x.ActionQuoteReferenceId);

        builder.Property(x => x.ActionQuoteReferenceId)
            .HasColumnName("ActionQuoteReferencesId");
            
        builder.Property(x => x.DateCreated)
            .HasDefaultValueSql("(getdate())");
        
        builder.Property(x => x.LastModified)
            .HasDefaultValueSql("(getdate())");        
        
        builder.HasOne(aqf => aqf.Action)
            .WithMany(a => a.ActionQuoteReferences)
            .HasForeignKey(aqf => aqf.ActionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__ActionQuo__Actio__3E7703A6");
        
        builder.HasOne(aqf => aqf.ItineraryQuote)
            .WithMany(iq => iq.ActionQuoteReferences)
            .HasForeignKey(aqf => aqf.ItineraryQuoteId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ActionQuoteReferences_ItineraryQuote");                
    }
}