using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripArc.Case.Data.DataContext;
using TripArc.Case.Domain.Common.Settings;
using TripArc.Common.Abstractions.Repository;
using TripArc.Common.HttpHandlers;
using TripArc.Common.Storage.Repositories;
using TripArc.Profile.Client.Profile;

namespace TripArc.Case.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionsExtensions
{
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GeneralSettings>(configuration.GetSection(nameof(GeneralSettings)));
        services.Configure<ProfileApiSettings>(configuration.GetSection(nameof(ProfileApiSettings)));
    }
        
    public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TMX")).EnableSensitiveDataLogging());
    }
        
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<CaseContext>, UnitOfWork<CaseContext>>();
    }

    public static void AddProfileClient(this IServiceCollection services, ProfileApiSettings profileApiSettings)
    {
        services.AddScoped<IProfileApiClient, ProfileApiClient>();
        services.AddScoped<DefaultHttpMessageHandler>();
        services.AddHttpClient<IProfileApiClient, ProfileApiClient>(x => x.BaseAddress = new Uri(profileApiSettings.ApiUrl))
            .AddHttpMessageHandler<DefaultHttpMessageHandler>();        
    }
}