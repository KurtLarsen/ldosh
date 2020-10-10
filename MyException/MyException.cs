using System;
using System.Xml.Serialization;

namespace MyException{
public abstract class MyException:Exception{
    private readonly int _code;
    
    public int Code => _code;

    // constructor
    public MyException(int code, string msgMask, params Object[] args) : base(Msg(msgMask, args)){
        _code = code;
    }

    private static string Msg(string mask, params Object[] args){
        return string.Format(mask, args);
    }


}
}