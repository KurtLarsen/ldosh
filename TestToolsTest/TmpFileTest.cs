using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TestToolsLib;

namespace TestToolsTest{
[TestFixture]
public class TmpFileTest{
    [Test]
    public void TmpFile_can_be_created_with_no_arguments(){
        var x = new TmpFile();
        Assert.NotNull(x);
        Assert.NotNull(x.FullPath);
        var folder = Path.GetDirectoryName(x.FullPath) + Path.DirectorySeparatorChar;
        var filename = Path.GetFileName(x.FullPath);
        var extension = Path.GetExtension(x.FullPath);
        Assert.That(folder, Is.EqualTo(Path.GetTempPath()));
        Assert.That(filename.Length, Is.GreaterThan(10));
        Assert.That(extension, Is.EqualTo(""));
        Assert.False(File.Exists(x.FullPath));
    }

    private static IEnumerable<TestCaseData> Data_for_TmpFile_can_be_created_given_only_extension(){
        yield return new TestCaseData(
            new string[]{null},
            new[]{""}
        ).SetName("null");
        yield return new TestCaseData(
            new[]{""},
            new[]{""}
        ).SetName("\"\"");
        yield return new TestCaseData(
            new[]{"txt"},
            new[]{".txt"}
        ).SetName("txt");
        yield return new TestCaseData(
            new[]{".txt"},
            new[]{".txt"}
        ).SetName(".txt");
    }

    [Test]
    [TestCaseSource(nameof(Data_for_TmpFile_can_be_created_given_only_extension))]
    public void TmpFile_can_be_created_given_only_extension(string[] input, string[] expected){
        var x = new TmpFile(input[0]);
        Assert.NotNull(x);
        Assert.NotNull(x.FullPath);
        var fullPathFolder = Path.GetDirectoryName(x.FullPath);
        var fullPathFileName = Path.GetFileName(x.FullPath);
        var fullPathExtension = Path.GetExtension(x.FullPath);
        Assert.That(fullPathFolder, Is.EqualTo(Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar)));
        Assert.That(fullPathFileName.Length, Is.GreaterThan(10));
        Assert.That(fullPathExtension, Is.EqualTo(expected[0]));


        Assert.False(File.Exists(x.FullPath));
    }

