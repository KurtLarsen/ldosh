using System;
using System.IO;
using System.Linq;
using System.Xml;
using ArgumentHandlerLib;
using consoleApp.exceptions;
using XmlHelperLib;

namespace consoleApp{
public class Deployer{
    private const string ProfileName = "p";

    private static readonly string[] LaravelFolders =
        {"app", "bootstrap", "config", "database", "public", "resources", "routes", "storage", "vendor"};

    private readonly string _projectName;
    private readonly string _projectRoot;

    public Deployer(ArgumentHandler argumentHandler){
        var optionFile = argumentHandler.GetArgument("px").Values[0];
        var deployerXmlDoc = new XmlDocument();
        deployerXmlDoc.Load(optionFile);
        var deployerNode = XmlHelper.GetUniqueNode(XmlNames.Deployer, deployerXmlDoc);
        
        /*
         * _projectRoot
         * 1. priority: command line argument -pr
         * 2. priority: projectRoot-attribute in profileXml: <deployer projectRoot="xxx">
         * 3. priority: auto-detect via PathToProfileXml (works if profile.xml is placed somewhere inside project tree)
         * else fail
         */
        _projectRoot = argumentHandler.GetArgument("pr")?.Values[0] ??
                       XmlHelper.GetOptionalAttr(XmlNames.ProjectRoot, deployerNode) ??
                       LocateProjectRoot(optionFile) ??
                       throw new LaravelProjectRootNotFoundException();

        /*
         * _projectName
         * 1. priority: command line argument -n
         * 2. priority: attribute "projectName" in <deployer> in profile.Xml
         * 3. priority: auto-detect via _profileRoot (use name of project folder as projectName)
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
        var profileList = XmlHelper.GetOneOrMoreNodes(XmlNames.Profile, deployerNode);

        var requestedProfileName = argumentHandler.GetArgument(ProfileName)?.Values[0] ??
                                   XmlHelper.GetOptionalAttr(XmlNames.DefaultProfile, deployerNode);
        if (requestedProfileName == null){
            if (profileList.Count == 1)
                requestedProfileName = XmlHelper.GetAttr(XmlNames.ProfileName, profileList[0]);
            else
                throw new Exception("No profile selected");
        }

        XmlNode selectedProfileNode = null;
        foreach (XmlNode profileNode in profileList){
            if (!XmlHelper.GetAttr(XmlNames.ProfileName, profileNode).Equals(requestedProfileName)) continue;
            selectedProfileNode = profileNode;
            break;
        }

        if (selectedProfileNode == null) throw new RequestedProfileNodeNotFoundException(requestedProfileName);
    }

    public string ProjectName => _projectName;

    public string ProjectRoot => _projectRoot;

    // ReSharper disable once MemberCanBePrivate.Global
    public static string ExtractProjectName(string pathToProjectRoot){
        return pathToProjectRoot.Split('\\').Last();
    }

    public static string LocateProjectRoot(string pathToEntryInProject){
        if (File.Exists(pathToEntryInProject)){
            var fi = new FileInfo(pathToEntryInProject);
            pathToEntryInProject = fi.DirectoryName;
        }

        if (pathToEntryInProject == null) return null;

        var di = new DirectoryInfo(pathToEntryInProject);
        while (di != null && !IsProjectFolder(di)) di = di.Parent;

        return di?.FullName;
    }

    public static bool IsProjectFolder(DirectoryInfo di){
        if (di == null) return false;
        if (!di.Exists) return false;
        var subDirs = di.GetDirectories();
        foreach (var laravelFolder in LaravelFolders) // matcher laravelFolder ét af elementerne i subDirs ?
            if (!Array.Exists(subDirs, subDirsItem => subDirsItem.Name.Equals(laravelFolder)))
                return false;

        return true;
    }
}
}