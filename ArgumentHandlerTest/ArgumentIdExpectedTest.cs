using ArgumentHandlerLib;
using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class ArgumentIdExpectedTest{
    [Test]
    public void ArgumentIdExpected_works(){
        string arg1 = "abc";
        var expectedMsg = string.Format(ArgumentIdExpected.MsgMask, arg1);
        var expectedCode = ArgumentIdExpected.ErrCode;

        var exception = Assert.Throws<ArgumentIdExpected>(delegate{
            throw new ArgumentIdExpected(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code == expectedCode);
    }
}
}