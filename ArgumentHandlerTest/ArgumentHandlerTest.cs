using System.Collections.Generic;
using ArgumentHandlerLib;
using ArgumentHandlerLib.exceptions;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class ArgumentHandlerTest{
    [Test]
    public void ArgumentHandler_works(){
        var x = new ArgumentHandler(
            new Argument("px").Required().RequiredValueCount(1).LongId("profileXml"),
            new Argument("p").Optional().RequiredValueCount(1));
        var result = x.SetInputParam("-px", @"c:\abc.xml");
        Assert.True(result);
    }

    [Test]
    public void ArgumentHandler_can_be_initialized_with_array_of_Arguments(){
        var x = new ArgumentHandler(
            new Argument("px").Required().RequiredValueCount(1).LongId("profileXml"), 
            new Argument("p").Optional().RequiredValueCount(1));

        Assert.NotNull(x);

        var result = x.SetInputParam("-px", @"c:\abc.xml");
        Assert.True(result);
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_required_argument_is_missing(){
        const string requiredArgument = "a";
        const string optionalArgument = "b";

        var x = new ArgumentHandler(
            new Argument(requiredArgument),
            new Argument(optionalArgument).Optional());

        var result = x.SetInputParam("-" + optionalArgument);

        Assert.False(result);

        Assert.NotNull(x.ExceptionThrown);

        Assert.IsInstanceOf<MissingRequiredArgumentException>(x.ExceptionThrown);

        Assert.That(((MissingRequiredArgumentException) x.ExceptionThrown).Code(),
            Is.EqualTo(MissingRequiredArgumentException.Code));

        Assert.That(x.ExceptionThrown.Message,
            Is.EqualTo(string.Format(MissingRequiredArgumentException.MsgMask, requiredArgument)));
    }

    [Test]
    [TestCaseSource(nameof(MissingValuesTestData))]
    public void ArgumentHandler_throws_exception_when_value_is_missing(int requiredCount, string[] givenValueStrings,
        string expectedValueCountMsg){
        const string argumentName = "a";

        var x = new ArgumentHandler(new Argument(argumentName).RequiredValueCount(requiredCount));

        var a = new string[givenValueStrings.Length + 1];
        a[0] = "-" + argumentName;
        var index = 1;
        foreach (var aGivenValueGivenString in givenValueStrings) a[index++] = aGivenValueGivenString;

        var result = x.SetInputParam(a);

        Assert.False(result);

        Assert.NotNull(x.ExceptionThrown);

        Assert.IsInstanceOf<MissingValuesException>(x.ExceptionThrown);

        Assert.That(((MissingValuesException) x.ExceptionThrown).Code(), Is.EqualTo(MissingValuesException.Code));

        var expectedMessage = string.Format(
            MissingValuesException.MsgMask,
            "-" + argumentName,
            expectedValueCountMsg,
            string.Join(" ", givenValueStrings)
        );

        Assert.That(x.ExceptionThrown.Message, Is.EqualTo(expectedMessage));
    }

    private static IEnumerable<TestCaseData> MissingValuesTestData{
        get{
            yield return new TestCaseData(1, new string[]{ }, "1 value").SetName("1 value required, 0 given");
            yield return new TestCaseData(2, new string[]{ }, "2 values").SetName("2 values required, 0 given");
            yield return new TestCaseData(2, new[]{"xxx"}, "2 values").SetName("2 values required, 1 given");
        }
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_syntax_has_error(){
        var x = new ArgumentHandler(new Argument("a").RequiredValueCount(1));
        var result = x.SetInputParam("-");
        Assert.False(result);
        Assert.NotNull(x.ExceptionThrown);
        Assert.IsInstanceOf<UnknownArgumentIdException>(x.ExceptionThrown);
        Assert.That(((UnknownArgumentIdException) x.ExceptionThrown).Code,
            Is.EqualTo(UnknownArgumentIdException.ErrCode));
        Assert.That(x.ExceptionThrown.Message, Is.EqualTo(string.Format(UnknownArgumentIdException.MsgMask, "-")));
    }

    [Test]
    public void Argument_property_stop_causes_execution_to_stop(){
        var x = new ArgumentHandler(
            new Argument("p").RequiredValueCount(1).Required(),
            new Argument("?").Optional().IgnoreOthers()
            );
        x.SetInputParam("-?", "-p", "abc");
        Assert.True(x.Contains("?"));
        Assert.False(x.Contains("p"));
    }
}
}