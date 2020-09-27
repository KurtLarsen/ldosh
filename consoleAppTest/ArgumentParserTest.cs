using System.IO;
using consoleApp;
using NUnit.Framework;

namespace consoleAppTest{
[TestFixture]
public class ArgumentParserTest{
    [Test]
    public void ArgumentParser_returns_help_message_when_no_arguments_are_given(){
        var result = Global.ArgumentParser(new string[]{ });
        Assert.False(result);
        Assert.That(Global.ExitCode, Is.EqualTo(Global.EXITCODE_HELP));
        Assert.NotNull(Global.ErrorMessage);
        Assert.IsInstanceOf<string>(Global.ErrorMessage);
    }

    [Test]
    public void ArgumentParser_return_unknown_argumet(){
        var result = Global.ArgumentParser(new string[]{ "-dummy"});
        Assert.False(result);
        Assert.That(Global.ExitCode, Is.EqualTo(Global.EXITCODE_UNKNOWN_ARGUMENT));
        Assert.NotNull(Global.ErrorMessage);
        Assert.IsInstanceOf<string>(Global.ErrorMessage);
    }

    [Test]
    public void ArgumentParser_return_missing_PX_argument(){
        var result = Global.ArgumentParser(new string[]{"-p", "abc"});
        Assert.False(result);
        Assert.That(Global.ExitCode, Is.EqualTo(Global.EXITCODE_MISSING_PX_ARGUMENNT));
        Assert.NotNull(Global.ErrorMessage);
        Assert.IsInstanceOf<string>(Global.ErrorMessage);
    }

    [Test]
    public void ArgumentParser_return_PX_FILE_NOT_FUND(){
        var filename = "abc";
        var result = Global.ArgumentParser(new string[]{"-px", filename});
        Assert.False(result);
        Assert.That(Global.ExitCode, Is.EqualTo(Global.EXITCODE_PX_FILE_NOT_FOUND));
        Assert.NotNull(Global.ErrorMessage);
        Assert.IsInstanceOf<string>(Global.ErrorMessage);
        Assert.That(Global.ErrorMessage,Is.EqualTo($"File not found: {filename}"));
    }

    [Test]
    public void ArgumentParser_return_ok(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");
        var filename = testDataFolder + @"testProject\deployment\profiles.xml";
        var result = Global.ArgumentParser(new string[]{"-px", filename});
        Assert.True(result);
    }
}
}