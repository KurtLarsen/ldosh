using System;
using ArgumentHandlerLib;

namespace consoleApp{
public static class Program{
    public const string ArgumentProfileName = "p";
    private static readonly string HelpMsg = $"-px <path to profile.xml>{Environment.NewLine}" +
                                             $"-p  <profile name>{Environment.NewLine}" +
                                             $"-sx <path to servers.xml{Environment.NewLine}" +
                                             $"-d  <domain>{Environment.NewLine}" +
                                             $"-db <database>{Environment.NewLine}" +
                                             $"-r  <path to project root>{Environment.NewLine}";

    public static void Main(string[] args){

        var argumentHandler = new ArgumentHandler();
        argumentHandler.DefineArgument(new Argument("?").Optional().IgnoreOthers());
        argumentHandler.DefineArgument(new Argument("h").Optional().LongId("help").IgnoreOthers());
        argumentHandler.DefineArgument(new Argument("px").RequiredValueCount(1));
        try{
            argumentHandler.AnalyzeGivenInput(args);
        }
        catch (Exception ex){
            Console.Error.WriteLine(ex.Message);
            Environment.Exit(-1);
        }

        if (argumentHandler.Contains("?", "h")){
            Console.Out.WriteLine(HelpMsg);
            Environment.Exit(1);
        }
        
        var x=new Deployer(argumentHandler);
        
        
    }
}
}