using System.IO;
using NUnit.Framework;
using TestToolsLib;

namespace TestToolsTest{
[TestFixture]
public class TestToolsTest{
    [Test]
    public void a_tmp_file_is_deleted_on_dispose(){
        var testTools = new TestTools();

        Assert.NotNull(testTools);

        var tmpFileName = testTools.GetNewTmpFilePath(".txt");

        Assert.NotNull(tmpFileName);
        Assert.False(File.Exists(tmpFileName));

        File.WriteAllText(tmpFileName, "abc");

        Assert.True(File.Exists(tmpFileName));

        testTools.Dispose();

        Assert.False(File.Exists(tmpFileName));
    }
}
}