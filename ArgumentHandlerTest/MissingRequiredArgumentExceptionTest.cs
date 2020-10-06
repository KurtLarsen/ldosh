using ArgumentHandlerLib;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class MissingRequiredArgumentExceptionTest{
    [Test]
    public void MissingRequiredArgumentException_works(){
        Argument arg1 = new Argument("abc");
        
        var expectedMsg = string.Format(MissingRequiredArgumentException.MsgMask, arg1.GetShortId);
        var expectedCode = MissingRequiredArgumentException.Code;

        var exception = Assert.Throws<MissingRequiredArgumentException>(delegate{
            throw new MissingRequiredArgumentException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.ErrCode() == expectedCode);
    }
}
}