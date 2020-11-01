using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MyExceptionList{
internal static class Program{
    private static Regex _myExceptionPattern;
    private static DirectoryInfo _rootDi;
    private static StreamWriter _file;

    // ReSharper disable once UnusedParameter.Global
    public static void Main(string[] args){
        _myExceptionPattern = new Regex(@"class\s+(\S+)\s*:\s*(\S+)\s*{.+int\s+ErrCode\s*=\s*(\d+);", RegexOptions.Singleline);
        _rootDi = new DirectoryInfo(@"c:\projects\LaravelDeployOnSharedHost");
        var textFile = _rootDi.FullName + @"\exceptions.txt";
        using (_file = new StreamWriter(textFile)){
            GetExGroupsRecursive(_rootDi);
        }

        var content = File.ReadAllLines(textFile);
        Array.Sort(content);
        File.WriteAllLines(textFile, content);
    }


    private static void GetExGroupsRecursive(DirectoryInfo di){
        foreach (var subDirInfo in di.GetDirectories()) GetExGroupsRecursive(subDirInfo);

        foreach (var fileInfo in di.GetFiles("*.cs")){
            var m = _myExceptionPattern.Match(File.ReadAllText(fileInfo.FullName));
            if (m.Success) _file.WriteLine(m.Groups[2] + "\t" + m.Groups[3] + "\t" + m.Groups[1]);
        }
    }
}
}