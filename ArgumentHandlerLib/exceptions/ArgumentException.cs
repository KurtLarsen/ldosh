using System;

namespace ArgumentHandlerLib.exceptions{
public abstract class ArgumentException : MyException.MyException{

    public ArgumentException(string msgMask, params Object[] args) : base(msgMask, args){ }

}
}