using System;

namespace ArgumentHandler{
public class MissingRequiredArgumentException:Exception{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Required argument not found: \"{0}\"";
    
    public MissingRequiredArgumentException(Argument argument):base(Msg(argument)){ }

    private static string Msg(Argument argument){
        return string.Format(MsgMask, argument.GetShortId);

    }

}
}