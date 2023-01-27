using System;

namespace ModERROR{
    public static class Export{
        public static void failFast(bool pause, string message, REASON code){
            Console.WriteLine(message);

            if(pause){
                Console.ReadKey();
            }

            Environment.Exit((int)code);
        }

        public enum REASON{
            INVALID_ARGUMENT_COUNT = 10,
            INVALID_ARGUMENT_VALUE = 11,
            FAILED_OPEN_SERIALPORT = 12
        }
    }
}