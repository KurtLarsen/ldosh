using System;
using System.Xml;

namespace XmlHelperLib{
// ReSharper disable once UnusedType.Global
public class XmlNodeUnknown : Exception{
    public XmlNodeUnknown(XmlNode parentNode) :
        base(TextMessage(parentNode, null)){ }

    public XmlNodeUnknown(XmlNode parentNode, string filename) :
        base(TextMessage(parentNode, filename)){ }

    private static string TextMessage(XmlNode parentNode, string filename){
        var nodeContent = parentNode.OuterXml;
        var s = $"Unknown: Node <{parentNode.Name}>";
        while (parentNode != null){
            s += $" in node <{parentNode.LocalName}>";
            parentNode = parentNode.ParentNode;
        }

        if (filename != null) s += $" in file {filename}";

        return s + ":" + Environment.NewLine + Environment.NewLine + nodeContent + Environment.NewLine;
    }
}
}