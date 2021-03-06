﻿namespace XmlHelperLib.exceptions{
public class XpathFoundNothingException : XmlHelperException{
    public const string MsgMask = "Xpath found nothing: {0}";
    public const int ErrCode = 5;

    public XpathFoundNothingException(string xPath) : base(ErrCode, MsgMask, xPath){ }
}
}