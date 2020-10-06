using System;
using System.Collections.Generic;
using ArgumentHandlerLib;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class ArgumentHandlerTest{
    [Test]
    public void ArgumentHandler_works(){
        var x = new ArgumentHandler();
        x.DefineArgument(new Argument("px").Required().RequiredValueCount(1).LongId("profileXml"));
        x.DefineArgument(new Argument("p").Optional().RequiredValueCount(1));
        var result = x.AnalyzeGivenInput("-px", @"c:\abc.xml");
        Assert.True(result);
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_required_argument_is_missing(){
        var requiredArgument = "a";
        var optionalArgument = "b";

        var x = new ArgumentHandler();
        x.DefineArgument(new Argument(requiredArgument));
        x.DefineArgument(new Argument(optionalArgument).Optional());

        var result = x.AnalyzeGivenInput("-" + optionalArgument);

        Assert.False(result);

        Assert.NotNull(x.ExceptionThrown);

        Assert.IsInstanceOf<MissingRequiredArgumentException>(x.ExceptionThrown);

        Assert.That(((MissingRequiredArgumentException) x.ExceptionThrown).ErrCode(),
            Is.EqualTo(MissingRequiredArgumentException.Code));

        Assert.That(x.ExceptionThrown.Message,
            Is.EqualTo(string.Format(MissingRequiredArgumentException.MsgMask, requiredArgument)));
    }

    [Test, TestCaseSource(nameof(MissingValuesTestData))]
    public void ArgumentHandler_throws_exception_when_value_is_missing(int requiredCount, string[] givenValueStrings,
        string expectedValueCountMsg){

        var argumentName = "a";
        
        var x = new ArgumentHandler();
        x.DefineArgument(new Argument(argumentName).RequiredValueCount(requiredCount));

        var a = new string[givenValueStrings.Length + 1];
        a[0] = "-"+argumentName;
        var index = 1;
        foreach (string aGivenValueGivenString in givenValueStrings){
            a[index++] = aGivenValueGivenString;
        }

        var result = x.AnalyzeGivenInput(a);

        Assert.False(result);
        
        Assert.NotNull(x.ExceptionThrown);
        
        Assert.IsInstanceOf<MissingValuesException>(x.ExceptionThrown);
        
        Assert.That(((MissingValuesException) x.ExceptionThrown).ErrCode(), Is.EqualTo(MissingValuesException.Code));
        
        var expectedMessage = string.Format(
            MissingValuesException.MsgMask, 
            "-"+argumentName, 
            expectedValueCountMsg,
            String.Join(" ", givenValueStrings)
            );
        
        Assert.That(x.ExceptionThrown.Message, Is.EqualTo(expectedMessage));
    }

    private static IEnumerable<TestCaseData> MissingValuesTestData{
        get{
            yield return new TestCaseData(1, new string[]{ }, "1 value").SetName("1 value required, 0 given");
            yield return new TestCaseData(2, new string[]{ }, "2 values").SetName("2 values required, 0 given");
            yield return new TestCaseData(2, new string[]{"xxx"}, "2 values").SetName("2 values required, 1 given");
        }
    }

    [Test]
    public void ArgumentHandler_throws_exception_when_syntax_has_error(){
        var x = new ArgumentHandler();
        x.DefineArgument(new Argument("a").RequiredValueCount(1));
        var result = x.AnalyzeGivenInput("-");
        Assert.False(result);
        Assert.NotNull(x.ExceptionThrown);
        Assert.IsInstanceOf<UnknownArgumentIdException>(x.ExceptionThrown);
        Assert.That(((UnknownArgumentIdException) x.ExceptionThrown).ErrCode(),
            Is.EqualTo(UnknownArgumentIdException.Code));
        Assert.That(x.ExceptionThrown.Message, Is.EqualTo(string.Format(UnknownArgumentIdException.MsgMask, "-")));
    }

    [Test]
    public void Argument_property_stop_causes_execution_to_stop(){
        var x = new ArgumentHandler();
        x.DefineArgument(new Argument("p").RequiredValueCount(1).Required());
        x.DefineArgument(new Argument("?").Optional().IgnoreOthers());
        x.AnalyzeGivenInput("-?", "-p", "abc");
        Assert.True(x.Contains("?"));
        Assert.False(x.Contains("p"));
    }
}
}