using System;

namespace ArgumentHandler{
public class UnknownArgumentIdException:Exception{
    public const string MsgMask = "Unknown argument id: \"{0}\"";

    public UnknownArgumentIdException(string argumentId):base(Msg(argumentId)){ }
    
    private static string Msg(string msg){
        return string.Format(MsgMask, msg);

    }

}
}