using System;

namespace ArgumentHandlerLib.exceptions{
public abstract class ArgumentException : MyException.MyException{

    public ArgumentException(int code, string msgMask, params Object[] args) : base(code,msgMask, args){ }

}
}