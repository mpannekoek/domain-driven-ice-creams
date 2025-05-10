namespace IceCream.Sales.Application.Startup;

public interface ISalesInfrastructureEntry
{
    void AddSalesInfrastructure(IServiceCollection serviceCollection, IConfiguration configuration);
}
