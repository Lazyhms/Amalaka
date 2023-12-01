using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.EntityFrameworkCore;

public class NoneRelationalSqlServerDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) :
    NoneRelationalDbContextOptionsBuilder<NoneRelationalSqlServerDbContextOptionsBuilder, NoneRelationalSqlServerOptionsExtension>(optionsBuilder)
{
    public NoneRelationalSqlServerDbContextOptionsBuilder WithMigrationNoneForeignKey()
    {
        OptionsBuilder.ReplaceService<IMigrationsSqlGenerator, NoneRelationalSqlServerMigrationsSqlGenerator>();
        return this;
    }
}