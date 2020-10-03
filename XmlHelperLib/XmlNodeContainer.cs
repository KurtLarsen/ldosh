using System;
using System.Xml;

namespace XmlHelperLib{
public class XmlNodeContainer{
    protected readonly XmlNode SelfNode_;

    protected XmlNodeContainer(XmlNode node){
        SelfNode_ = node;
    }

    public XmlNode SelfNode => SelfNode_;

    protected void AssertSelf(string nodeName, string attrName = null){
        if (SelfNode == null) throw new Exception("Node is null");
        if (SelfNode.Name != nodeName) throw new XmlNodeWrongName(SelfNode, nodeName);
        if (attrName != null){
            var unused = SelfNode.Attributes?[attrName] ?? throw new XmlAttributeNotFound(attrName, SelfNode);
        }
    }

    protected void AssertOneAndOnlyChildNode(string childNodeName){
        var childNodeList = SelfNode.SelectNodes(childNodeName) ?? throw new Exception("Can not selectNodes");
        if (childNodeList.Count == 0) throw new XmlNodeNotFound(childNodeName, SelfNode);
        if (childNodeList.Count > 1) throw new XmlNodeNotUnique(childNodeList[0]);
    }

    protected void AssertOneOrMoreChildNodes(string childNodeName){
        var childNodeList = SelfNode.SelectNodes(childNodeName) ?? throw new Exception("Can not selectNodes");
        if (childNodeList.Count == 0) throw new XmlNodeNotFound(childNodeName, SelfNode);
    }
}
}