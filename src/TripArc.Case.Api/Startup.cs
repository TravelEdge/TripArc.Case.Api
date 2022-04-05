using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using TripArc.Case.Api.Case.QueryHandlers;
using TripArc.Case.Api.Extensions;
using TripArc.Case.Data.Case.AutoMapperProfiles;
using TripArc.Case.Data.Case.Repositories;
using TripArc.Common.Base.ActionFilters;
using TripArc.Common.CQRS;
using TripArc.Common.Extensions;
using TripArc.Common.Storage.Repositories;

namespace TripArc.Case.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDefaultServices(services);
            ConfigureDatabaseServices(services);
            ConfigureRepositories(services);
            ConfigureUnitOfWork(services);
            ConfigureCommonServices(services);

            services.AddAutoMapper(typeof(CaseMapperProfile).Assembly);
            services.RegisterCqrsDependencies(Configuration)
                .RegisterApplicationQueries<CaseSearchByIdQueryHandler>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TripArc.Case.Api", Version = "v1" });
            });
        }
        
        protected virtual void ConfigureUnitOfWork(IServiceCollection services)
        {
            services.AddUnitOfWork();
        }
        
        private void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddSettings(Configuration);
            services.AddAuthentication(Configuration);
            services.AddControllers(op => op.Filters.Add<BaseActionFilter>())
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>())
                .ConfigureApiBehaviorOptions(op => op.SuppressModelStateInvalidFilter = true);
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TripArc.Case.Api", Version = "v1"});
            });
            services.AddApiVersioning();
        }        

        protected virtual void ConfigureCommonServices(IServiceCollection services)
        {
            services.ConfigureCommonServices();
        }
        
        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddDataContext(Configuration);
        }
        
        protected void ConfigureRepositories(IServiceCollection services)
        {
            services.AddApplicationRepositories<CaseRepository>(typeof(Repository<>));
        }        
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TripArc.Case.Api v1"));
            }
            
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
