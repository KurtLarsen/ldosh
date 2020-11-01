namespace XmlHelperLib.exceptions{

/**
 * This class serves as a general exception type for all inheritors
 * It is never called direct
 */
public class XmlHelperException : MyException.MyException{
    // constructor
    protected XmlHelperException(int code, string msgMask, params object[] args) : base(code, msgMask, args){ }
}
}