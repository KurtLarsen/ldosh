using System;

namespace XmlHelperLib.exceptions{
public abstract class XmlHelperException:MyException.MyException{
    // constructor
    public XmlHelperException(int code,string msgMask, params Object[] args) : base(code,msgMask, args){ }

}
}