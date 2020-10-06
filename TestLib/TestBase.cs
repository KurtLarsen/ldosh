using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace TestLib{
[TestFixture]
public class TestBase{
    

    protected static string TestDataFolder{
        get{ return Path.GetFullPath(TestContext.CurrentContext.TestDirectory + @"\..\..\TestData\"); }
    }

    
    

}
}