using System;
using ArgumentException = ArgumentHandlerLib.exceptions.ArgumentException;

namespace ArgumentHandlerLib{
public class MissingRequiredArgumentException:ArgumentException{
    public const string MsgMask = "Required argument not found: \"-{0}\"";
    public const int ErrCode = 3;

    public MissingRequiredArgumentException(Argument argument) : base(ErrCode, MsgMask, argument.GetShortId){ }


}
}