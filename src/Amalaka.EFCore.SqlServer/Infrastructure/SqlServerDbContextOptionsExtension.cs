using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

public class SqlServerDbContextOptionsExtension : DbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public override DbContextOptionsExtensionInfo Info
        => _info ??= new SqlServerExtensionInfo(this);

    private sealed class SqlServerExtensionInfo(IDbContextOptionsExtension extension) : ExtensionInfo(extension)
    {
        private new DbContextOptionsExtension Extension
            => (SqlServerDbContextOptionsExtension)base.Extension;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is SqlServerExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Amalaka:EFCore" + nameof(SqlServerDbContextOptionsBuilderExtensions.UseAmalakaSqlServer)] = "1";
    }
}
