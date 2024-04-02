namespace Microsoft.EntityFrameworkCore.Query.Internal;

public class MethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory) : IMethodCallTranslatorPlugin
{
    public ISqlExpressionFactory SqlExpressionFactory => sqlExpressionFactory;

    public IEnumerable<IMethodCallTranslator> Translators => [];
}
