using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace TestToolsLib{
/*
 * For the use of IDisposable and Dispose() see https://stackoverflow.com/questions/12368/how-to-dispose-a-class-in-net
 */
public sealed class TestTools : IDisposable{
    private const string TmpFileMask = "{{tmpFiles[{0}]}}";
    private readonly List<Placeholder> _placeholderList = new List<Placeholder>();
    private readonly Regex _placeholderRegex;
    private readonly List<TmpFile> _tmpFileList = new List<TmpFile>();
    private bool _disposed;

    // constructor
    public TestTools(){
        _placeholderRegex = new Regex(@"\{.+\}");
    }

    public Placeholder[] Placeholders{
        get => _placeholderList.ToArray();
        set{
            _placeholderList.Clear();
            foreach (var placeholder in value) _placeholderList.Add(placeholder);
        }
    }

    /**
     * Call Dispose() to ensure that tmp-files are deleted
     */
    public void Dispose(){
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    // finalizer
    ~TestTools(){
        Dispose(false);
    }

    public static string GetTestFolder(){
        return Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..");
    }

    private void Dispose(bool disposing){
        if (!_disposed)
            if (disposing) // Dispose managed resources here.
                foreach (var tmpFile in _tmpFileList)
                    tmpFile.Dispose();

        // Dispose unmanaged resources here.

        _disposed = true;
    }

    public string ReplacePlaceholders(string s){
        if (s == null) return s;
        // replace {tmpFile[x]}
        for (var n = 0; n < _tmpFileList.Count; n++){
            // problem: _tmpFileList[0] er et TmpFile object, men det giver timeout, når man forsøger at hente extension, filename, folder og fullPath
            var oldValue = string.Format(TmpFileMask, n);
            var newValue = _tmpFileList[n].FullPath;
            s = s.Replace(oldValue, newValue);
        }

        // replace all known placeholders
        foreach (var placeholder in Placeholders) s = s.Replace(placeholder.Mask, placeholder.Value);

        // check for unresolved placeholders
        var match = _placeholderRegex.Match(s);
        if (match.Success) throw new Exception($"Unresolved placeholder: {match.Groups[0]} in \"{s}\"");
        return s;
    }

    public string GetNewTmpFilePath(string extension, string folder = null){
        if (folder == null) folder = Path.GetTempPath();
        if (extension == null) extension = "";
        var fullPath = folder + Guid.NewGuid() + extension;
        _tmpFileList.Add(new TmpFile{FullPath = fullPath}); // remember, so it will bee cleaned up on dispose()
        return fullPath;
    }

    public void SetupTFiles(IEnumerable<TFile> tFiles){
        foreach (var tFile in tFiles)
            _tmpFileList.Add(
                new TmpFile(
                    ReplacePlaceholders(tFile.Extension),
                    ReplacePlaceholders(tFile.Folder),
                    ReplacePlaceholders(tFile.Content)
                )
            );
    }

    public string ReplacePlaceholders2(string s){
        for (var n = 0; n < _tmpFileList.Count; n++){
            var needle = $"{{TmpFile[{n}]}}";
            s = s.Replace(needle, _tmpFileList[n].FullPath);
        }

        foreach (var placeholder in Placeholders) s = s.Replace(placeholder.Mask, placeholder.Value);

        return s;
    }
}

public readonly struct Placeholder{
    public readonly string Mask;
    public readonly string Value;

    public Placeholder(string mask, string value){
        Mask = mask;
        Value = value;
    }
}
}