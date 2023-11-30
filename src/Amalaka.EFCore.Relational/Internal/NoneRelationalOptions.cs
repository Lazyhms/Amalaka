using Amalaka.EntityFrameworkCore.Infrastructure;
using Amalaka.EntityFrameworkCore.Infrastructure.Internal;

namespace Amalaka.EntityFrameworkCore.Internal;

public class NoneRelationalOptions : INoneRelationalOptions
{
    public bool NoneForeignKey { get; set; }

    public NoneRelationalOptions()
    {
        NoneForeignKey = true;
    }

    public void Initialize(IDbContextOptions options)
    {
        var optionsExtension = (NoneRelationalOptionsExtension?)options.Extensions.FirstOrDefault(f => f is NoneRelationalOptionsExtension) ?? new NoneRelationalOptionsExtension();
        NoneForeignKey = optionsExtension!.NoneForeignKey;
    }

    public void Validate(IDbContextOptions options)
    {
        var optionsExtension = (NoneRelationalOptionsExtension?)options.Extensions.FirstOrDefault(f => f is NoneRelationalOptionsExtension) ?? new NoneRelationalOptionsExtension();
        if (!Equals(NoneForeignKey, optionsExtension!.NoneForeignKey))
        {

        }
    }
}