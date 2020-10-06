using System;

namespace ArgumentHandlerLib{
public class ArgumentGivenMoreThanOnceException:ArgumentException{
    public const string MsgMask = "Argument given more than once: \"{0}\"";
    public const int Code = 1;
    
    public ArgumentGivenMoreThanOnceException(Argument argument):base(MsgMask,argument.RawId){ }

    public override int ErrCode(){
        return Code;
    }
}
}