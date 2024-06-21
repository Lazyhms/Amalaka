using System.Xml.XPath;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

public sealed class ModelCommentConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
        {
            using var stream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            var xmlCommentsDocument = new XmlCommentsDocument(new XPathDocument(stream));

            foreach (var conventionEntityType in modelBuilder.Metadata.GetEntityTypes())
            {
                if (string.IsNullOrWhiteSpace(conventionEntityType.GetComment()))
                {
                    conventionEntityType.SetComment(xmlCommentsDocument.GetMemberNameForType(conventionEntityType.ClrType));
                }

                foreach (var conventionProperty in conventionEntityType.GetProperties().Where(w => !w.IsShadowProperty()))
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
