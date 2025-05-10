namespace MediatoR;

internal abstract class NotificationHandlerWrapper
{
    public abstract Task Handle(INotification notification, ServiceFactory serviceFactory, 
        Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, 
            CancellationToken, Task> publish, CancellationToken cancellationToken);
}

internal class NotificationHandlerWrapperImpl<TNotification> : NotificationHandlerWrapper
    where TNotification : INotification
{
    public override Task Handle(INotification notification, ServiceFactory serviceFactory, 
        Func<IEnumerable<Func<INotification, CancellationToken, Task>>, INotification, 
            CancellationToken, Task> publish, CancellationToken cancellationToken)
    {
        var handlers = serviceFactory
            .GetInstances<INotificationHandler<TNotification>>()
            .Select(x => new Func<INotification, CancellationToken, Task>((theNotification, theToken) => x.Handle((TNotification)theNotification, theToken)));

        return publish(handlers, notification, cancellationToken);
    }
}