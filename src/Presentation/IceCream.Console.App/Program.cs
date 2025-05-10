// See https://aka.ms/new-console-template for more information

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilderContext, services) =>
        services
            .AddSalesApplication()
            .AddSalesInfrastructure(hostBuilderContext.Configuration, Assembly.LoadFrom("IceCream.Sales.Infrastructure.dll")))
    .Build();

var createBuyerCommand = host.Services.GetRequiredService<CreateBuyerCommand>();

await createBuyerCommand.Execute();