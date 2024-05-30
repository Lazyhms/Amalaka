namespace Microsoft.EntityFrameworkCore.Infrastructure;

public abstract class NoneRelationalDbContextOptionsBuilder<TBuilder, TExtension>(DbContextOptionsBuilder optionsBuilder) : IRelationalDbContextOptionsBuilderInfrastructure
    where TBuilder : NoneRelationalDbContextOptionsBuilder<TBuilder, TExtension>
    where TExtension : NoneRelationalOptionsExtension, new()
{
    protected virtual DbContextOptionsBuilder OptionsBuilder { get; } = optionsBuilder;

    DbContextOptionsBuilder IRelationalDbContextOptionsBuilderInfrastructure.OptionsBuilder => OptionsBuilder;

    public virtual TBuilder WithNoneForeignKey()
        => WithOption(e => (TExtension)e.WithNoneForeignKey());

    public virtual TBuilder UseSoftDelete(string? columnName = null, string? comment = null)
        => WithOption(e => (TExtension)e.UseSoftDelete(columnName, comment));

    protected virtual TBuilder WithOption(Func<TExtension, TExtension> setAction)
    {
        var extension = setAction(OptionsBuilder.GetOrCreateExtension<TExtension>());
        OptionsBuilder.AddOrUpdateExtension(extension);
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
