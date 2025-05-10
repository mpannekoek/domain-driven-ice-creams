namespace IceCream.Sales.Infrastructure.Startup;

public class SalesInfrastructureEntry : ISalesInfrastructureEntry
{
    public void AddSalesInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SalesContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SalesDb");

            options.UseSqlite(connectionString);
        });

        services.AddTransient<IBuyerRepository, BuyerRepository>();
    }
}
