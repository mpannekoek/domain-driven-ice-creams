namespace IceCream.Sales.Application.Application;

public class CreateBuyerCommand
{
    private readonly IBuyerRepository _buyerRepository;

    public CreateBuyerCommand(IBuyerRepository buyerRepository)
    {
        _buyerRepository = buyerRepository;
    }

    public async Task Execute()
    {
        var buyer = Buyer.Create(Guid.NewGuid());

        _buyerRepository.Add(buyer);

        await _buyerRepository.UnitOfWork.SaveEntitiesAsync();

        var buyers = await _buyerRepository.ListAsync();
    }
}
