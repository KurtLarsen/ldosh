using System;
using System.Xml;

namespace XmlHelperLib{
public class XmlNodeNotUnique : Exception{
    public XmlNodeNotUnique(XmlNode node) :
        base(TextMessage(node)){ }


    private static string TextMessage(XmlNode node){
        var s = $"XML node <{node.Name}> is not unique in <{node.ParentNode?.Name}>";
        return s;
    }
}
}