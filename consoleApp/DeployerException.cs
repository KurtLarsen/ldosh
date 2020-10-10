namespace consoleApp{
public class DeployerException:MyException.MyException{
    public DeployerException(int code, string msgMask, params object[] args) : base(code,msgMask, args){ }
}
}