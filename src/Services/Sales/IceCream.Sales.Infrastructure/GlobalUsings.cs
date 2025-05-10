global using System.Text.Json;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using IceCream.SharedKernel.Domain;

global using IceCream.Sales.Application.Startup;
global using IceCream.Sales.Domain.Repositories;
global using IceCream.Sales.Domain.Entities.BuyerAggregate;
global using IceCream.Sales.Infrastructure.Repositories;
global using IceCream.Sales.Infrastructure.DbContexts;
global using IceCream.Sales.Infrastructure.DbContexts.EntityTypeConfigurations;

global using MediatoR;