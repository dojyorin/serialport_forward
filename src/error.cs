using System;

internal static class _ERROR_{
    internal static void errorExit(string message, EXITCODE code){
        Console.Error.WriteLine(message);
        Environment.Exit((int)code);
    }

    internal enum EXITCODE{
        INVALID_ARGUMENT = 10,
        FAILED_OPEN = 11
    }
}