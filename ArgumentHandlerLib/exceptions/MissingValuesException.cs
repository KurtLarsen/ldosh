using System;
using System.CodeDom;

namespace ArgumentHandlerLib{
public class MissingValuesException : ArgumentException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Value(s) missing for argument {0}. Expected {1}. Found: {2}";
    public const int Code = 4;

    public MissingValuesException(Argument argument) : base(MsgMask,argument.RawId,argument.RequiredValueCountAsString(),argument.GivenValuesAsString()){ }

    
    public override int ErrCode(){
        return Code;
    }
}
}