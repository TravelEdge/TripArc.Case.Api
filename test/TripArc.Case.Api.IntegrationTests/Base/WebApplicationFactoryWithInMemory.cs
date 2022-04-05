using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TripArc.Case.Api.IntegrationTests.CustomStartup;
using TripArc.Common.Test.IntegrationTest.Base;
using TripArc.Common.Test.IntegrationTest.Fakers;

namespace TripArc.Case.Api.IntegrationTests.Base
{
    /// <summary>
    /// Implementation based on <see href="https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0"/>
    /// </summary>
    public class WebApplicationFactoryWithInMemory : BaseWebApplicationFactory<TestStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseSolutionRelativeContentRoot("")
                .ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
    
                    services
                        .AddAuthentication(FakeSchemeSettings.Name)
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(FakeSchemeSettings.Name, _ => { });
                });
        }
    }
}