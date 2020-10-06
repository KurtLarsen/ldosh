using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace TestLib{
public class TestTools{
    private readonly List<string> _tmpFileList=new List<string>();

    public TestTools(){ }

    public string GetNewTmpFilePath(string extension, string folder = null){
        if (folder == null) folder = Path.GetTempPath();
        var tmpFilePath = folder + Guid.NewGuid() + extension;

        _tmpFileList.Add(tmpFilePath);

        return tmpFilePath;
    }

    // finalizer
    ~TestTools(){
        foreach (var file in _tmpFileList)
            File.Delete(file);

    }

    public string GetTestDataFolder(){
        return Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\"); 
    }
}
}