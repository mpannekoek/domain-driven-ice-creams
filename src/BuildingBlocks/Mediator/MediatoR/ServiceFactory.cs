namespace MediatoR;

public delegate object ServiceFactory(Type serviceType);

public static class ServiceFactoryExtensions
{
    public static IEnumerable<T> GetInstances<T>(this ServiceFactory factory)
        => (IEnumerable<T>)factory(typeof(IEnumerable<T>));
}
