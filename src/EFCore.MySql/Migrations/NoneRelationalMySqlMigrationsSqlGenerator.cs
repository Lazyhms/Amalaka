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
    protected override void CreateTableColumns(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
    {
        var addColumnOperations = new List<AddColumnOperation>();
        var columns = model?.GetRelationalModel().FindTable(operation.Name, operation.Schema)?.Columns ?? Enumerable.Empty<IColumn>();

        for (int i = 0; i < operation.Columns.Count; i++)
        {
            var column = columns.FirstOrDefault(f => f.Name == operation.Columns[i].Name);
            operation.Columns[i].SetAnnotation(RelationalAnnotationNames.ColumnOrder, column?.Order ?? 20 + i);
            addColumnOperations.Add(operation.Columns[i]);
        }

        operation.Columns.Clear();
        operation.Columns.AddRange(addColumnOperations.OrderBy(o => o.GetAnnotation(RelationalAnnotationNames.ColumnOrder).Value));

        base.CreateTableColumns(operation, model, builder);
    }

    protected override void CreateTableForeignKeys(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
    {
        operation.ForeignKeys.Clear();
        base.CreateTableForeignKeys(operation, model, builder);
    }
}