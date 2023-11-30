using Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

public static class NoneRelationalMySqlDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaMySql(this DbContextOptionsBuilder optionsBuilder, Action<NoneRelationalMySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        optionsBuilder.AddOrUpdateExtension<NoneRelationalMySqOptionsExtension>();
        mySqlOptionsAction?.Invoke(new NoneRelationalMySqlDbContextOptionsBuilder(optionsBuilder));
        return optionsBuilder;
    }
}
