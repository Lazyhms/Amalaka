namespace Microsoft.EntityFrameworkCore;

public static class NoneRelationalSqlServerDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAmalakaSqlServer(this DbContextOptionsBuilder optionsBuilder, Action<NoneRelationalSqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
    {
        var extension = optionsBuilder.GetOrCreateExtension<NoneRelationalSqlServerOptionsExtension>();
        optionsBuilder.AddOrUpdateExtension(extension);
        sqlServerOptionsAction?.Invoke(new NoneRelationalSqlServerDbContextOptionsBuilder(optionsBuilder));
        return optionsBuilder;
    }
}
