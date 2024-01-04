namespace Microsoft.EntityFrameworkCore;

public static class NoneRelationalMySqlDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaMySql(this DbContextOptionsBuilder optionsBuilder, Action<NoneRelationalMySqlDbContextOptionsBuilder>? mySqlOptionsAction = null)
    {
        var extension = optionsBuilder.GetOrCreateExtension<NoneRelationalMySqlOptionsExtension>();
        optionsBuilder.AddOrUpdateExtension(extension);
        mySqlOptionsAction?.Invoke(new NoneRelationalMySqlDbContextOptionsBuilder(optionsBuilder));
        return optionsBuilder;
    }
}
