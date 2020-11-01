namespace ArgumentHandlerLib.exceptions{
public class MissingRequiredArgumentException : ArgumentException{
    public const string MsgMask = "Required argument not found: \"-{0}\"";
    public new const int Code = 3;

    public MissingRequiredArgumentException(Argument argument) : base(Code, MsgMask, argument.GetShortId){ }
}
}