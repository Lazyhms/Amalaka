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
        foreach (var item in columns)
        {
            var column = operation.Columns.FirstOrDefault(f => f.Name == item.Name);
            column?.AddAnnotation(RelationalAnnotationNames.ColumnOrder, item.Order ?? 2);
            if (column != null)
            {
                addColumnOperations.Add(column);
            }
        }

        operation.Columns.Clear();
        operation.Columns.AddRange(addColumnOperations.OrderBy(o => o.GetAnnotation(RelationalAnnotationNames.ColumnOrder).Value).ThenBy(o => o.Name));

        base.CreateTableColumns(operation, model, builder);
    }

    protected override void CreateTableForeignKeys(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
    {
        operation.ForeignKeys.Clear();
        base.CreateTableForeignKeys(operation, model, builder);
    }
}