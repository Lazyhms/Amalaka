namespace Amalaka.EntityFrameworkCore.Infrastructure.Internal;

public interface INoneRelationalOptions : ISingletonOptions
{
    public bool NoneForeignKey { get; set; }
}
