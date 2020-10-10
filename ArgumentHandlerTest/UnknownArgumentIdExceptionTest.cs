using ArgumentHandlerLib;
using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class UnknownArgumentIdExceptionTest{
    [Test]
    public void UnknownArgumentIdException_works(){
        string arg1 = "abc";
        var expectedMsg = string.Format(UnknownArgumentIdException.MsgMask, arg1);
        var expectedCode = UnknownArgumentIdException.ErrCode;

        var exception = Assert.Throws<UnknownArgumentIdException>(delegate{
            throw new UnknownArgumentIdException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code == expectedCode);
    }
}
}