namespace consoleApp{
public class NoProfileSelected:DeployerException{
    public static int Code = 1;
    public static string MsgMask = "No Profile Selected";
    
    public NoProfileSelected(params object[] args) : base(Code, MsgMask, args){ }
}
}