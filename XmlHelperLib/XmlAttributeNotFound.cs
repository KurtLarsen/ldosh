using System;
using System.Xml;

namespace XmlHelperLib{
public class XmlAttributeNotFound : Exception{
    public XmlAttributeNotFound(string missingAttributeName, XmlNode parentNode) :
        base(TextMessage(missingAttributeName, parentNode, null)){ }

    // ReSharper disable once UnusedMember.Global
    public XmlAttributeNotFound(string missingAttributeName, XmlNode parentNode, string xmlFile) :
        base(TextMessage(missingAttributeName, parentNode, xmlFile)){ }

    private static string TextMessage(string missingAttributeName, XmlNode parentNode, string filename){
        var nodeContent = parentNode.OuterXml;
        var s = $"Not found: Attribute [{missingAttributeName}]";
        while (parentNode != null){
            s += $" in {NodeAsText(parentNode)}";
            parentNode = parentNode.ParentNode;
        }

        if (filename != null) s += $" in file {filename}";

        return s + ":" + Environment.NewLine + Environment.NewLine + nodeContent + Environment.NewLine;
    }

    private static string NodeAsText(XmlNode node){
        var p = node.OuterXml.IndexOf('>');
        return node.OuterXml.Substring(0, p + 1);
    }
}
}