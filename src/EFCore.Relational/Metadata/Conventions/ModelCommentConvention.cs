using System.Xml.XPath;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

internal sealed class ModelCommentConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var conventionEntityType in modelBuilder.Metadata.GetEntityTypes())
        {
            var conventionEntityTypeBuilder = modelBuilder.Entity(conventionEntityType.Name);

            if (null == conventionEntityTypeBuilder)
            {
                return;
            }

            foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
            {
                using var stream = File.OpenRead(xmlFile);
                var xmlCommentsDocument = new XmlCommentsDocument(new XPathDocument(stream));

                if (string.IsNullOrWhiteSpace(conventionEntityType.GetComment()))
                {
                    conventionEntityTypeBuilder.HasComment(xmlCommentsDocument.GetMemberNameForType(conventionEntityTypeBuilder.Metadata.ClrType));
                }

                foreach (var conventionProperty in conventionEntityTypeBuilder.Metadata.GetProperties().Where(w => !w.IsShadowProperty()))
                {
                    if (string.IsNullOrWhiteSpace(conventionProperty.GetComment()))
                    {
                        conventionProperty.SetComment(xmlCommentsDocument.GetMemberNameForFieldOrProperty(conventionProperty.PropertyInfo!));
                    }
                }
            }
        }
    }
}
