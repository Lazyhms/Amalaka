using Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

public static class MySqlDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaMySql(this DbContextOptionsBuilder optionsBuilder, Action<MySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        optionsBuilder.AddOrUpdateExtension<MySqlDbContextOptionsExtension>();

        mySqlOptionsAction?.Invoke(new MySqlDbContextOptionsBuilder(optionsBuilder));

        return optionsBuilder;
    }
}
