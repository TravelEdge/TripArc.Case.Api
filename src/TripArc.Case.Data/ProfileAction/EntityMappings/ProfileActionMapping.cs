namespace TripArc.Case.Data.ProfileAction.EntityMappings;

public class ProfileActionMapping : IEntityTypeConfiguration<Domain.ProfileAction.Entities.ProfileAction>
{
    public void Configure(EntityTypeBuilder<Domain.ProfileAction.Entities.ProfileAction> builder)
    {
        builder.ToTable("ProfileActions");

        builder.HasKey(x => x.ProfileActionId);

        builder.HasIndex(e => e.ActionId)
            .HasDatabaseName("idx_ProfileActions_ActionId");

        builder.HasIndex(e => e.ProfileId)
            .HasDatabaseName("idx_ProfileActions_ProfileId");
    }
}