using System;

namespace ArgumentHandler{
public class ArgumentGivenMoreThanOnceException:Exception{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Argument given more than once: \"{0}\"";
    
    public ArgumentGivenMoreThanOnceException(Argument argument):base(Msg(argument)){ }

    private static string Msg(Argument argument){
        return string.Format(MsgMask, argument.RawId);

    }

}
}