using System;
using System.Xml;
using NUnit.Framework;
using XmlHelperLib.exceptions;

namespace XmlHelperTest{
[TestFixture]
public class XmlNodeWrongNameExceptionTest{
    [Test]
    public void exception_works(){
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<root><person>John Doe</person></root>");
        var selectedNode = xmlDoc.SelectSingleNode("//person") ?? throw new Exception("Test error! node is null");

        const string expectedName = "animal";

        var exception = Assert.Throws<XmlNodeWrongNameException>(delegate{
            throw new XmlNodeWrongNameException(expectedName, selectedNode);
        });

        Assert.IsInstanceOf<XmlHelperException>(exception);
        Assert.IsInstanceOf<MyException.MyException>(exception);
        Assert.That(exception.Code, Is.EqualTo(XmlNodeWrongNameException.ErrCode));
        Assert.That(exception.Message,
            Is.EqualTo(string.Format(XmlNodeWrongNameException.MsgMask, expectedName, selectedNode.Name)));
    }
}
}