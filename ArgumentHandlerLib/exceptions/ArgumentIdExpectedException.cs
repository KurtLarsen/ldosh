namespace ArgumentHandlerLib.exceptions{
public class ArgumentIdExpectedException:ArgumentException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Argument id expected. Found \"{0}\"";
    public const int ErrCode = 2;

    public ArgumentIdExpectedException(string unexpectedElement) : base(ErrCode,MsgMask, unexpectedElement){ }
    
}
}