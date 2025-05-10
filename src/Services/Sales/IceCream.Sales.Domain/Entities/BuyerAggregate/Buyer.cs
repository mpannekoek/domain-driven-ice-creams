namespace IceCream.Sales.Domain.Entities.BuyerAggregate;

public sealed class Buyer : AggregateRoot<Guid>
{
    private Buyer(Guid id) : base(id)
    {
    }

    public static Buyer Create(Guid id)
    {
        var buyer = new Buyer(id);

        buyer.AddDomainEvent(new BuyerCreatedDomainEvent(buyer));

        return buyer;
    }
}
