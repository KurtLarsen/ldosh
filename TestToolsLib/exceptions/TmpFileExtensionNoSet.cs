namespace TestToolsLib.exceptions{
// ReSharper disable once UnusedType.Global
public class TmpFileExtensionNoSet : TestException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string Msg = "TmpFile extension not set";

    // ReSharper disable once MemberCanBePrivate.Global
    public const int ErrCode = 3;
    public TmpFileExtensionNoSet() : base(ErrCode, Msg){ }
}
}