    private static IEnumerable<TestCaseData> Data_for_TmpFile_can_be_created_given_extension_and_folder(){
        yield return new TestCaseData(
            new string[]{null, null},
            new[]{"", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar)}
        ).SetName("null, null");
        yield return new TestCaseData(
            new[]{".txt", null},
            new[]{".txt", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar)}
        ).SetName(".txt, null");
        yield return new TestCaseData(
            new[]{".txt", ""},
            new[]{".txt", Directory.GetCurrentDirectory().TrimEnd(Path.DirectorySeparatorChar)}
        ).SetName(".txt, \"\"");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder"},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder"}
        ).SetName(".txt, \\tmpFilesFolder");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder\"},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder"}
        ).SetName(".txt, \\tmpFilesFolder\\");
    }

    [Test]
    [TestCaseSource(nameof(Data_for_TmpFile_can_be_created_given_extension_and_folder))]
    public void TmpFile_can_be_created_given_extension_and_folder(string[] input, string[] expected){
        var x = new TmpFile(input[0], input[1]);
        Assert.NotNull(x);
        Assert.NotNull(x.FullPath);
        var fullPathFolder = Path.GetDirectoryName(x.FullPath);
        var fullPathFileName = Path.GetFileName(x.FullPath);
        var fullPathExtension = Path.GetExtension(x.FullPath);
        Assert.That(fullPathFolder, Is.EqualTo(expected[1]));
        Assert.That(fullPathFileName.Length, Is.GreaterThan(10));
        Assert.That(fullPathExtension, Is.EqualTo(expected[0]));


        Assert.False(File.Exists(x.FullPath));
    }

    private static IEnumerable<TestCaseData> Data_for_TmpFile_can_be_created_given_extension_folder_and_content(){
        yield return new TestCaseData(
            new string[]{null, null, null},
            new[]{"", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar), false.ToString()}
        ).SetName("null, null, null");
        yield return new TestCaseData(
            new[]{".txt", null, null},
            new[]{".txt", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar), false.ToString()}
        ).SetName(".txt, null, null");
        yield return new TestCaseData(
            new[]{".txt", "", null},
            new[]{".txt", Directory.GetCurrentDirectory().TrimEnd(Path.DirectorySeparatorChar), false.ToString()}
        ).SetName(".txt, \"\", null");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", null},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", false.ToString()}
        ).SetName(".txt, \\tmpFilesFolder, null");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder\", null},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", false.ToString()}
        ).SetName(".txt, \\tmpFilesFolder\\, null");

        yield return new TestCaseData(
            new[]{null, null, ""},
            new[]{"", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar), true.ToString()}
        ).SetName("null, null, \"\"");
        yield return new TestCaseData(
            new[]{".txt", null, ""},
            new[]{".txt", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar), true.ToString()}
        ).SetName(".txt, null, \"\"");
        yield return new TestCaseData(
            new[]{".txt", "", ""},
            new[]{".txt", Directory.GetCurrentDirectory().TrimEnd(Path.DirectorySeparatorChar), true.ToString()}
        ).SetName(".txt, \"\", \"\"");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", ""},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", true.ToString()}
        ).SetName(".txt, \\tmpFilesFolder, \"\"");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder\", ""},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", true.ToString()}
        ).SetName(".txt, \\tmpFilesFolder\\, \"\"");


        yield return new TestCaseData(
            new[]{null, null, "abc"},
            new[]{"", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar), true.ToString()}
        ).SetName("null, null, \"abc\"");
        yield return new TestCaseData(
            new[]{".txt", null, "abc"},
            new[]{".txt", Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar), true.ToString()}
        ).SetName(".txt, null, \"abc\"");
        yield return new TestCaseData(
            new[]{".txt", "", "abc"},
            new[]{".txt", Directory.GetCurrentDirectory().TrimEnd(Path.DirectorySeparatorChar), true.ToString()}
        ).SetName(".txt, \"\", \"abc\"");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", "abc"},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", true.ToString()}
        ).SetName(".txt, \\tmpFilesFolder, \"abc\"");
        yield return new TestCaseData(
            new[]{".txt", TestFolder() + @"\tmpFilesFolder\", "abc"},
            new[]{".txt", TestFolder() + @"\tmpFilesFolder", true.ToString()}
        ).SetName(".txt, \\tmpFilesFolder\\, \"abc\"");
    }

    [Test]
    [TestCaseSource(nameof(Data_for_TmpFile_can_be_created_given_extension_folder_and_content))]
    public void TmpFile_can_be_created_given_extension_folder_and_content(string[] input, string[] expected){
        var testTools = new TestTools();

        var x = new TmpFile(input[0], input[1], input[2]);
        x.AssignTestTools(testTools);
        Assert.NotNull(x);
        Assert.NotNull(x.FullPath);
        var fullPathFolder = Path.GetDirectoryName(x.FullPath);
        var fullPathFileName = Path.GetFileName(x.FullPath);
        var fullPathExtension = Path.GetExtension(x.FullPath);
        Assert.That(fullPathFolder, Is.EqualTo(expected[1]));
        Assert.That(fullPathFileName.Length, Is.GreaterThan(10));
        Assert.That(fullPathExtension, Is.EqualTo(expected[0]));

        Assert.That(File.Exists(x.FullPath).ToString(), Is.EqualTo(expected[2]));
    }

    [Test]
    public void FullPath_can_be_assigned_directly(){
        var x = new TmpFile();
        Assert.NotNull(x);
        Assert.That(x.Extension, Is.EqualTo(""));
        Assert.That(x.Filename.Length, Is.EqualTo(36));
        Assert.That(x.Folder, Is.EqualTo(Path.GetTempPath().TrimEnd(Path.DirectorySeparatorChar)));
        Assert.That(x.FullPath, Is.EqualTo(x.Folder + Path.DirectorySeparatorChar + x.Filename + x.Extension));

        x.FullPath = @"c:\dummyFolder\dummyFileName.dummyExtension";

        Assert.That(x.Extension, Is.EqualTo(".dummyExtension"));
        Assert.That(x.Filename, Is.EqualTo("dummyFileName"));
        Assert.That(x.Folder, Is.EqualTo(@"c:\dummyFolder"));
        Assert.That(x.FullPath, Is.EqualTo(@"c:\dummyFolder\dummyFileName.dummyExtension"));
    }

    [Test]
    public void If_TmpFile_points_to_a_existing_file_than_this_file_is_deleted_when_TmpFile_is_disposed(){
        var testTools = new TestTools();
        var filePath = testTools.GetNewTmpFilePath(".txt");

        Assert.False(File.Exists(filePath));

        File.WriteAllText(filePath, "abc");

        Assert.True(File.Exists(filePath));

        var x = new TmpFile{FullPath = filePath};

        Assert.True(File.Exists(filePath));

        x.Dispose();

        Assert.False(File.Exists(filePath));
    }

    private static string TestFolder(){
        return TestContext.CurrentContext.TestDirectory + @"\..\..";
    }
}
}