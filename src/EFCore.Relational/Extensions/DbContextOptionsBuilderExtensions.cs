using Amalaka.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.EntityFrameworkCore;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder AddOrUpdateExtension<TExtension>(this DbContextOptionsBuilder optionsBuilder, TExtension extension) where TExtension : NoneRelationalOptionsExtension, new()
    {
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
        return optionsBuilder;
    }

    public static TExtension GetOrCreateExtension<TExtension>(this DbContextOptionsBuilder optionsBuilder) where TExtension : NoneRelationalOptionsExtension, new()
        => optionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension();

    public static DbContextOptionsBuilder AddExceptionInterceptors(this DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(new SaveChangesExceptionInterceptor());
}