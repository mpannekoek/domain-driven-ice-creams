namespace IceCream.Sales.Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly SalesContext _salesContext;

        public BuyerRepository(SalesContext salesContext)
        {
            _salesContext = salesContext;
        }

        public IUnitOfWork UnitOfWork => _salesContext;

        public Buyer Add(Buyer buyer)
        {
            return _salesContext
                .Buyers
                .Add(buyer)
                .Entity;
        }

        public Task<Buyer[]> ListAsync(CancellationToken cancellationToken = default)
        {
            return _salesContext
                .Buyers
                .ToArrayAsync(cancellationToken);
        }
    }
}
