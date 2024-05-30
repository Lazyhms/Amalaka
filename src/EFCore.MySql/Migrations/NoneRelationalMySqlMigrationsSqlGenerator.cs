using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Migrations;

namespace Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable EF1001 // Internal EF Core API usage.
public class NoneRelationalMySqlMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer, IMySqlOptions options)
#pragma warning restore EF1001 // Internal EF Core API usage.
    : MySqlMigrationsSqlGenerator(dependencies, commandBatchPreparer, options)
{
    protected override void CreateTableForeignKeys(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
    {
    }
}