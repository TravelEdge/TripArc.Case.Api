namespace TripArc.Case.Data.CaseAction.EntityMappings;

public class CaseActionMapping : IEntityTypeConfiguration<Domain.CaseAction.Entities.CaseAction>
{
    public void Configure(EntityTypeBuilder<Domain.CaseAction.Entities.CaseAction> builder)
    {
        builder.ToTable("CaseActions");

        builder.HasKey(x => x.CaseActionId);
        
        builder.HasIndex(x => x.CaseId, "IX_CaseActions_CaseId");
        
        builder.HasOne(ca => ca.Action)
            .WithMany(a => a.CaseActions)
            .HasForeignKey(ca => ca.ActionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseActions_Actions");
        
        builder.HasOne(ca => ca.Case)
            .WithMany(c => c.CaseActions)
            .HasForeignKey(ca => ca.CaseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseActions_Cases");        
    }
}