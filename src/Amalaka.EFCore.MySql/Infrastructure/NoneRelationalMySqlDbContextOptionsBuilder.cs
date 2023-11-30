using Amalaka.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Amalaka.EntityFrameworkCore.SqlServer.Infrastructure;

public class NoneRelationalMySqlDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
    : NoneRelationalDbContextOptionsBuilder<NoneRelationalMySqlDbContextOptionsBuilder, NoneRelationalMySqOptionsExtension>(optionsBuilder)
{
    public NoneRelationalMySqlDbContextOptionsBuilder WithMigrationNoneForeignKey()
    {
        OptionsBuilder.ReplaceService<IMigrationsSqlGenerator, NoneRelationalMySqlMigrationsSqlGenerator>();
        return this;
    }
}