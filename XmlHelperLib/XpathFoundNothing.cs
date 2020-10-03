using System;

namespace XmlHelperLib{
public class XpathFoundNothing : Exception{
    public XpathFoundNothing(string xPath) : base(xPath){ }
}
}