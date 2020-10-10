using System.Xml;

namespace XmlHelperLib.exceptions{
public class XmlNodeWrongNameException :XmlHelperException{
    public const string MsgMask = "Wrong XML node. Expected: <{0}> Found: <{1}>";
    public const int ErrCode = 3;

    public XmlNodeWrongNameException(string expectedName,XmlNode node) :
        base(ErrCode,MsgMask, expectedName, node.Name){ }

}
}