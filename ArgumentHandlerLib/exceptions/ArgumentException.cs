namespace ArgumentHandlerLib.exceptions{
public class ArgumentException : MyException.MyException{
    /**
    * This class serves as a general exception type for all inheritors
    * It is never called direct
    */
    protected ArgumentException(int code, string msgMask, params object[] args) : base(code, msgMask, args){ }
}
}
