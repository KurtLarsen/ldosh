using System.IO;
using consoleApp;
using NUnit.Framework;

namespace consoleAppTest{
[TestFixture]
public class ProfilesLoaderTest{
    [Test]
    public void ProfilesLoader_return_false_when_xml_error(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");
        var filename = testDataFolder + @"testProject\deployment\profiles.xml";
        if (!Global.ArgumentParser(new string[]{"-px", filename})){
            Assert.Fail($"ArgumentParser failed with exitcode {Global.ExitCode}: {Global.ErrorMessage}");
        }

        var result=Global.ProfilesLoad();
        Assert.False(result,Global.ErrorMessage);

    }
}
}