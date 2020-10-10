using ArgumentHandlerLib;
using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class ArgumentGivenMoreThanOnceExceptionTest{
    [Test]
    public void ArgumentGivenMoreThanOnceException_works(){
        Argument arg1 = new Argument("abc");
        arg1.RawId = "def";
        var expectedMsg = string.Format(ArgumentGivenMoreThanOnceException.MsgMask, arg1.RawId);
        var expectedCode = ArgumentGivenMoreThanOnceException.ErrCode;

        var exception = Assert.Throws<ArgumentGivenMoreThanOnceException>(delegate{
            throw new ArgumentGivenMoreThanOnceException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code == expectedCode);
    }
}
}