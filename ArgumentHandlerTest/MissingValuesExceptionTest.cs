using ArgumentHandlerLib;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class MissingValuesExceptionTest{
    [Test]
    public void MissingValuesException_works(){
        Argument arg1 = new Argument("abc").RequiredValueCount(1);
        var expectedMsg = string.Format(MissingValuesException.MsgMask, arg1.RawId,arg1.RequiredValueCountAsString(),arg1.GivenValuesAsString());
        var expectedCode = MissingValuesException.Code;

        var exception = Assert.Throws<MissingValuesException>(delegate{
            throw new MissingValuesException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.ErrCode() == expectedCode);
    }
}
}