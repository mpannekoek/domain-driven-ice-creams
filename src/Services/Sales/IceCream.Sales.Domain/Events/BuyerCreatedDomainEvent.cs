namespace IceCream.Sales.Domain.Events;

public sealed class BuyerCreatedDomainEvent : IDomainEvent
{
	internal BuyerCreatedDomainEvent(Buyer buyer) => Buyer = buyer;

	public Buyer Buyer { get; }
}
