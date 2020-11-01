using System;
using System.Xml;
using NUnit.Framework;
using XmlHelperLib.exceptions;

namespace XmlHelperTest{
[TestFixture]
public class XmlAttributeNotFoundExceptionTest{
    [Test]
    public void exception_works(){
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<root><person>John Doe</person></root>");
        var node = xmlDoc.SelectSingleNode("//person") ?? throw new Exception("Test error! node is null");

        const string attributeName = "attributeName";

        var exception = Assert.Throws<XmlAttributeNotFoundException>(delegate{
            throw new XmlAttributeNotFoundException(attributeName, node);
        });

        Assert.IsInstanceOf<XmlHelperException>(exception);
        Assert.IsInstanceOf<MyException.MyException>(exception);
        Assert.That(exception.Code, Is.EqualTo(XmlAttributeNotFoundException.ErrCode));
        Assert.That(exception.Message,
            Is.EqualTo(string.Format(XmlAttributeNotFoundException.MsgMask, attributeName, node.Name)));
    }
}
}