namespace IceCream.SharedKernel.Domain;

public interface IUnitOfWork
{
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
