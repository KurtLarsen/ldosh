using System;

namespace ArgumentHandlerLib{
public class UnknownArgumentIdException:ArgumentException{
    public const string MsgMask = "Unknown argument id: \"{0}\"";
    public const int Code = 4;

    public UnknownArgumentIdException(string argumentId):base(MsgMask,argumentId){ }
    
    public override int ErrCode(){
        return Code;
    }
}
}