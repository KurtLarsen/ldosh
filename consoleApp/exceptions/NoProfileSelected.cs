namespace consoleApp.exceptions{
// ReSharper disable once UnusedType.Global
public class NoProfileSelected : DeployerException{
    public new const int Code = 1;

    private const string MsgMask = "No Profile Selected";

    public NoProfileSelected(params object[] args) : base(Code, MsgMask, args){ }
}
}