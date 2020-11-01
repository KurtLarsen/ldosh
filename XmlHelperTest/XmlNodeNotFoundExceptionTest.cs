using System;
using System.Xml;
using NUnit.Framework;
using XmlHelperLib.exceptions;

namespace XmlHelperTest{
[TestFixture]
public class XmlNodeNotFoundExceptionTest{
    [Test]
    public void exception_works(){
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<root><person>John Doe</person></root>");
        var parentNode = xmlDoc.SelectSingleNode("//person") ?? throw new Exception("Test error! node is null");

        const string requestedNodeName = "age";

        var exception = Assert.Throws<XmlNodeNotFoundException>(delegate{
            throw new XmlNodeNotFoundException(requestedNodeName, parentNode);
        });

        Assert.IsInstanceOf<XmlHelperException>(exception);
        Assert.IsInstanceOf<MyException.MyException>(exception);
        Assert.That(exception.Code, Is.EqualTo(XmlNodeNotFoundException.ErrCode));
        Assert.That(exception.Message,
            Is.EqualTo(string.Format(XmlNodeNotFoundException.MsgMask, requestedNodeName, parentNode.Name)));
    }
}
}