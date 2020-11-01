using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest.exceptionTests{
[TestFixture]
public class UnknownArgumentIdExceptionTest{
    [Test]
    public void UnknownArgumentIdException_works(){
        const string arg1 = "abc";
        var expectedMsg = string.Format(UnknownArgumentIdException.MsgMask, arg1);
        const int expectedCode = UnknownArgumentIdException.ErrCode;

        var exception = Assert.Throws<UnknownArgumentIdException>(delegate{
            throw new UnknownArgumentIdException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code() == expectedCode);
    }
}
}