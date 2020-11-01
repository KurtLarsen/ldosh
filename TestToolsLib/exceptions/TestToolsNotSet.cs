namespace TestToolsLib.exceptions{
public class TestToolsIsNotAssigned : TestException{
    private const string MsgMask = "TestTools is not assigned";

    private const int ErrCode = 1;

    public TestToolsIsNotAssigned() : base(ErrCode, MsgMask){ }
}
}