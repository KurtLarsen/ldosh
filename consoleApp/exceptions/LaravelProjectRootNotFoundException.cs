namespace consoleApp.exceptions{
public class LaravelProjectRootNotFoundException:DeployerException{
    public new const int Code = 1;

    // ReSharper disable once MemberCanBePrivate.Global
    private const string MsgMask = "Project Root Not Found";

    public LaravelProjectRootNotFoundException() : base(Code,MsgMask){ }
}
}