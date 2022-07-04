namespace TripArc.Case.Data.Action.EntityMappings;

public class ActionMapping : IEntityTypeConfiguration<Domain.Action.Entities.Action>
{
    public void Configure(EntityTypeBuilder<Domain.Action.Entities.Action> builder)
    {
        builder.ToTable("Actions");

        builder.HasKey(x => x.ActionId);

        builder.Property(x => x.Name)
            .HasColumnName("Action");
    }
}