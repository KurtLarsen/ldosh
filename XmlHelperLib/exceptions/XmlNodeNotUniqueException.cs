using System.Xml;

namespace XmlHelperLib.exceptions{
public class XmlNodeNotUniqueException : XmlHelperException{
    public const string MsgMask = "XML node <{0}> is not unique in <{1}>";
    public const int ErrCode = 3;

    public XmlNodeNotUniqueException(XmlNode subNode) :
        base(ErrCode,MsgMask,subNode.Name,subNode.ParentNode.Name){ }
}
}