namespace Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtension
{
    public static ModelBuilder RegisterEntityFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
    {
        Array.ForEach(assembly.GetTypes(), item =>
        {
            modelBuilder.Entity(item);
        });
        return modelBuilder;
    }
}