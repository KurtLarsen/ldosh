using System;

namespace MyException{
public class MyException:Exception{
    protected int _code;
    
    public int Code => _code;

    // constructor
    public MyException(string msgMask, params Object[] args) : base(Msg(msgMask, args)){ }

    private static string Msg(string mask, params Object[] args){
        return string.Format(mask, args);
    }


}
}