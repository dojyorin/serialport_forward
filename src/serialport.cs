using System.IO.Ports;

namespace ModSP{
    public static class Export{
        public static SerialPort createSerialPort(string device, int speed){
            using var sp = new SerialPort(device, speed, Parity.None, 8, StopBits.One);
            sp.DtrEnable = true;
            sp.RtsEnable = true;
            sp.ReadBufferSize = 16384;
            sp.WriteBufferSize = 16384;

            return sp;
        }
    }
}