using Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

public static class SqlServerDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaSqlServer(this DbContextOptionsBuilder optionsBuilder, Action<SqlServerDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        optionsBuilder.AddOrUpdateExtension<SqlServerDbContextOptionsExtension>();

        mySqlOptionsAction?.Invoke(new SqlServerDbContextOptionsBuilder(optionsBuilder));

        return optionsBuilder;
    }
}
