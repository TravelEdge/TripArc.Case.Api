using System.Diagnostics.CodeAnalysis;
using TripArc.Case.Domain.Action.Entities;
using TripArc.Case.Domain.Case.Entities;
using TripArc.Case.Domain.Itinerary.Entities;
using TripArc.Common.Abstractions.Entity;
using TripArc.Common.Storage.Extensions;

namespace TripArc.Case.Data.DataContext;

[ExcludeFromCodeCoverage]
public class CaseContext : DbContext
{
    public virtual DbSet<Actions> Actions { get; set; }
    public virtual DbSet<ActionQuoteReference> ActionQuoteReferences { get; set; }
    public virtual DbSet<Domain.Case.Entities.Case> Cases { get; set; }
    public virtual DbSet<CaseActions> CaseActions { get; set; }
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
        modelBuilder.ApplyQueryFilterToEntitiesCompatibleWith<ISoftDeleteEntity>(x => !x.Deleted);
            
        base.OnModelCreating(modelBuilder);
    }
}