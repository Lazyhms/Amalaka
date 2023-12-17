namespace Microsoft.EntityFrameworkCore.Query.Internal;

public class AggregateMethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory) : IAggregateMethodCallTranslatorPlugin
{
    public IEnumerable<IAggregateMethodCallTranslator> Translators => [];
}
