using System.Xml;
using NUnit.Framework;
using XmlHelperLib;
using XmlHelperLib.exceptions;

namespace XmlHelperTest{
[TestFixture]
public class XmlHelperTest{
    [Test]
    public void ChangeRootName_works(){
        const string xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>";
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlString);
        const string newRootName = "xxx";
        var newDoc = XmlHelper.ChangeRootName(xmlDocument, newRootName);
        Assert.NotNull(newDoc);
        var actualRootName = newDoc.DocumentElement?.Name;
        Assert.That(actualRootName, Is.EqualTo(newRootName));
    }

    [Test]
    public void DeleteNodes_works(){
        var xmlDocument = new XmlDocument();

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.DeleteNodes(xmlDocument, "//b");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a></a>"));

        xmlDocument.LoadXml("<a><b>b-text</b></a>");
        XmlHelper.DeleteNodes(xmlDocument, "/b");
        Assert.That(xmlDocument.OuterXml, Is.EqualTo("<a><b>b-text</b></a>"));

        xmlDocument.LoadXml("<a><b>b-text</b></a>");
        XmlHelper.DeleteNodes(xmlDocument, "/a");
        Assert.That(xmlDocument.OuterXml, Is.EqualTo(""));

        xmlDocument.LoadXml("<a><b>b-text</b></a>");
        XmlHelper.DeleteNodes(xmlDocument, "/a/b");
        Assert.That(xmlDocument.OuterXml, Is.EqualTo("<a></a>"));

        xmlDocument.LoadXml("<a><b>b-text</b></a>");
        XmlHelper.DeleteNodes(xmlDocument, "a");
        Assert.That(xmlDocument.OuterXml, Is.EqualTo(""));
    }

    [Test]
    public void RenameNode_works(){
        var xmlDocument = new XmlDocument();

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "//b", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><x>b-text</x></a>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "/b", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "b", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "a/b", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><x>b-text</x></a>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "/a/b", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><x>b-text</x></a>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "//a/b", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><x>b-text</x></a>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "//a", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><x><b>b-text</b></x>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "/a", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><x><b>b-text</b></x>"));

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.RenameNodes(xmlDocument, "a", "x");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><x><b>b-text</b></x>"));
    }

    [Test]
    public void ChangeInnerText_works(){
        var xmlDocument = new XmlDocument();

        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");
        XmlHelper.ChangeInnerText(xmlDocument, "a/b", "new text");
        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>new text</b></a>"));
    }

    [Test]
    public void AddChild_works(){
        var xmlDocument = new XmlDocument();

        // setup
        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");

        // test
        XmlHelper.AddChildNode(xmlDocument, "a", "<c>content of c</c>");

        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b><c>content of c</c></a>"));

        // setup
        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");

        // test
        XmlHelper.AddChildNode(xmlDocument, "/a", "<c>content of c</c>");

        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b><c>content of c</c></a>"));

        // setup
        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");

        // test
        XmlHelper.AddChildNode(xmlDocument, "a/b", "<c>content of c</c>");

        Assert.That(xmlDocument.OuterXml,
            Is.EqualTo(
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text<c>content of c</c></b></a>"));

        // setup
        xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><!-- comment --><a><b>b-text</b></a>");

        // test
        var exception = Assert.Throws<XpathFoundNothingException>(delegate{
            var unused = XmlHelper.AddChildNode(xmlDocument, "b", "<c>content of c</c>");
        });

        Assert.That(exception.Message, Is.EqualTo(string.Format(XpathFoundNothingException.MsgMask, "b")));
    }
}
}