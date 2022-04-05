using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripArc.Case.Data.DataContext;
using TripArc.Case.Domain.Common.Settings;
using TripArc.Common.Abstractions.Repository;
using TripArc.Common.Storage.Repositories;

namespace TripArc.Case.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionsExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeneralSettings>(configuration.GetSection(nameof(GeneralSettings)));
        }
        
        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
#if DEBUG
            services.AddDbContext<CaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TMX")).EnableSensitiveDataLogging());
#else
                services.AddDbContext<ProfileContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("TMX")));
#endif
        }
        
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<CaseContext>, UnitOfWork<CaseContext>>();
        }        
    }
}