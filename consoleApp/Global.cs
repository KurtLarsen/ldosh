using System;
using System.IO;
using System.Xml;

namespace consoleApp{
public static class Global{
    public const int EXITCODE_OK = 0;
    public const int EXITCODE_HELP = 1;
    public const int EXITCODE_UNKNOWN_ARGUMENT = 2;
    public const int EXITCODE_MISSING_PX_ARGUMENNT = 3;
    public const int EXITCODE_PX_FILE_NOT_FOUND = 4;
    public const int EXITCODE_SX_FILE_NOT_FOUND = 5;
    public const int EXITCODE_ROOT_NOT_FOUND = 6;
    public const int EXITCODE_XML_ERROR = 7;

    internal static readonly string HelpMsg = $"-px <path to profile.xml>{Environment.NewLine}" +
                                              $"-p  <profile name>{Environment.NewLine}" +
                                              $"-sx <path to servers.xml{Environment.NewLine}" +
                                              $"-d  <domain>{Environment.NewLine}" +
                                              $"-db <database>{Environment.NewLine}" +
                                              $"-r  <path to project root>{Environment.NewLine}";

    private static readonly string UNKNOWN_OPTION = "Unknown option: {0}";
    private static readonly string PX_OPTION_NOT_FOUND = "-px <pathToProfileXml> option not found";
    private static readonly string FILE_NOT_FOUND = "File not found: {0}";
    private static readonly string DIRECTORY_NOT_FOUND = "Directory not found: {0}";


    public static readonly string RequiredNotFound = "Required argument not found: -px <pathToProfileXml>";

    private static string _pathToProfileXml;
    private static string _profileName;
    private static string _pathToServersXml;
    private static string _domain;
    private static string _database;
    private static string _pathToProjectRoot;
    private static string _errorMessage;
    private static int _exitCode;

    public static string PathToProfileXml => _pathToProfileXml;

    public static string ErrorMessage => _errorMessage;

    public static int ExitCode => _exitCode;


    public static bool ArgumentParser(string[] args){
        if (args.Length == 0){
            _errorMessage = HelpMsg;
            _exitCode = EXITCODE_HELP;
            return false;
        }

        var index = 0;
        while (index < args.Length){
            switch (args[index++]){
                case "-h":
                case "-?":
                    _errorMessage = HelpMsg;
                    _exitCode = EXITCODE_HELP;
                    return false;
                case "-px":
                    _pathToProfileXml = args[index++];
                    break;
                case "-p":
                    _profileName = args[index++];
                    break;
                case "-sx":
                    _pathToServersXml = args[index++];
                    break;
                case "-d":
                    _domain = args[index++];
                    break;
                case "-db":
                    _database = args[index++];
                    break;
                case "-r":
                    _pathToProjectRoot = args[index++];
                    break;
                default:
                    _errorMessage = String.Format(UNKNOWN_OPTION, args[index - 1]);
                    _exitCode = EXITCODE_UNKNOWN_ARGUMENT;
                    return false;
            }
        }

        if (PathToProfileXml == null){
            _errorMessage = PX_OPTION_NOT_FOUND;
            _exitCode = EXITCODE_MISSING_PX_ARGUMENNT;
            return false;
        }

        if (!File.Exists(PathToProfileXml)){
            _errorMessage = string.Format(FILE_NOT_FOUND, PathToProfileXml);
            _exitCode = EXITCODE_PX_FILE_NOT_FOUND;
            return false;
        }

        if (_pathToServersXml != null && !File.Exists(_pathToServersXml)){
            _errorMessage = string.Format(FILE_NOT_FOUND, _pathToServersXml);
            _exitCode = EXITCODE_SX_FILE_NOT_FOUND;
            return false;
        }

        if (_pathToProjectRoot != null && !Directory.Exists(_pathToProjectRoot)){
            _errorMessage = string.Format(DIRECTORY_NOT_FOUND, _pathToProjectRoot);
            _exitCode = EXITCODE_ROOT_NOT_FOUND;
            return false;
        }

        return true;
    }

    public static bool ProfilesLoad(){
        try{
            var profilesDoc = new XmlDocument();
            profilesDoc.Load(PathToProfileXml);
        }
        catch (Exception exception){
            _errorMessage = exception.Message;
            _exitCode = EXITCODE_XML_ERROR;
            return false;
        }

        return true;
    }
}
}