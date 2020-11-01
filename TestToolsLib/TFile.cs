namespace TestToolsLib{
// ReSharper disable once InconsistentNaming
public class TFile{
    public readonly string Content;
    public readonly string Extension;
    public readonly string Folder;

    public TFile(string extension, string folder, string content){
        Extension = extension;
        Folder = folder;
        Content = content;
    }
}
}