namespace ArgumentHandlerLib.exceptions{
public class MissingValuesException : ArgumentException{
    // ReSharper disable once MemberCanBePrivate.Global
    public const string MsgMask = "Value(s) missing for argument {0}. Expected {1}. Found: {2}";
    public new const int Code = 4;

    public MissingValuesException(Argument argument) : base(Code, MsgMask, argument.RawId,
        argument.RequiredValueCountAsString(), argument.GivenValuesAsString()){ }
}
}