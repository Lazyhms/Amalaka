namespace Microsoft.EntityFrameworkCore.Query.Internal;

public class MethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory) : IMethodCallTranslatorPlugin
{
    public IEnumerable<IMethodCallTranslator> Translators => [];
}
