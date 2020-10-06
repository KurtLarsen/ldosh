using System;

namespace ArgumentHandlerLib{
public class MissingRequiredArgumentException:ArgumentException{
    public const string MsgMask = "Required argument not found: \"-{0}\"";
    public const int Code = 3;
    
    public MissingRequiredArgumentException(Argument argument):base( MsgMask,argument.GetShortId){ }


    public override int ErrCode(){
        return Code;
    }
}
}