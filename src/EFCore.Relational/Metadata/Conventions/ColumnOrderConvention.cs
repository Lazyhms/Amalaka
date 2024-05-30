namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

public class ColumnOrderConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var conventionEntityTypes in modelBuilder.Metadata.GetEntityTypes())
        {
            var columnOrder = 15;
            foreach (var conventionProperty in conventionEntityTypes.GetProperties())
            {
                if (!conventionProperty.GetColumnOrder().HasValue)
                {
                    conventionProperty.SetColumnOrder(columnOrder++);
                }
            }
        }
    }
}
