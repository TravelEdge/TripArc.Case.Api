using System.Diagnostics.CodeAnalysis;
using TripArc.Case.Domain.ItineraryQuote.Entities;

namespace TripArc.Case.Data.DataContext;

[ExcludeFromCodeCoverage]
public class CaseContext : DbContext
{
    public virtual DbSet<Domain.Action.Entities.Action> Actions { get; set; }
    public virtual DbSet<Domain.ActionQuoteReference.Entities.ActionQuoteReference> ActionQuoteReferences { get; set; }
    public virtual DbSet<Domain.Case.Entities.Case> Cases { get; set; }
    public virtual DbSet<Domain.CaseAction.Entities.CaseAction> CaseActions { get; set; }
    public virtual DbSet<Domain.CaseProfile.Entities.CaseProfile> CaseProfiles { get; set; }
    public virtual DbSet<Domain.Trip.Entities.Trip> Trips { get; set; }
    public virtual DbSet<Domain.ProfileAction.Entities.ProfileAction> ProfileActions { get; set; }
    
    public virtual DbSet<Domain.Itinerary.Entities.Itinerary> Itineraries { get; set; }
    public virtual DbSet<ItineraryQuote> ItineraryQuotes { get; set; }

    public CaseContext()
    {
    }

    public CaseContext(DbContextOptions<CaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}