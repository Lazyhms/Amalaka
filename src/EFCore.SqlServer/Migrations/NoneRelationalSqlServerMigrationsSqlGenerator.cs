using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;

namespace Microsoft.EntityFrameworkCore.Migrations;

public class NoneRelationalSqlServerMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer)
        : SqlServerMigrationsSqlGenerator(dependencies, commandBatchPreparer)
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