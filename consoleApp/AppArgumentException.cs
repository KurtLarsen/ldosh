using System;

namespace consoleApp{
[Obsolete]
public class AppArgumentException : Exception{
    public const int HelpRequestCode = 1;
    public const int UnknownArgumentCode = 2;
    public const int PxOptionNotFoundCode = 3;
    public const int FileNotFoundCode = 4;
    public const int FolderNotFoundCode = 5;
    public const int InvalidValueCode = 6;

    public const string HelpRequestMsg = "Help Request";
    public const string UnknownArgumentMsg = "Unknown argument: {0}";
    public const string PxOptionNotFoundMsg = "-px option not found";
    public const string FileNotFoundMsg = "File not found: {0}";
    public const string FolderNotFoundMsg = "Folder not found: {0}";
    public const string UnknownErrorCodeMsg = "Unknown ErrorCode: {0}";
    public const string InvalidValueMsg = "Invalid value {0}";

    private readonly int _errorCode;

    public AppArgumentException(int errorCode) : base(MessageGenerator(errorCode)){
        _errorCode = errorCode;
    }


    public AppArgumentException(int errorCode, string data) : base(MessageGenerator(errorCode, data)){
        _errorCode = errorCode;
    }

    private static string MessageGenerator(int errorCode, string data = null){
        switch (errorCode){
            case HelpRequestCode: return HelpRequestMsg;
            case UnknownArgumentCode: return string.Format(UnknownArgumentMsg, data);
            case PxOptionNotFoundCode: return PxOptionNotFoundMsg;
            case FileNotFoundCode: return string.Format(FileNotFoundMsg, data);
            case FolderNotFoundCode: return string.Format(FolderNotFoundMsg, data);
            case InvalidValueCode: return string.Format(InvalidValueMsg, data);
            default:
                throw new Exception(string.Format(UnknownErrorCodeMsg, data));
        }
    }

    public int ErrorCode => _errorCode;
}
}