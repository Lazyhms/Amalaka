using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Infrastructure;

public class NoneRelationalMySqlOptionsExtension : NoneRelationalOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public NoneRelationalMySqlOptionsExtension()
    {
    }

    public NoneRelationalMySqlOptionsExtension(NoneRelationalMySqlOptionsExtension copyFrom) : base(copyFrom)
    {
    }

    public override DbContextOptionsExtensionInfo Info
        => _info ??= new MySqlExtensionInfo(this);

    protected override NoneRelationalMySqlOptionsExtension Clone()
        => new(this);

    private sealed class MySqlExtensionInfo(IDbContextOptionsExtension extension) : ExtensionInfo(extension)
    {
        private new NoneRelationalMySqlOptionsExtension Extension
            => (NoneRelationalMySqlOptionsExtension)base.Extension;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is MySqlExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Amalaka:EFCore" + nameof(NoneRelationalMySqlDbContextOptionsBuilderExtensions.UseAmalakaMySql)] = "1";
    }
}
