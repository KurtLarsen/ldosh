namespace consoleApp.exceptions{
public class RequestedProfileNodeNotFoundException : DeployerException{
    public new const int Code = 2;

    private const string MsgMask = "Requested Profile Node Not Found";

    // constructor
    public RequestedProfileNodeNotFoundException(params object[] args) : base( Code,MsgMask, args){ }
}
}