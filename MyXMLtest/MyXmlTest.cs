using System.IO;
using MyXML;
using NUnit.Framework;
using TestToolsLib;

namespace MyXMLtest{
[TestFixture]
public class MyXmlTest{
    [Test]
    public void MyXml_can_create_new_myXmlDoc_from_file(){
        var testTools = new TestTools();
        var testDataFolder = testTools.GetTestFolder();
        var myXmlDoc = MyXmlDoc.NewDocFromFile(testDataFolder + @"testData\dummy1.xml");
        Assert.NotNull(myXmlDoc);
    }

    [Test]
    public void can_create_new_myXmlDoc_form_string(){
        var myXmlDoc = MyXmlDoc.NewDocFromString("<root></root>");
        Assert.NotNull(myXmlDoc);
    }

    [Test]
    public void can_create_temp_xmlFile(){
        var testTools = new TestTools();
        
        var myXmlDoc = MyXmlDoc.NewDocFromString("<root></root>");
        var tmpFileName = testTools.GetNewTmpFilePath(".xml");
        myXmlDoc.Save(tmpFileName);

        Assert.That(File.Exists(tmpFileName), Is.True);
        
        testTools.Dispose();

        Assert.That(File.Exists(tmpFileName), Is.False);
    }
}
}