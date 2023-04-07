using System;

partial internal static class StaticUtility{
    internal static void failExit(bool pause, string message, REASON code){
        Console.WriteLine(message);

        if(pause){
            Console.ReadKey();
        }

        Environment.Exit((int)code);
    }

    internal enum REASON{
        INVALID_ARGUMENT_COUNT = 10,
        INVALID_ARGUMENT_VALUE = 11,
        FAILED_OPEN_SERIALPORT = 12
    }
}