using System;
using ArgumentHandler;

namespace consoleApp{
public static class Program{
    internal static readonly string HelpMsg = $"-px <path to profile.xml>{Environment.NewLine}" +
                                              $"-p  <profile name>{Environment.NewLine}" +
                                              $"-sx <path to servers.xml{Environment.NewLine}" +
                                              $"-d  <domain>{Environment.NewLine}" +
                                              $"-db <database>{Environment.NewLine}" +
                                              $"-r  <path to project root>{Environment.NewLine}";

    public static void Main(string[] args){
        // try{
        //     var arguments = new ConsoleAppArguments(args);
        //     var deployer = new Deployer(arguments);
        // }
        // catch (AppArgumentException e){
        //     string msg;
        //     if (e.ErrorCode == AppArgumentException.HelpRequestCode){
        //         msg = HelpMsg;
        //     }
        //     else{
        //         msg = e.Message;
        //     }
        //     Console.WriteLine(msg);
        // }
        // catch (Exception e){
        //     Console.WriteLine(e);
        //     throw;
        // }

        var argumentHandler = new ArgumentHandler.ArgumentHandler();
        argumentHandler.AddArgument(new Argument("?").Optional().Stop());
        argumentHandler.AddArgument(new Argument("h").Optional().LongId("help").Stop());
        argumentHandler.AddArgument(new Argument("px").ValueCount(1));
        try{
            argumentHandler.Input(args);
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