using System.Xml;

namespace MyXML{
public sealed class MyXmlDoc : XmlDocument{
    private MyXmlDoc(){ }

    public static MyXmlDoc NewDocFromFile(string pathToXmlString){
        var x = new MyXmlDoc();
        x.Load(pathToXmlString);
        return x;
    }

    public static MyXmlDoc NewDocFromString(string xmlString){
        var x = new MyXmlDoc();
        x.LoadXml(xmlString);
        return x;
    }
}
}