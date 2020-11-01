// #define PRINT

using System;
using System.IO;

namespace TestToolsLib{
public class TmpFile{
    private bool _disposed;
    private string _extension;
    private string _filename;
    private string _folder;
    private TestTools _testTools;

    /**
     * Constructor
     */
    public TmpFile(){ }

    /**
     * Constructor
     */
    public TmpFile(string extension){
        _extension = extension ?? "";
    }

    /**
     * Constructor
     */
    public TmpFile(string extension, string folder){
        _extension = extension;
        _folder = folder;
    }

    /**
     * Constructor
     */
    public TmpFile(string extension, string folder, string content){
        _extension = extension;
        _folder = folder;

        if (content != null)
            File.WriteAllText(FullPath, content);
    }


    /**
     * NO! Folder always ends with Path.DirectorySeparatorChar ("\" or "/")
     * If _folder is null Path.GetTempPath() is returned
     */
    public string Folder{
        get{
            var s = _folder ?? Path.GetTempPath();
            if (s == "") s = Directory.GetCurrentDirectory();
            s = s.TrimEnd(Path.DirectorySeparatorChar);
            if (_testTools != null) s = _testTools.ReplacePlaceholders(s);

            return s;
        }
    }

    public string Filename{
        get{
            if (_filename == null) _filename = Guid.NewGuid().ToString();

            var s = _filename;
            if (_testTools != null) s = _testTools.ReplacePlaceholders(s);

            return s;
        }
    }

    public string Extension{
        get{
            var s = _extension ?? "";
            if (!s.Equals("") && !s.StartsWith(".")) s = "." + s;
            if (_testTools != null) s = _testTools.ReplacePlaceholders(s);

            return s;
        }
    }


    public string FullPath{
        get => Folder + Path.DirectorySeparatorChar + Filename + Extension;
        set{
            _folder = Path.GetDirectoryName(value); // trailing directorySeparator is trimmed
            _filename = Path.GetFileNameWithoutExtension(value);
            _extension = Path.GetExtension(value);
        }
    }


    ~TmpFile(){
#if DEBUG && PRINT
        Console.Out.WriteLine("Called TmpFile.~TmpFile()");
#endif


        Dispose(false);
    }

    public void Dispose(){
#if DEBUG && PRINT
        Console.Out.WriteLine("Called TmpFile.Dispose()");
#endif
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // ReSharper disable once UnusedParameter.Local
    private void Dispose(bool disposing){
#if DEBUG && PRINT
        Console.Out.WriteLine($"Called TmpFile.Dispose({disposing})");
#endif

        if (!_disposed){
            // if (disposing){
            //     // Dispose managed resources here.
            //     // _disposing is True when this function is called from Dispose()
            //     // _disposing is False when this function is called from ~TmpFile()
            //     if(File.Exists(_fullPath)) File.Delete(_fullPath);
            // }
#if DEBUG && PRINT
            Console.Out.WriteLine($"\t_fullPath = {_fullPath}");
            Console.Out.WriteLine($"\tFile.Exists(_fullPath) = {File.Exists(_fullPath)}");
#endif
            // Dispose unmanaged resources here.
            if (File.Exists(FullPath)){
#if DEBUG && PRINT
                Console.Out.WriteLine("\tDeleting file...");
#endif
                File.Delete(FullPath);
            }
        }

        _disposed = true;
    }

    public void AssignTestTools(TestTools testTools){
        _testTools = testTools;
    }
}
}