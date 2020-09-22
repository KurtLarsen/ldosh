using System;
using consoleApp;
using NUnit.Framework;

namespace consoleAppTest{
[TestFixture]
public class Tests{
    [Test]
    public void Test1(){
        var argsHandler=new ArgsHandler(new []{"--help"});
        Assert.NotNull(argsHandler);
    }
}
}