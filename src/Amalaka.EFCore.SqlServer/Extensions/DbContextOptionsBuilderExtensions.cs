using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.EntityFrameworkCore.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder WithMigrationNoneForeignKey(this DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        dbContextOptionsBuilder.ReplaceService<IMigrationsSqlGenerator, NoneRelationalSqlServerMigrationsSqlGenerator>();
        return dbContextOptionsBuilder;
    }
}
