using System;
using System.CodeDom;
using ArgumentException = ArgumentHandlerLib.exceptions.ArgumentException;

namespace ArgumentHandlerLib{
public class MissingValuesException : ArgumentException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Value(s) missing for argument {0}. Expected {1}. Found: {2}";
    public const int ErrCode = 4;

    public MissingValuesException(Argument argument) : base(ErrCode,MsgMask, argument.RawId,
        argument.RequiredValueCountAsString(), argument.GivenValuesAsString()){ }

    
}
}