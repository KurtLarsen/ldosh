using System;
using System.Xml;

namespace XmlHelperLib{
public class XmlNodeWrongName : Exception{
    public XmlNodeWrongName(XmlNode node, string expectedName) :
        base(TextMessage(node, expectedName)){ }

    private static string TextMessage(XmlNode node, string expectedName, string filename = null){
        var nodeContent = node.OuterXml;
        var s = $"Wrong Node Name: Expected: <{expectedName}> Found: <{node.Name}>";
        var parentNode = node;
        while (parentNode != null){
            s += $" in node <{parentNode.LocalName}>";
            parentNode = parentNode.ParentNode;
        }

        if (filename != null) s += $" in file {filename}";

        return s + ":" + Environment.NewLine + Environment.NewLine + nodeContent + Environment.NewLine;
    }
}
}