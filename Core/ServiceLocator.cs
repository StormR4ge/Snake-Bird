public static class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        if (service == null)
            throw new ArgumentNullException(nameof(service), "Service cannot be null.");

        if (_services.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Service of type {typeof(T)} is already registered.");

        _services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Service of Type {typeof(T)} is not registered.");
        return (T) _services[typeof(T)];
    }
}
