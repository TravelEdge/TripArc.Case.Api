using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TripArc.Common.Abstractions.Entity;
using TripArc.Common.Storage.Extensions;

namespace TripArc.Case.Data.DataContext
{
    [ExcludeFromCodeCoverage]
    public class CaseContext : DbContext
    {
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