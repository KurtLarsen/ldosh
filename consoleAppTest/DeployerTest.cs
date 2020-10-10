using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ArgumentHandlerLib;
using consoleApp;
using NUnit.Framework;
using TestToolsLib;

namespace consoleAppTest{
public class DeployerTest{
    private TestTools _testTools;

    [SetUp]
    public void Setup(){
        _testTools = new TestTools();
    }

    [TearDown]
    public void TeadDown(){
        _testTools.Dispose();
    }

    [Test, TestCaseSource(nameof(ConstructDeployerTestData))]
    public void Deployer_can_be_constructed(string xml, string[] args){
        // ----------- setup ------------
        var projectName = "testProject";
        var projectRoot = _testTools.GetTestFolder() + @"testData\" + projectName;

        // create xml file
        var tmpXml = _testTools.GetNewTmpFilePath(".xml", projectRoot + @"\deployment\tmp");
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        xmlDoc.Save(tmpXml);

        // add -px profile.xml to arguments
        var allArgs = new string[args.Length + 2];
        allArgs[0] = "-px";
        allArgs[1] = tmpXml;
        var index = 2;
        foreach (string s in args){
            allArgs[index] = s;
        } 
        
        var argumentHandler = new ArgumentHandlerLib.ArgumentHandler();
        argumentHandler.DefineArgument(new Argument("px").RequiredValueCount(1));

        Assert.True( argumentHandler.AnalyzeGivenInput(allArgs));

        
        // ----------- test ------------
        var deployer = new Deployer(argumentHandler);
        Assert.NotNull(deployer);

        Assert.That(deployer.ProjectRoot, Is.EqualTo(projectRoot));
        Assert.That(deployer.ProjectName, Is.EqualTo(projectName));
    }

    private static IEnumerable<TestCaseData> ConstructDeployerTestData{
        get{
            yield return
                new TestCaseData("<deployer><profile name='abc'></profile></deployer>", new string[]{ }).SetName("-px profile.xml");
        }
    }
}
}