namespace ArgumentHandlerLib.exceptions{
public class UnknownArgumentIdException : ArgumentException{
    public const string MsgMask = "Unknown argument id: \"{0}\"";
    public const int ErrCode = 5;

    public UnknownArgumentIdException(string argumentId) : base(ErrCode, MsgMask, argumentId){ }
}
}