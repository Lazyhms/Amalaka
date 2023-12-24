namespace Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyEntityFromAssembly(this ModelBuilder builder, string assemblyName)
        => builder.ApplyEntityFromAssembly(Assembly.Load(assemblyName));

    public static ModelBuilder ApplyEntityFromAssembly(this ModelBuilder builder, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(w => w.IsDefined(typeof(DbEntityAttribute))))
        {
            if (!type.IsClass || type.BaseType == typeof(object))
            {
                continue;
            }
            var entityTypeBuilder = builder.Entity(type);
            if (type.IsDefined(typeof(SoftDeleteAttribute)))
            {
                var softDeleteAttribute = type.GetCustomAttribute<SoftDeleteAttribute>();
                entityTypeBuilder.Property(softDeleteAttribute!.ColumnName)
                                 .HasComment(softDeleteAttribute.Comment)
                                 .HasDefaultValue(false);
            }
        }
        return builder;
    }
}
