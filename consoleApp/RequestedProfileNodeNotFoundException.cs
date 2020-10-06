using System;

namespace consoleApp{
public class RequestedProfileNodeNotFoundException:Exception{
    public RequestedProfileNodeNotFoundException(string message) : base(message){ }
}
}