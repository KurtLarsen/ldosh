using System;
using System.Xml;
using NUnit.Framework;
using XmlHelperLib;
using XmlHelperLib.exceptions;

namespace XmlHelperTest{
[TestFixture]
public class XmlNodeNotUniqueExceptionTest{
    [Test]
    public void exception_works(){
        var xmlDoc=new XmlDocument();
        xmlDoc.LoadXml("<root><person>John Doe</person><person>John Doe</person></root>");
        // var parentNode = xmlDoc.SelectSingleNode("root")??throw new Exception("Test error! node is null");
        var subNode = xmlDoc.SelectSingleNode("//person")??throw new Exception("Test error! node is null");

        var exception = Assert.Throws<XmlNodeNotUniqueException>(delegate{
            throw new XmlNodeNotUniqueException(subNode);
        });
        
        Assert.IsInstanceOf<XmlHelperException>(exception);
        Assert.IsInstanceOf<MyException.MyException>(exception);
        Assert.That(exception.Code,Is.EqualTo(XmlNodeNotUniqueException.ErrCode));
        Assert.That(exception.Message,Is.EqualTo(string.Format(XmlNodeNotUniqueException.MsgMask,subNode.Name,subNode.ParentNode?.Name)));
    }
}
}