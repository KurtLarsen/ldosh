using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace MyExceptionList{
internal class Program{
    private static Regex _myExceptionPattern;
    private static DirectoryInfo _rootDi;
   private static StreamWriter _file;
   
    public static void Main(string[] args){
        RegexOptions options = RegexOptions.Singleline;
        _myExceptionPattern=new Regex(@"class\s+(\S+)\s*:\s*(\S+)\s*{.+int\s+ErrCode\s*=\s*(\d+);",options);
        _rootDi=new DirectoryInfo(@"c:\projects\LaravelDeployOnSharedHost");
        var textFile = _rootDi.FullName + @"\exceptions.txt";
        using (_file = new System.IO.StreamWriter(textFile)){
            GetExGroupsRecursive(_rootDi);
        }

        var content = File.ReadAllLines(textFile);
        Array.Sort(content);
        File.WriteAllLines(textFile, content);

    }


    private static void GetExGroupsRecursive(DirectoryInfo di){
        foreach (var subDirInfo in di.GetDirectories()){
            GetExGroupsRecursive(subDirInfo);
        }
        
        foreach (var fileInfo in di.GetFiles("*.cs")){
            var m = _myExceptionPattern.Match(File.ReadAllText(fileInfo.FullName));
            if(m.Success){
                _file.WriteLine(m.Groups[2]+"\t"+m.Groups[3]+"\t"+m.Groups[1]);
            }
        }
    }

    
}
}