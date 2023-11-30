using Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

public static class NoneRelationalSqlServerDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaSqlServer(this DbContextOptionsBuilder optionsBuilder, Action<NoneRelationalSqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
    {
        optionsBuilder.AddOrUpdateExtension<NoneRelationalSqlServerOptionsExtension>();
        sqlServerOptionsAction?.Invoke(new NoneRelationalSqlServerDbContextOptionsBuilder(optionsBuilder));
        return optionsBuilder;
    }
}
