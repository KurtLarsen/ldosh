using ArgumentHandlerLib;
using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest.exceptionTests{
[TestFixture]
public class ArgumentGivenMoreThanOnceExceptionTest{
    [Test]
    public void ArgumentGivenMoreThanOnceException_works(){
        var arg1 = new Argument("abc"){RawId = "def"};
        var expectedMsg = string.Format(ArgumentGivenMoreThanOnceException.MsgMask, arg1.RawId);

        var exception = Assert.Throws<ArgumentGivenMoreThanOnceException>(delegate{
            throw new ArgumentGivenMoreThanOnceException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code() == ArgumentGivenMoreThanOnceException.Code);
        
    }
}
}