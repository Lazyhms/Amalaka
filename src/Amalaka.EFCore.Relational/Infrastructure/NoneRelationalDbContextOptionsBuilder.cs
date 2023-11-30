namespace Amalaka.EntityFrameworkCore.Infrastructure;

public abstract class NoneRelationalDbContextOptionsBuilder<TBuilder, TExtension>(DbContextOptionsBuilder optionsBuilder) : IRelationalDbContextOptionsBuilderInfrastructure
    where TBuilder : NoneRelationalDbContextOptionsBuilder<TBuilder, TExtension>
    where TExtension : NoneRelationalOptionsExtension, new()
{
    public DbContextOptionsBuilder OptionsBuilder { get; } = optionsBuilder;

    DbContextOptionsBuilder IRelationalDbContextOptionsBuilderInfrastructure.OptionsBuilder => OptionsBuilder;

    public virtual TBuilder WithNoneForeignKey()
    {
        OptionsBuilder.Options.FindExtension<CoreOptionsExtension>();

        return WithOption(e => (TExtension)e.WithNoneForeignKey());
    }

    protected virtual TBuilder WithOption(Func<TExtension, TExtension> setAction)
    {
        ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(setAction(OptionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension()));
        return (TBuilder)this;
    }

    #region Hidden System.Object members

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals(object? obj)
        => base.Equals(obj);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode()
        => base.GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string? ToString()
        => base.ToString();

    #endregion
}
