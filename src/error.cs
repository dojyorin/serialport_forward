using System;

internal static class _ERROR_{
    internal static void errorExit(string message, EXIT_CODE code){
        Console.Error.WriteLine(message);
        Environment.Exit((int)code);
    }

    internal enum EXIT_CODE{
        INVALID_ARGUMENT_COUNT = 10,
        INVALID_ARGUMENT_VALUE = 11,
        FAILED_OPEN_SERIALPORT = 12
    }
}