namespace TestToolsLib.exceptions{
// ReSharper disable once UnusedType.Global
public class TmpFolderNotSet : TestException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string Msg = "TmpFolder not set";

    // ReSharper disable once MemberCanBePrivate.Global
    public const int ErrCode = 2;

    public TmpFolderNotSet() : base(ErrCode, Msg){ }
}
}