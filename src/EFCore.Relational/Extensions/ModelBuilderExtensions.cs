namespace Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyEntityFromAssembly(this ModelBuilder builder, string assemblyName)
        => builder.ApplyEntityFromAssembly(Assembly.Load(assemblyName));

    public static ModelBuilder ApplyEntityFromAssembly(this ModelBuilder builder, Assembly assembly)
    {
        return builder.ApplyEntityFromAssembly(assembly, w => w.IsDefined<DbEntityAttribute>());
    }

    public static ModelBuilder ApplyEntityFromAssembly(this ModelBuilder builder, Assembly assembly, Func<Type, bool> predicate)
    {
        foreach (var type in assembly.GetTypes().Where(predicate.Invoke))
        {
            if (!type.IsClass || type.BaseType == typeof(object))
            {
                continue;
            }
            builder.Entity(type);
        }
        return builder;
    }
}