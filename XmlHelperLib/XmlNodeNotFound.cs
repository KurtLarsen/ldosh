using System;
using System.Xml;

namespace XmlHelperLib{
public class XmlNodeNotFound : Exception{
    // ReSharper disable once UnusedMember.Global
    public XmlNodeNotFound(string missingNodeName) :
        base(TextMessage(missingNodeName, null, null)){ }

    public XmlNodeNotFound(string missingNodeName, XmlNode parentNode) :
        base(TextMessage(missingNodeName, parentNode, null)){ }

    // ReSharper disable once UnusedMember.Global
    public XmlNodeNotFound(string missingNodeName, string filename) :
        base(TextMessage(missingNodeName, null, filename)){ }

    // ReSharper disable once UnusedMember.Global
    public XmlNodeNotFound(string missingNodeName, XmlNode parentNode, string filename) :
        base(TextMessage(missingNodeName, parentNode, filename)){ }

    private static string TextMessage(string missingNodeName, XmlNode parentNode, string filename){
        var s = $"Not found: <{missingNodeName}>";
        while (parentNode != null){
            s += $" in {ToText(parentNode)}";
            parentNode = parentNode.ParentNode;
        }

        if (filename != null) s += $" in file {filename}";

        return s;
    }

    private static string ToText(XmlNode node){
        var a = node.OuterXml.Split('>');
        return a[0] + '>';
    }
}
}