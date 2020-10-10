using System;
using System.Linq;
using System.Xml;
using XmlHelperLib.exceptions;

namespace XmlHelperLib{
public static class XmlHelper{
    public static string GetAttr(string attrName, XmlNode node){
        var s = node.Attributes?[attrName]?.Value ?? throw new XmlAttributeNotFoundException(attrName, node);
        return s;
    }

    public static XmlNodeList GetOneOrMoreNodes(string nodeName, XmlNode parent){
        var nodeList = parent.SelectNodes(nodeName);
        if (nodeList == null) throw new Exception($"Error selecting node <{nodeName}> from <{parent}>");
        if (nodeList.Count == 0) throw new XmlNodeNotFoundException(nodeName, parent);
        return nodeList;
    }

    public static XmlNode GetZeroOrOneNode(string nodeName, XmlNode parent){
        var nodeList = parent.SelectNodes(nodeName);
        if (nodeList == null) throw new Exception($"Error selecting node <{nodeName}> from <{parent}>");
        if (nodeList.Count > 1) throw new XmlNodeNotUniqueException(nodeList[0]);
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (nodeList.Count == 0) return null;
        return nodeList[0];
    }

    public static XmlNode GetUniqueNode(string nodeName, XmlNode parent){
        var nodeList = parent.SelectNodes(nodeName);
        if (nodeList == null) throw new Exception($"Error selecting node <{nodeName}> from <{parent}>");
        if (nodeList.Count == 0) throw new XmlNodeNotFoundException(nodeName, parent);
        if (nodeList.Count > 1) throw new XmlNodeNotUniqueException(nodeList[0]);
        return nodeList[0];
    }


    public static XmlNodeList GetZeroOrMoreNodes(string nodeName, XmlNode parent){
        var nodeList = parent.SelectNodes(nodeName);
        if (nodeList == null) throw new Exception($"Error selecting node <{nodeName}> from <{parent}>");
        return nodeList;
    }

