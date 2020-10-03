using System;
using System.IO;

namespace consoleApp{
[Obsolete]
public class ConsoleAppArguments{
    private readonly string _pathToProfileXml;
    private string _profileName;
    private readonly string _pathToServersXml;
    private string _domain;
    private string _database;
    private readonly string _pathToProjectRoot;
    private readonly string _projectName;

    
    public string PathToProfileXml => _pathToProfileXml;
    public string PathToServersXml => _pathToServersXml;
    public string PathToProjectRoot => _pathToProjectRoot;

    public string ProjectName => _projectName;

    // constructor
    public ConsoleAppArguments(string[] args){

        if (args.Length == 0) throw new AppArgumentException(AppArgumentException.HelpRequestCode);

        var index = 0;
        while (index < args.Length){
            switch (args[index++]){
                case "-h":
                case "-?":
                    throw new AppArgumentException(AppArgumentException.HelpRequestCode);
                case "-px":
                    _pathToProfileXml = value(args[index++]);
                    break;
                case "-p":
                    _profileName = value(args[index++]);
                    break;
                case "-sx":
                    _pathToServersXml = value(args[index++]);
                    break;
                case "-d":
                    _domain = value(args[index++]);
                    break;
                case "-db":
                    _database = value(args[index++]);
                    break;
                case "-r":
                    _pathToProjectRoot = value(args[index++]);
                    break;
                case "-pn":
                    _projectName = value(args[index++]);
                    break;
                default:
                    throw new AppArgumentException(AppArgumentException.UnknownArgumentCode, args[index - 1]);
            }
        }

        if (_pathToProfileXml == null)
            throw new AppArgumentException(AppArgumentException.PxOptionNotFoundCode);


        if (!File.Exists(_pathToProfileXml))
            throw new AppArgumentException(AppArgumentException.FileNotFoundCode, _pathToProfileXml);

        if (_pathToServersXml != null && !File.Exists(_pathToServersXml)){
            throw new AppArgumentException(AppArgumentException.FileNotFoundCode, _pathToServersXml);
        }

        if (_pathToProjectRoot != null && !Directory.Exists(_pathToProjectRoot)){
            throw new AppArgumentException(AppArgumentException.FolderNotFoundCode, _pathToProjectRoot);
        }
    }

    private string value(string arg){
        if (arg.StartsWith("-")){
            throw new AppArgumentException(AppArgumentException.InvalidValueCode,arg);
        }

        return arg;
    }

}
}