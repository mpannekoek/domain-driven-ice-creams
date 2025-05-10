namespace IceCream.Sales.Application.Startup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSalesApplication(this IServiceCollection services)
    {
        // TryAdd because maybe the IMediator is already registered in the presentation layer
        services.TryAddTransient<IMediator>(s => new Mediator(s.GetRequiredService));

        services.AddTransient<CreateBuyerCommand>();

        return services;
    }

    public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly salesInfrastructureAssembly)
    {
        var salesInfrastructureEntryType = salesInfrastructureAssembly
            .GetTypes()
            .SingleOrDefault(x => x.GetInterfaces().Any(b => b.Name == nameof(ISalesInfrastructureEntry)));

        if (salesInfrastructureEntryType is not null)
        {
            var entry = Activator.CreateInstance(salesInfrastructureEntryType) as ISalesInfrastructureEntry;

            if (entry is not null)
            {
                entry.AddSalesInfrastructure(services, configuration);
            }            
        }

        return services;
    }
}
