namespace IceCream.Sales.Domain.Repositories
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);

        Task<Buyer[]> ListAsync(CancellationToken cancellationToken = default);
    }
}
