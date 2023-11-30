using Amalaka.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Amalaka.EntityFrameworkCore.Infrastructure;

public class NoneRelationalOptionsExtension : IDbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public NoneRelationalOptionsExtension()
    {
        NoneForeignKey = false;
    }

    protected NoneRelationalOptionsExtension(NoneRelationalOptionsExtension copyFrom)
    {
        NoneForeignKey = copyFrom.NoneForeignKey;
    }

    public virtual DbContextOptionsExtensionInfo Info
        => _info ??= new ExtensionInfo(this);

    protected virtual NoneRelationalOptionsExtension Clone()
    {
        return new NoneRelationalOptionsExtension(this);
    }

    public bool NoneForeignKey { get; private set; }

    public NoneRelationalOptionsExtension WithNoneForeignKey()
    {
        var clone = Clone();
        clone.NoneForeignKey = true;
        return clone;
    }

    public virtual void ApplyServices(IServiceCollection services)
    {
        services.AddEntityFrameworkCoreServices();
    }

    public virtual void Validate(IDbContextOptions options)
    {
    }

    protected class ExtensionInfo(IDbContextOptionsExtension extension) : DbContextOptionsExtensionInfo(extension)
    {
        public override bool IsDatabaseProvider
            => false;

        public override int GetServiceProviderHashCode()
            => 0;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is ExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["EFCore:UseAmalaka"] = "1";

        public override string LogFragment
            => "using Amalaka.EFCore ";
    }
}
