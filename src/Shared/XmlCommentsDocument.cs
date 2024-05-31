namespace System.Xml.XPath;

internal class XmlCommentsDocument(XPathDocument xmlDoc)
{
    private const string SummaryXPath = "/doc/members/member[@name='{0}']/summary";

    public readonly XPathNavigator _xmlNavigator = xmlDoc.CreateNavigator();

    public string? GetMemberNameForType(Type type)
    {
        var memberXPath = XmlCommentsNodeNameHelper.GetMemberNameForType(type);
        var xPathNavigator = _xmlNavigator.SelectSingleNode(string.Format(SummaryXPath, memberXPath));
        return XmlCommentsTextHelper.Humanize(xPathNavigator?.Value);
    }

    public string? GetMemberNameForFieldOrProperty(FieldInfo fieldInfo)
    {
        var memberXPath = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(fieldInfo);
        var xPathNavigator = _xmlNavigator.SelectSingleNode(string.Format(SummaryXPath, memberXPath));
        return XmlCommentsTextHelper.Humanize(xPathNavigator?.Value);
    }

    public string? GetMemberNameForFieldOrProperty(PropertyInfo propertyInfo)
    {
        var memberXPath = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(propertyInfo);
        var xPathNavigator = _xmlNavigator.SelectSingleNode(string.Format(SummaryXPath, memberXPath));
        return XmlCommentsTextHelper.Humanize(xPathNavigator?.Value);
    }
}