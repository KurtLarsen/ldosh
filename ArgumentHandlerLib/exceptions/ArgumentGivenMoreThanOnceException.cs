namespace ArgumentHandlerLib.exceptions{
public class ArgumentGivenMoreThanOnceException : ArgumentException{
    public const string MsgMask = "Argument given more than once: \"{0}\"";
    public new const int Code = 1;

    public ArgumentGivenMoreThanOnceException(Argument argument) : base(Code, MsgMask, argument.RawId){ }
}
}