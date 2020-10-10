using System.Xml;

namespace XmlHelperLib.exceptions{
public class XmlAttributeNotFoundException : XmlHelperException{
    public const string MsgMask = "Argument \"{0}\" not found in node <{1}>";
    public const int ErrCode = 1;

    public XmlAttributeNotFoundException(string missingAttributeName, XmlNode node)
        : base(MsgMask, missingAttributeName, node.Name){
        _code = ErrCode;
    }

}
}