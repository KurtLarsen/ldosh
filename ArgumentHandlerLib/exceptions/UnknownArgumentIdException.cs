namespace ArgumentHandlerLib.exceptions{
public class UnknownArgumentIdException:ArgumentException{
    public const string MsgMask = "Unknown argument id: \"{0}\"";
    public const int ErrCode = 4;

    public UnknownArgumentIdException(string argumentId) : base(MsgMask, argumentId){
        _code = ErrCode;
    }
    
}
}