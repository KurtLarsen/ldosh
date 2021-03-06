﻿using NUnit.Framework;
using XmlHelperLib.exceptions;

namespace XmlHelperTest{
[TestFixture]
public class XpathFoundNothingExceptionTest{
    [Test]
    public void exception_works(){
        const string xpath = @"\\dummy";

        var exception = Assert.Throws<XpathFoundNothingException>(delegate{
            throw new XpathFoundNothingException(xpath);
        });

        Assert.IsInstanceOf<XmlHelperException>(exception);
        Assert.IsInstanceOf<MyException.MyException>(exception);
        Assert.That(exception.Code, Is.EqualTo(XpathFoundNothingException.ErrCode));
        Assert.That(exception.Message, Is.EqualTo(string.Format(XpathFoundNothingException.MsgMask, xpath)));
    }
}
}