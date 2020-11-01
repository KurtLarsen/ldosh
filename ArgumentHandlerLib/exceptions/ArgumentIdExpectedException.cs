namespace ArgumentHandlerLib.exceptions{
public class ArgumentIdExpectedException : ArgumentException{
    public const string MsgMask = "Argument id expected. Found \"{0}\"";
    public new const int Code = 2;

    public ArgumentIdExpectedException(string unexpectedElement) : base(Code, MsgMask, unexpectedElement){ }
}
}