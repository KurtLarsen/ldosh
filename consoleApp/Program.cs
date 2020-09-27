using System;

namespace consoleApp{
public static class Program{
    public static void Main(string[] args){
        if (!Global.ArgumentParser(args)){
            Console.Error.WriteLine(Global.ErrorMessage);
            Environment.Exit(Global.ExitCode);
        }

        if (!Global.ProfilesLoad()){
            Console.Error.WriteLine(Global.ErrorMessage);
            Environment.Exit(Global.ExitCode);
            
        }
    }
}
}