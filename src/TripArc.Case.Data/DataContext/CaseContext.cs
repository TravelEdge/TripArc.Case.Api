using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TripArc.Case.Domain.Action.Entities;
using TripArc.Case.Domain.Case.Entities;
using TripArc.Common.Abstractions.Entity;
using TripArc.Common.Storage.Extensions;

namespace TripArc.Case.Data.DataContext
{
    [ExcludeFromCodeCoverage]
    public class CaseContext : DbContext
    {
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Domain.Case.Entities.Case> Cases { get; set; }
        public virtual DbSet<CaseActions> CaseActions { get; set; }
        public virtual DbSet<CaseProfile> CaseProfiles { get; set; }
        public virtual DbSet<Domain.Trip.Entities.Trip> Trips { get; set; }
        
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
}