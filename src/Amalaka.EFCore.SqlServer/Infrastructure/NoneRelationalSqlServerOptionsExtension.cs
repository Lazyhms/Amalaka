using Amalaka.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Infrastructure;

public class NoneRelationalSqlServerOptionsExtension : NoneRelationalOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public NoneRelationalSqlServerOptionsExtension()
    {
    }

    public NoneRelationalSqlServerOptionsExtension(NoneRelationalSqlServerOptionsExtension copyFrom) : base(copyFrom)
    {
    }

    public override DbContextOptionsExtensionInfo Info
        => _info ??= new SqlServerExtensionInfo(this);

    protected override NoneRelationalSqlServerOptionsExtension Clone()
        => new(this);

    private sealed class SqlServerExtensionInfo(IDbContextOptionsExtension extension) : ExtensionInfo(extension)
    {
        private new NoneRelationalSqlServerOptionsExtension Extension
            => (NoneRelationalSqlServerOptionsExtension)base.Extension;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is SqlServerExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Amalaka:EFCore" + nameof(NoneRelationalSqlServerDbContextOptionsBuilderExtensions.UseAmalakaSqlServer)] = "1";
    }
}
