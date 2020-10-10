using System;
using ArgumentException = ArgumentHandlerLib.exceptions.ArgumentException;

namespace ArgumentHandlerLib{
public class ArgumentIdExpected:ArgumentException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Argument id expected. Found \"{0}\"";
    public const int ErrCode = 2;

    public ArgumentIdExpected(string unexpectedElement) : base(ErrCode,MsgMask, unexpectedElement){ }
    
}
}