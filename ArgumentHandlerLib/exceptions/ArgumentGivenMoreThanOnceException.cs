namespace ArgumentHandlerLib.exceptions{
public class ArgumentGivenMoreThanOnceException:ArgumentException{
    public const string MsgMask = "Argument given more than once: \"{0}\"";
    public const int ErrCode = 1;

    public ArgumentGivenMoreThanOnceException(Argument argument) : base(MsgMask, argument.RawId){
        _code = ErrCode;
    }

}
}