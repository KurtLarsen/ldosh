using ArgumentHandler;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class ArgumentHandlerTest{
    [Test]
    public void ArgumentHandler_works(){
        var x = new ArgumentHandler.ArgumentHandler();
        x.AddArgument(new Argument("px").Required().ValueCount(1).LongId("profileXml"));
        x.AddArgument(new Argument("p").Optional().ValueCount(1));
        x.Input("-px", @"c:\abc.xml");
        Assert.True(true);
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_required_argument_is_missing(){
        var x =new ArgumentHandler.ArgumentHandler();
        x.AddArgument(new Argument("a"));
        x.AddArgument(new Argument("b").Optional());
        Assert.Throws<MissingRequiredArgumentException>(delegate{
            x.Input("-b");
        });
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_value_is_missing(){
        var x =new ArgumentHandler.ArgumentHandler();
        x.AddArgument(new Argument("a").ValueCount(1));
        Assert.Throws<MissingValuesException>(delegate{
            x.Input("-a");
        });
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_syntax_has_error(){
        var x =new ArgumentHandler.ArgumentHandler();
        x.AddArgument(new Argument("a").ValueCount(1));
        var exception = Assert.Throws<UnknownArgumentIdException>(delegate{
            x.Input("-");
        });
        Assert.That(exception.Message,Is.EqualTo(string.Format(UnknownArgumentIdException.MsgMask,"-")));
    }

    [Test]
    public void Argument_property_stop_causes_execution_to_stop(){
        var x = new ArgumentHandler.ArgumentHandler();
        x.AddArgument(new Argument("p").ValueCount(1).Required());
        x.AddArgument(new Argument("?").Optional().Stop());
        x.Input("-?","-p","abc");
        Assert.True(x.Contains("?"));
        Assert.False(x.Contains("p"));
    }
}
}