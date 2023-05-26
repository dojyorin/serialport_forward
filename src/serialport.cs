using System.IO.Ports;

internal static class _SERIALPORT_{
    internal static SerialPort createSerialPort(string path, int speed){
        using var sp = new SerialPort();
        sp.PortName = path;
        sp.BaudRate = speed;
        sp.Parity = Parity.None;
        sp.StopBits = StopBits.One;
        sp.DataBits = 8;
        sp.DtrEnable = true;
        sp.RtsEnable = true;
        sp.ReadBufferSize = 16384;
        sp.WriteBufferSize = 16384;

        return sp;
    }
}