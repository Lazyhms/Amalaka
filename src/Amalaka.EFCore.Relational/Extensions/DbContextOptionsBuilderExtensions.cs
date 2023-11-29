using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder AddOrUpdateExtension<TExtension>(this DbContextOptionsBuilder optionsBuilder) where TExtension : DbContextOptionsExtension, new()
    {
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(GetOrCreateExtension<TExtension>(optionsBuilder));

        return optionsBuilder;
    }

    private static TExtension GetOrCreateExtension<TExtension>(DbContextOptionsBuilder optionsBuilder) where TExtension : DbContextOptionsExtension, new()
        => optionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension();
}
