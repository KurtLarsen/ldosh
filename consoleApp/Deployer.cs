using System;
using System.IO;
using System.Linq;
using System.Xml;
using XmlHelperLib;
using ArgumentHandlerLib;

namespace consoleApp{
public class Deployer{
    private const string ProfileName = "p";

    private readonly string _projectName;
    private readonly string _projectRoot;
    private readonly string _requestedProfileName;

    public string ProjectName => _projectName;

    public string ProjectRoot => _projectRoot;

    public Deployer(ArgumentHandler argumentHandler){
        var deployerXmlDoc = new XmlDocument();
        deployerXmlDoc.Load(argumentHandler.Arguments.Find(x=>x.GetShortId=="px" && x.RawId!=null).Values[0]);


        /*
         * _projectRoot
         * 1. priority: command line argument -r
         * 2. priority: projectRoot-attribute in profileXml: <deployer projectRoot="xxx">
         * 3. priority: auto-detect via PathToProfileXml (works if profile.xml is placed somewhere inside project tree)
         * else fail
         */
        _projectRoot = argumentHandler.GetArgument("r")?.Values[0] ??
                       XmlHelper.GetOptionalAttr(XmlNames.ProjectRoot, deployerXmlDoc) ??
                       LocateProjectRoot(argumentHandler.GetArgument("px").Values[0]);

        /*
         * _projectName
         * 1. priority: command line argument -n
         * 2. priority: attribute "projectName" in <deployer> in profile.Xml
         * 3. priority: auto-detect via _profileRoot (use name of project folder as projectName)
         * else fail
         */
        _projectName = argumentHandler.GetArgument("pn")?.Values[0] ??
                       XmlHelper.GetOptionalAttr(XmlNames.ProjectName, deployerXmlDoc) ??
                       ExtractProjectName(_projectRoot);


        /*
         * _selectedProfileName
         * 1. priority: command line argument
         * 2. priority: attribute "defaultProfile" in <deployer> in profile.xml
         * 3. priority: if only one <profile> inside <deployer> than choose that
         * else fail
         */
        var deployerNode = XmlHelper.GetUniqueNode(XmlNames.Deployer, deployerXmlDoc);
        var profileList = XmlHelper.GetOneOrMoreNodes(XmlNames.Profile, deployerNode);
        
        _requestedProfileName = argumentHandler.GetArgument(ProfileName)?.Values[0] ??
                           XmlHelper.GetOptionalAttr(XmlNames.DefaultProfile, deployerNode);
        if (_requestedProfileName == null){
            if (profileList.Count == 1){
                _requestedProfileName = XmlHelper.GetAttr(XmlNames.ProfileName, profileList[0]);
            }
            else{
                throw new Exception("No profile selected");
            }
        }

        XmlNode selectedProfileNode=null;
        foreach (XmlNode profileNode in profileList){
            if (XmlHelper.GetAttr(XmlNames.ProfileName, profileNode).Equals(_requestedProfileName)){
                selectedProfileNode = profileNode;
                break;
            }
        }

        if (selectedProfileNode == null){
            throw new RequestedProfileNodeNotFoundException(_requestedProfileName);
        }
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