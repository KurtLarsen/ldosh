using System.IO;
using consoleApp;
using NUnit.Framework;

namespace consoleAppTest{
[TestFixture]
public class DeployerStaticTest{
    [Test]
    public void function_IsProjectFolder_works(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");
        var folderName = testDataFolder + @"testProject";

        var di = new DirectoryInfo(folderName);

        var result = Deployer.IsProjectFolder(di);
        Assert.True(result);

        folderName = testDataFolder + @"testProject\app";

        di = new DirectoryInfo(folderName);

        result = Deployer.IsProjectFolder(di);
        Assert.False(result);
    }

    [Test]
    public void function_LocateProjectRoot_works_when_given_projectRoot(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");

        var result = Deployer.LocateProjectRoot(testDataFolder + @"testProject");

        Assert.That(result, Is.EqualTo(testDataFolder + "testProject"));
    }

    [Test]
    public void function_LocateProjectRoot_works_when_given_existing_file_in_project_tree(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");

        var result = Deployer.LocateProjectRoot(testDataFolder + @"testProject\deployment\dummyProfiles.xml");

        Assert.That(result, Is.EqualTo(testDataFolder + "testProject"));
    }

    [Test]
    public void function_LocateProjectRoot_works_when_given_existing_folder_in_project_tree(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");

        var result = Deployer.LocateProjectRoot(testDataFolder + @"testProject\deployment");

        Assert.That(result, Is.EqualTo(testDataFolder + "testProject"));
    }

    [Test]
    public void function_LocateProjectRoot_works_when_given_non_existing_file_in_project_tree(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");

        var result = Deployer.LocateProjectRoot(testDataFolder + @"testProject\deployment\xxx.xml");

        Assert.That(result, Is.EqualTo(testDataFolder + "testProject"));
    }

    [Test]
    public void function_LocateProjectRoot_works_when_given_existing_non_folder_in_project_tree(){
        var testDataFolder = Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\");

        var result = Deployer.LocateProjectRoot(testDataFolder + @"testProject\xxx");

        Assert.That(result, Is.EqualTo(testDataFolder + "testProject"));
    }

    [Test]
    public void function_LocateProjectRoot_works_when_given_non_existing_file_outside_project_tree(){
        var result = Deployer.LocateProjectRoot(@"c:\testProject\deployment\xxx.xml");

        Assert.That(result, Is.Null);
    }
}
}