using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

internal static class NoneRelationalDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder AddOrUpdateExtension<TExtension>(this DbContextOptionsBuilder optionsBuilder) where TExtension : NoneRelationalOptionsExtension, new()
    {
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(GetOrCreateExtension<TExtension>(optionsBuilder));
        return optionsBuilder;
    }

    private static TExtension GetOrCreateExtension<TExtension>(DbContextOptionsBuilder optionsBuilder) where TExtension : NoneRelationalOptionsExtension, new()
        => optionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension();
}
