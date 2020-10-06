using System;

namespace ArgumentHandlerLib{
public class ArgumentIdExpected:ArgumentException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Argument id expected. Found \"{0}\"";
    public const int Code = 2;
    
    public ArgumentIdExpected(string unexpectedElement):base(MsgMask,unexpectedElement){ }
    
    public override int ErrCode(){
        return Code;
    }
}
}