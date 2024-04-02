namespace Microsoft.EntityFrameworkCore;

public static class NoneRelationalMySqlDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaMySql(this DbContextOptionsBuilder optionsBuilder, Action<NoneRelationalMySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        optionsBuilder.AddOrUpdateExtension(optionsBuilder.GetOrCreateExtension<NoneRelationalMySqlOptionsExtension>());
        mySqlOptionsAction?.Invoke(new NoneRelationalMySqlDbContextOptionsBuilder(optionsBuilder));

        return optionsBuilder;
    }
}
