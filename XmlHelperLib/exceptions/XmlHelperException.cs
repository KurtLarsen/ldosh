using System;

namespace XmlHelperLib.exceptions{
public abstract class XmlHelperException:MyException.MyException{
    // constructor
    public XmlHelperException(string msgMask, params Object[] args) : base(msgMask, args){ }

}
}