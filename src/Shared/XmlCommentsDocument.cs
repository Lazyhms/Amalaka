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

    public string? GetMemberNameForFieldOrProperty(MemberInfo memberInfo)
    {
        var memberXPath = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(memberInfo);
        var xPathNavigator = _xmlNavigator.SelectSingleNode(string.Format(SummaryXPath, memberXPath));
        return XmlCommentsTextHelper.Humanize(xPathNavigator?.Value);
    }
}