using System.Collections.Generic;
using ArgumentHandlerLib;
using consoleApp;
using consoleApp.exceptions;
using NUnit.Framework;
using TestToolsLib;

namespace consoleAppTest{
public class DeployerTest{
    private TestTools _testTools;
    private ArgumentHandler _argumentHandler;


    [SetUp]
    public void Setup(){
        _testTools = new TestTools{
            Placeholders = new[]{
                new Placeholder("{projectRoot}", TestTools.GetTestFolder() + @"\TestData\testProject")
            }
        };
        _argumentHandler = Program.GetArgumentHandler();
    }

    [TearDown]
    public void TearDown(){
        _testTools.Dispose();
    }

    [Test,TestCaseSource(nameof(ConstructDeployerTestData2))]
    public void Deployer_can_be_constructed(ConsoleAppTestInput consoleAppTestInput, ExpectedData expectedData){
        // ---------- setup -----------
        _testTools.SetupTFiles(consoleAppTestInput.Files);
        var args=_testTools.ReplacePlaceholders2(consoleAppTestInput.Args).Split(' ');
        if (!_argumentHandler.SetInputParam(args)){
            Assert.Fail(_argumentHandler.ExceptionThrown.Message);
        }
        
        // ----------- test ------------
        var deployer = new Deployer(_argumentHandler);
        Assert.NotNull(deployer);

        Assert.That(deployer.ProjectRoot, Is.EqualTo(_testTools.ReplacePlaceholders(expectedData.ProjectRoot)));
        Assert.That(deployer.ProjectName, Is.EqualTo(_testTools.ReplacePlaceholders(expectedData.ProjectName)));
    }

    // testCaseSource
    private static IEnumerable<TestCaseData> ConstructDeployerTestData2{
        get{
            yield return
                new TestCaseData(
                    new ConsoleAppTestInput{
                        Args = "-px {TmpFile[0]}",
                        Files = new[]{
                            new TFile(".xml", @"{projectRoot}\deployment\tmp",
                                "<deployer><profile name='abc'></profile></deployer>")
                        }
                    },
                    new ExpectedData{
                        ProjectRoot = "{projectRoot}",
                        ProjectName = "testProject",
                        SelectedProfileName = "abc"
                    }
                ).SetName(@"-px <xml inside Laravel project tree>");
            yield return
                new TestCaseData(
                    new ConsoleAppTestInput{
                        Args = "-px {TmpFile[0]} -pr {projectRoot}",
                        Files = new[]{
                            new TFile(".xml", null,
                                "<deployer><profile name='abc'></profile></deployer>")
                        }
                    },
                    new ExpectedData{
                        ProjectRoot = "{projectRoot}",
                        ProjectName = "testProject",
                        SelectedProfileName = "abc"
                    }
                ).SetName(@"-px <xml outside Laravel project tree> -pr <projectRoot>");
            yield return
                new TestCaseData(
                    new ConsoleAppTestInput{
                        Args = "-px {TmpFile[0]}",
                        Files = new[]{
                            new TFile(".xml", null,
                            "<deployer projectRoot='{projectRoot}'><profile name='abc'></profile></deployer>")
                        }
                    },
                    new ExpectedData{
                        ProjectRoot = "{projectRoot}",
                        ProjectName = "testProject",
                        SelectedProfileName = "abc"
                    }
                ).SetName(@"-px <xml outside Laravel project tree> projectRoot is defined in .xml");
            yield return
                new TestCaseData(
                    new ConsoleAppTestInput{
                        Args = "-px {TmpFile[0]} -pr {projectRoot}",
                        Files = new[]{
                            new TFile(".xml", null,
                                "<deployer projectRoot='c:\\dummyProjectRoot'><profile name='abc'></profile></deployer>")
                        }
                    },
                    new ExpectedData{
                        ProjectRoot = "{projectRoot}",
                        ProjectName = "testProject",
                        SelectedProfileName = "abc"
                    }
                ).SetName(@"-px <xml outside Laravel project tree> projectRoot is defined in .xml");
        }
    }


    [Test, TestCaseSource(nameof(Data_Resulting_In_LaravelProjectRootNotFoundException))]
    public void Deployer_throw_LaravelProjectRootNotFoundException(ConsoleAppTestInput consoleAppTestInput, ExpectedData expectedData){
        // ---------- setup -----------
        _testTools.SetupTFiles(consoleAppTestInput.Files);
        _argumentHandler.SetInputParam(_testTools.ReplacePlaceholders2(consoleAppTestInput.Args).Split(' '));
        
        // ---------- test -----------
        var exception = Assert.Throws<LaravelProjectRootNotFoundException>(delegate{
            var unused = new Deployer(_argumentHandler);
        });

        Assert.That(exception.Code(),Is.EqualTo(LaravelProjectRootNotFoundException.Code));
    }

    // ReSharper disable once InconsistentNaming
    private static IEnumerable<TestCaseData> Data_Resulting_In_LaravelProjectRootNotFoundException{
        get{
            yield return
                new TestCaseData(
                    new ConsoleAppTestInput{
                        Args = "-px {TmpFile[0]}",
                        Files = new[]{
                            new TFile(".xml", null,
                                "<deployer><profile name='abc'></profile></deployer>")
                        }
                    },
                    new ExpectedData{
                        ProjectRoot = "{projectRoot}",
                        ProjectName = "testProject",
                        SelectedProfileName = "abc"
                        
                    }
                ).SetName(@"-px <xml in tmp folder>");
            
        }
    } 

}


public class ExpectedData{
    public string ProjectRoot{ get; set; }
    public string ProjectName{ get; set; }
    public string SelectedProfileName{ get; set; }
}
}