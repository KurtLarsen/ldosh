using ArgumentHandlerLib;
using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest.exceptionTests{
[TestFixture]
public class MissingValuesExceptionTest{
    [Test]
    public void MissingValuesException_works(){
        var arg1 = new Argument("abc").RequiredValueCount(1);
        var expectedMsg = string.Format(MissingValuesException.MsgMask, arg1.RawId, arg1.RequiredValueCountAsString(),
            arg1.GivenValuesAsString());
        const int expectedCode = MissingValuesException.Code;

        var exception = Assert.Throws<MissingValuesException>(delegate{ throw new MissingValuesException(arg1); });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code() == expectedCode);
    }
}
}