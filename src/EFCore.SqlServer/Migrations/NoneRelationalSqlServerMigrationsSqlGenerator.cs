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