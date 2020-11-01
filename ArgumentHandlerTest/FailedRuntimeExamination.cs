using consoleApp;
using NUnit.Framework;

namespace ArgumentHandlerTest{
[TestFixture]
public class FailedRuntimeExamination{
    [Test]
    public void why_does_this_fail(){
        var argumentHandler = Program.GetArgumentHandler();

        if (!argumentHandler.SetInputParam(
            "-px",
            @"C:\Users\ms\AppData\Local\Temp\2949c170-d942-466d-b4c1-105e918eac87.xml",
            "-pr",
            @"C:\projects\LaravelDeployOnSharedHost\consoleAppTest\TestData\testProject")){
            throw argumentHandler.ExceptionThrown;
        }

        Assert.True(true);
    }
}
}