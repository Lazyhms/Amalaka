using Amalaka.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

public class NoneRelationalSqlServerDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) :
    NoneRelationalDbContextOptionsBuilder<NoneRelationalSqlServerDbContextOptionsBuilder, NoneRelationalSqlServerOptionsExtension>(optionsBuilder)
{
    public NoneRelationalSqlServerDbContextOptionsBuilder WithMigrationNoneForeignKey()
    {
        OptionsBuilder.ReplaceService<IMigrationsSqlGenerator, NoneRelationalSqlServerMigrationsSqlGenerator>();
        return this;
    }
}