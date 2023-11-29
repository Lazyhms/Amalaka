using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

public class MySqlDbContextOptionsExtension : DbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public override DbContextOptionsExtensionInfo Info
        => _info ??= new MySqlExtensionInfo(this);

    private sealed class MySqlExtensionInfo(IDbContextOptionsExtension extension) : ExtensionInfo(extension)
    {
        private new DbContextOptionsExtension Extension
            => (MySqlDbContextOptionsExtension)base.Extension;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is MySqlExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Amalaka:EFCore" + nameof(MySqlDbContextOptionsBuilderExtensions.UseAmalakaMySql)] = "1";
    }
}
