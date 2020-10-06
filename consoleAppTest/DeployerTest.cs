using System;
using System.IO;
using ArgumentHandlerLib;
using consoleApp;
using NUnit.Framework;
using TestToolsLib;

namespace consoleAppTest{
public class DeployerTest{
    [Test]
    public void Deployer_can_be_constructed(){
        var testTools=new TestTools();
        // var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");
        var testDataFolder = testTools.GetTestFolder();
        var dummyProfilesXml = testDataFolder + @"testProject\deployment\dummyProfiles.xml";


        var argumentHandler = new ArgumentHandlerLib.ArgumentHandler();
        argumentHandler.DefineArgument(new Argument("px").RequiredValueCount(1));
        if (!argumentHandler.AnalyzeGivenInput("-px", dummyProfilesXml)){
            Environment.Exit(0);
        }

        var deployer=new Deployer(argumentHandler);
        Assert.NotNull(deployer);
        
        Assert.That(deployer.ProjectRoot,Is.EqualTo(testDataFolder+"testProject") );
        Assert.That(deployer.ProjectName,Is.EqualTo("testProject"));
    }

}
}