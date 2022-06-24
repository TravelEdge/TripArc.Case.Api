using Entities = TripArc.Case.Domain.Case.Entities;

namespace TripArc.Case.Data.Case.EntityMappings;

public class CaseMapping : IEntityTypeConfiguration<Entities.Case>
{
    public void Configure(EntityTypeBuilder<Entities.Case> builder)
    {
        builder.ToTable("Cases");

        builder.HasKey(x => x.CaseId);

        builder.Property(x => x.ParentCaseId);

        builder.Property(x => x.CaseReference)
            .HasMaxLength(15)
            .IsUnicode(false);

        builder.Property(x => x.Name)
            .HasMaxLength(300)
            .IsUnicode(false);
            
        builder.Property(x => x.CaseType);
            
        builder.Property(x => x.CaseStatus);
            
        builder.Property(x => x.RequestedLocale);
            
        builder.Property(x => x.DateCreated)
            .HasDefaultValueSql("(getdate())");

        builder.Property(x => x.LastModified)
            .HasDefaultValueSql("(getdate())");
            
        builder.Property(x => x.Deleted)
            .HasDefaultValueSql("((0))");            

        builder.Property(x => x.TravelerPlaceholder)
            .HasColumnType("nvarchar(max)");            
            
        builder.Property(x => x.TripId);
            
        builder.HasIndex(x => x.TripId)
            .HasDatabaseName("idx_Cases_TripId");

        builder.HasIndex(x => new { x.Deleted, x.CaseReference })
            .HasDatabaseName("idx_Cases_CaseReference");

        builder.HasOne(c => c.Trip)
            .WithMany(t => t.Cases)
            .HasForeignKey(c => c.TripId)
            .HasConstraintName("FK_Cases_Trip");        
    }
}