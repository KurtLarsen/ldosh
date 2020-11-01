namespace consoleApp.exceptions{

/**
 * This class serves as a general exception type for all inheritors
 * It is never called direct
 */
public class DeployerException : MyException.MyException{
    protected DeployerException(int code,string msgMask, params object[] args) : base(code,msgMask, args){ }
}
}