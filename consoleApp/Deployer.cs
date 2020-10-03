using System;
using System.IO;
using System.Linq;
using System.Xml;
using XmlHelperLib;

namespace consoleApp{
public class Deployer{

    private readonly string _projectName;
    private readonly string _projectRoot;
    private readonly string _defaultProfile;

    public string ProjectName => _projectName;

    public string ProjectRoot => _projectRoot;

    public Deployer(ArgumentHandler.ArgumentHandler argumentHandler){
        var deployerXmlDoc = new XmlDocument();
        deployerXmlDoc.Load(argumentHandler.Arguments.Find(x=>x.GetShortId=="px" && x.RawId!=null).Values[0]);


        /*
         * Set _projectRoot
         * 1. priority: command line argument -r
         * 2. priority: projectRoot-attribute in profileXml: <deployer projectRoot="xxx">
         * 3. priority: auto-detect via PathToProfileXml 
         */
        _projectRoot = argumentHandler.Arguments.Find(x=>x.GetShortId=="r" && x.RawId!=null)?.Values[0] ??
                       XmlHelper.GetOptionalAttr(XmlNames.ProjectRoot, deployerXmlDoc) ??
                       LocateProjectRoot(argumentHandler.Arguments.Find(x=>x.GetShortId=="px" && x.RawId!=null).Values[0]);

        /*
         * Set _projectName
         * 1. priority: command line argument -n
         * 2. priority: <deployer> attribute "projectName" in file profileXml
         * 3. priority: auto-detect via _profileRoot
         */
        _projectName = argumentHandler.Arguments.Find(x=>x.GetShortId=="pn" && x.RawId!=null)?.Values[0] ??
                       XmlHelper.GetOptionalAttr(XmlNames.ProjectName, deployerXmlDoc) ??
                       ExtractProjectName(_projectRoot);

        _defaultProfile = XmlHelper.GetOptionalAttr(XmlNames.DefaultProfile, deployerXmlDoc);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static string ExtractProjectName(string pathToProjectRoot){
        return  pathToProjectRoot.Split('\\').Last();
    }

    public static string LocateProjectRoot(string pathToEntryInProject){
        if (File.Exists(pathToEntryInProject)){
            var fi = new FileInfo(pathToEntryInProject);
            pathToEntryInProject = fi.DirectoryName;
        }

        if (pathToEntryInProject == null) return null;
        
        var di = new DirectoryInfo(pathToEntryInProject);
        while (di != null && !IsProjectFolder(di)){
            di = di.Parent;
        }

        return di?.FullName;
    }

    private static readonly string[] LaravelFolders =
        {"app", "bootstrap", "config", "database", "public", "resources", "routes", "storage", "vendor"};

    public static bool IsProjectFolder(DirectoryInfo di){
        if (di == null) return false;
        if (!di.Exists) return false;
        var subdirs = di.GetDirectories();
        foreach (var laravelFolder in LaravelFolders){
            // matcher laravelFolder ét af elementerne i subdirs ?
            if (!Array.Exists(subdirs, subdirsItem => subdirsItem.Name.Equals(laravelFolder))) return false;
        }

        return true;
    }
}
}