namespace E_Learning
{
    public static class Extensions
    {
        public static void RegisterAllTypes(this IServiceCollection services, Type type, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(a => !a.IsAbstract && !a.IsInterface && a.GetInterfaces().Any(x => x.Name.Contains(type.Name)))
                .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
                .ToList()
                .ForEach(typesToRegister =>
                {
                    typesToRegister.serviceTypes.ForEach(typeToRegister =>
                    {
                        services.Add(new ServiceDescriptor(typeToRegister, typesToRegister.assignedType, lifetime));
                    });
                });
        }

        public static void RegisterAllDerivedTypes(this IServiceCollection services, Type type, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(a => !a.IsAbstract && !a.IsInterface && a.BaseType?.FullName == type.FullName)
                .ToList()
                .ForEach(typeToRegister =>
                {
                    services.Add(new ServiceDescriptor(typeToRegister, typeToRegister, lifetime));
                });
        }
    }
}