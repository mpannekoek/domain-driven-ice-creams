namespace IceCream.Sales.Infrastructure.DbContexts;

public class SalesContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public SalesContext(DbContextOptions<SalesContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        System.Diagnostics.Debug.WriteLine($"{nameof(SalesContext)}::ctor ->{GetHashCode()}");
    }

    public DbSet<Buyer> Buyers => Set<Buyer>();

    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var events = ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.DomainEvents;

                aggregateRoot.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in events)
        {
            //_mediator.Publish(domainEvent, cancellationToken);
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

public class DesignTimeSalesDbContextFactory : IDesignTimeDbContextFactory<SalesContext>
{
    public SalesContext CreateDbContext(string[] args)
    {
        if (args == null || args.Length != 1)
        {
            throw new InvalidOperationException("path to appsettings.json not found in the arguments");
        }

        var appsettingsPath = args.Single();

        if (!File.Exists(appsettingsPath))
        {
            throw new InvalidOperationException($"path to appsettings.json does not exists: {appsettingsPath}");
        }

        if (!appsettingsPath.EndsWith("appsettings.json"))
        {
            throw new InvalidOperationException($"path to appsettings.json is not valid: {appsettingsPath}");
        }

        string? connectionStringSalesDb;

        try
        {
            using var fileStream = File.Open(appsettingsPath, FileMode.Open, FileAccess.Read);

            var json = JsonSerializer.Deserialize<JsonElement>(fileStream);

            var connectionStrings = json.GetProperty("ConnectionStrings");

            connectionStringSalesDb = connectionStrings
                .GetProperty("SalesDb")
                .GetString();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("SalesDb not found in the ConnectionStrings", ex);
        }

        if (string.IsNullOrWhiteSpace(connectionStringSalesDb))
        {
            throw new InvalidOperationException("ConnectionString for SalesDb is not set");
        }

        var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();

        optionsBuilder.UseSqlite(connectionStringSalesDb);

        return new SalesContext(optionsBuilder.Options, new NoMediator());
    }

    private class NoMediator : IMediator
    {
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
            where TNotification : INotification
        {
            return Task.CompletedTask;
        }
    }
}
