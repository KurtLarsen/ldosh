using System;
using System.Net.Configuration;

namespace ArgumentHandlerLib{
public abstract class ArgumentException : Exception{

    public ArgumentException(string msgMask, params Object[] args) : base(Msg(msgMask, args)){ }

    public abstract int ErrCode();

    private static string Msg(string mask, params Object[] args){
        return string.Format(mask, args);
    }
}
}