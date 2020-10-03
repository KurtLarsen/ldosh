using System;

namespace ArgumentHandler{
public class ArgumentIdExpected:Exception{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Argument id expected. Found \"{0}\"";
    
    public ArgumentIdExpected(string unexpectedElement):base(Msg(unexpectedElement)){ }

    private static string Msg(string unexpectedElement){
        return string.Format(MsgMask, unexpectedElement);

    }
}
}