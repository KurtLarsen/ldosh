namespace TestToolsLib.exceptions{
public class TestException : MyException.MyException{
    protected TestException(int code, string msgMask, params object[] args) : base(code, msgMask, args){ }
}
}