    // -----------------------------------------------------------------------------------------------------------------
    //    ChangeInnerText
    // -----------------------------------------------------------------------------------------------------------------
    public static XmlDocument ChangeInnerText(XmlDocument xmlDocument, string xpath, string newText){
        var nodeList = xmlDocument.SelectNodes(xpath);

        // ReSharper disable once InvertIf
        if (nodeList != null)
            foreach (XmlNode node in nodeList)
                node.InnerText = newText;
        return xmlDocument;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static XmlDocument ChangeInnerText(string pathToXmlFile, string xpath, string newText){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(pathToXmlFile);
        return ChangeInnerText(xmlDocument, xpath, newText);
    }

    // ReSharper disable once UnusedMember.Global
    public static string ChangeInnerText(string pathToXmlFile, string xpath, string newText, string destinationFile){
        var xmlDoc = ChangeInnerText(pathToXmlFile, xpath, newText);
        xmlDoc.Save(destinationFile);
        return destinationFile;
    }


    // -----------------------------------------------------------------------------------------------------------------
    //    ChangeRootName
    // -----------------------------------------------------------------------------------------------------------------
    public static XmlDocument ChangeRootName(XmlDocument xmlDocument, string newRootName){
        // https://stackoverflow.com/questions/475293/change-the-node-names-in-an-xml-file-using-c-sharp

        var oldRootNode = xmlDocument.DocumentElement;
        var newRootNode = xmlDocument.CreateElement(newRootName);
        // ReSharper disable once PossibleNullReferenceException
        newRootNode.InnerXml = oldRootNode.InnerXml;

        var newXmlDoc = new XmlDocument();
        newXmlDoc.LoadXml(newRootNode.OuterXml);
        return newXmlDoc;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static XmlDocument ChangeRootName(string pathToXmlFile, string newRootName){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(pathToXmlFile);
        return ChangeRootName(xmlDocument, newRootName);
    }

    // ReSharper disable once UnusedMember.Global
    public static string ChangeRootName(string pathToXmlFile, string newRootName, string destinationFile){
        var xmlDoc = ChangeRootName(pathToXmlFile, newRootName);
        xmlDoc.Save(destinationFile);
        return destinationFile;
    }

    // -----------------------------------------------------------------------------------------------------------------
    //    DeleteNodes
    // -----------------------------------------------------------------------------------------------------------------
    public static XmlDocument DeleteNodes(XmlDocument xmlDocument, string xpath){
        var nodeList = xmlDocument.SelectNodes(xpath);
        // ReSharper disable once PossibleNullReferenceException
        foreach (XmlNode node in nodeList) node.ParentNode?.RemoveChild(node);
        return xmlDocument;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static XmlDocument DeleteNodes(string pathToXmlFile, string xpath){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(pathToXmlFile);
        return DeleteNodes(xmlDocument, xpath);
    }

    // ReSharper disable once UnusedMember.Global
    public static string DeleteNodes(string pathToXmlFile, string xpath, string destinationFile){
        var xmlDoc = DeleteNodes(pathToXmlFile, xpath);
        xmlDoc.Save(destinationFile);
        return destinationFile;
    }

    // -----------------------------------------------------------------------------------------------------------------
    //    RenameNodes
    // -----------------------------------------------------------------------------------------------------------------

    public static XmlDocument RenameNodes(XmlDocument xmlDocument, string xpath, string newName){
        var nodeList = xmlDocument.SelectNodes(xpath);
        // ReSharper disable once PossibleNullReferenceException
        foreach (XmlNode node in nodeList){
            var newElement = node.OwnerDocument?.CreateElement(newName);
            // ReSharper disable once PossibleNullReferenceException
            newElement.InnerXml = node.InnerXml;

            node.ParentNode?.ReplaceChild(newElement, node);
            // // ReSharper disable once AssignNullToNotNullAttribute
            // node.ParentNode
            // node.ParentNode?.InsertBefore(newElement, node);
            // node.ParentNode?.RemoveChild(node);
            //todo: Copy Attributes 
        }

        return xmlDocument;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static XmlDocument RenameNodes(string pathToXmlFile, string xpath, string newName){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(pathToXmlFile);
        return RenameNodes(xmlDocument, xpath, newName);
    }

    // ReSharper disable once UnusedMember.Global
    public static string RenameNodes(string pathToXmlFile, string xpath, string newName, string destinationFile){
        var xmlDoc = RenameNodes(pathToXmlFile, xpath, newName);
        xmlDoc.Save(destinationFile);
        return destinationFile;
    }

    public static void AssertName(XmlNode node, string name){
        if (node.Name != name) throw new XmlNodeWrongNameException(name,node);
    }

    public static void AssertName(XmlNode node, string[] nameArray){
        if (!nameArray.Contains(node.Name)) throw new XmlNodeWrongNameException(nameArray.ToString(),node);
    }

    public static XmlDocument AddChildNode(XmlDocument xmlDoc, string xpath, string xmlString){
        var fragment = xmlDoc.CreateDocumentFragment();
        fragment.InnerXml = xmlString;

        var parentNode = xmlDoc.SelectSingleNode(xpath);
        if (parentNode == null) throw new XpathFoundNothingException(xpath);
        parentNode.AppendChild(fragment);
        return xmlDoc;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static XmlDocument AddChildNode(string pathToXmlFile, string xPath, string xmlString){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(pathToXmlFile);
        return AddChildNode(xmlDocument, xPath, xmlString);
    }

    // ReSharper disable once UnusedMember.Global
    public static string AddChildNode(string pathToXmlFile, string xPath, string xmlString, string destinationFile){
        var xmlDoc = AddChildNode(pathToXmlFile, xPath, xmlString);
        xmlDoc.Save(destinationFile);
        return destinationFile;
    }

    // ReSharper disable once UnusedMember.Global
    public static string GetFragment(string pathToXmlFile, string xPath){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(pathToXmlFile);
        var x = xmlDocument.SelectSingleNode(xPath);
        return x?.OuterXml;
    }

    // ReSharper disable once UnusedMember.Global
    public static XmlNodeList GetOneOrMoreNodes(string childNodeName, string xmlFileName){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlFileName);
        return GetOneOrMoreNodes(childNodeName, xmlDocument);
    }

    public static XmlNode GetUniqueNode(string childNodeName, string xmlFileName){
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlFileName);
        return GetUniqueNode(childNodeName, xmlDocument);
    }

    public static string GetOptionalAttr(string attrName, XmlNode node){
        var s = node.Attributes?[attrName]?.Value ?? null;
        return s;
        
    }
}
}