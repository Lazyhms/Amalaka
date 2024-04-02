namespace Microsoft.EntityFrameworkCore.Query.Internal;

public class AggregateMethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory) : IAggregateMethodCallTranslatorPlugin
{
    public ISqlExpressionFactory SqlExpressionFactory => sqlExpressionFactory;

    public IEnumerable<IAggregateMethodCallTranslator> Translators => [];
}
