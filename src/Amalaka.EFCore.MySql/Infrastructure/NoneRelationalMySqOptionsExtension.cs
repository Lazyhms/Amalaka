using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Infrastructure;

public class NoneRelationalMySqOptionsExtension : NoneRelationalOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public NoneRelationalMySqOptionsExtension()
    {
    }

    public NoneRelationalMySqOptionsExtension(NoneRelationalMySqOptionsExtension copyFrom) : base(copyFrom)
    {
    }

    public override DbContextOptionsExtensionInfo Info
        => _info ??= new MySqlExtensionInfo(this);

    protected override NoneRelationalMySqOptionsExtension Clone()
        => new(this);

    private sealed class MySqlExtensionInfo(IDbContextOptionsExtension extension) : ExtensionInfo(extension)
    {
        private new NoneRelationalMySqOptionsExtension Extension
            => (NoneRelationalMySqOptionsExtension)base.Extension;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is MySqlExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Amalaka:EFCore" + nameof(NoneRelationalMySqlDbContextOptionsBuilderExtensions.UseAmalakaMySql)] = "1";
    }
}
