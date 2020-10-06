using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace TestToolsLib{

/*
 * For the use of IDisposable and Dispose() see https://stackoverflow.com/questions/12368/how-to-dispose-a-class-in-net
 */
public sealed class TestTools : IDisposable{
    private bool _disposed;
    private readonly List<string> _tmpFileList = new List<string>();

    // constructor
    public TestTools(){ }

    public string GetNewTmpFilePath(string extension, string folder = null){
        if (folder == null) folder = Path.GetTempPath();
        var tmpFilePath = folder + Guid.NewGuid() + extension;

        _tmpFileList.Add(tmpFilePath);

        return tmpFilePath;
    }

    // finalizer
    ~TestTools(){
        this.Dispose(false);
    }

    public string GetTestFolder(){
        return Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\");
    }

    public void Dispose(){
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing){
        if (!_disposed){
            if (disposing){
                foreach (var file in _tmpFileList)
                    File.Delete(file);
            }
            // Dispose unmanaged resources here.
        }
        _disposed = true;
    }
}
}