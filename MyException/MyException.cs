using System;

namespace MyException{
public class MyException : Exception{
    private readonly int _code;

    // constructor
    protected MyException(int code, string msgMask, params object[] args) : base(Msg(msgMask, args)){
        _code = code;
    }

    private static string Msg(string mask, params object[] args){
        return string.Format(mask, args);
    }

    public int Code(){
        return _code;
    }
}
}