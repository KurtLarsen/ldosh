using System.Xml;

namespace XmlHelperLib.exceptions{
public class XmlNodeNotFoundException : XmlHelperException{
    public const string MsgMask = "XML node <{0}> not found in parent node <{1}>";
    public const int ErrCode = 2;

    public XmlNodeNotFoundException(string missingNodeName, XmlNode node)
        : base(ErrCode, MsgMask, missingNodeName, node.Name){ }
}
}