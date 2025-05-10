namespace MediatoR;

public class Mediator : IMediator
{
    private static readonly ConcurrentDictionary<Type, NotificationHandlerWrapper> _notificationHandlers = new();

    private readonly ServiceFactory _serviceFactory;

    public Mediator(ServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        return PublishNotification(notification, cancellationToken);
    }

    private Task PublishNotification(INotification notification, CancellationToken cancellationToken)
    {
        var notificationType = notification.GetType();        

        var handler = _notificationHandlers.GetOrAdd(notificationType, type =>
        {
            var handlerWrapper = Activator.CreateInstance(typeof(NotificationHandlerWrapperImpl<>).MakeGenericType(type));

            if (handlerWrapper is NotificationHandlerWrapper notificationHandlerWrapper)
            {
                return notificationHandlerWrapper;
            }

            throw new InvalidOperationException($"Could not create wrapper for type {type}");
        });

        return handler.Handle(notification, _serviceFactory, PublishCore, cancellationToken);
    }

    private async Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, 
        INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in allHandlers)
        {
            await handler(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
