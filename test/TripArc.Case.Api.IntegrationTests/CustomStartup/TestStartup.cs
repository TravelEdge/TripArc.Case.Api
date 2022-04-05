using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripArc.Case.Data.DataContext;

namespace TripArc.Case.Api.IntegrationTests.CustomStartup
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
        
        protected override void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddDbContext<CaseContext>(options =>
                options.UseInMemoryDatabase("TestDb")
                    .ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        }        
    }
}