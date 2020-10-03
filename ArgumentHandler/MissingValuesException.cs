using System;

namespace ArgumentHandler{
public class MissingValuesException : Exception{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Value(s) missing for argument {0}. Expected {1}. Found: {2}";

    public MissingValuesException(Argument argument) : base(Msg(argument)){ }

    private static string Msg(Argument argument){
        return string.Format(MsgMask, argument.RawId, argument.RequiredValueCountAsString(), argument.ValuesAsString());
    }
}
}