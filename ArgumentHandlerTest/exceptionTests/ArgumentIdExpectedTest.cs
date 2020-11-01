using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest.exceptionTests{
[TestFixture]
public class ArgumentIdExpectedTest{
    [Test]
    public void ArgumentIdExpected_works(){
        const string arg1 = "abc";
        var expectedMsg = string.Format(ArgumentIdExpectedException.MsgMask, arg1);

        var exception = Assert.Throws<ArgumentIdExpectedException>(delegate{
            throw new ArgumentIdExpectedException(arg1);
        });

        Assert.IsInstanceOf<ArgumentException>(exception);
        Assert.That(exception.Message, Is.EqualTo(expectedMsg));
        Assert.That(exception.Code() == ArgumentIdExpectedException.Code);
    }
}
}