using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.EntityFrameworkCore;

public class NoneRelationalMySqlDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
    : NoneRelationalDbContextOptionsBuilder<NoneRelationalMySqlDbContextOptionsBuilder, NoneRelationalMySqlOptionsExtension>(optionsBuilder)
{
    public NoneRelationalMySqlDbContextOptionsBuilder WithMigrationNoneForeignKey()
    {
        OptionsBuilder.ReplaceService<IMigrationsSqlGenerator, NoneRelationalMySqlMigrationsSqlGenerator>();
        return this;
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