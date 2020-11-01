using System;
using ArgumentHandlerLib;

namespace consoleApp{
public static class Program{
    // ReSharper disable once UnusedMember.Global
    public const string ArgumentProfileName = "p";

    private static readonly string HelpMsg = $"-px <path to profile.xml>{Environment.NewLine}" +
                                             $"-p  <profile name>{Environment.NewLine}" +
                                             $"-sx <path to servers.xml{Environment.NewLine}" +
                                             $"-d  <domain>{Environment.NewLine}" +
                                             $"-db <database>{Environment.NewLine}" +
                                             $"-r  <path to project root>{Environment.NewLine}";

    public static void Main(string[] args){
        var argumentHandler = GetArgumentHandler();
        
        try{
            argumentHandler.SetInputParam(args);
        }
        catch (Exception ex){
            Console.Error.WriteLine(ex.Message);
            Environment.Exit(-1);
        }

        if (argumentHandler.Contains("?", "h")){
            Console.Out.WriteLine(HelpMsg);
            Environment.Exit(1);
        }

        // ReSharper disable once UnusedVariable
        var x = new Deployer(argumentHandler);
    }

    public static ArgumentHandler GetArgumentHandler(){
        return new ArgumentHandler(
            new Argument("?").Optional().IgnoreOthers(),
            new Argument("h").Optional().LongId("help").IgnoreOthers(),
            new Argument("px").RequiredValueCount(1),
            new Argument("pr").Optional().LongId("projectroot").RequiredValueCount(1)
        );
    }
}
}