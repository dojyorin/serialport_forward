using System;
using System.Threading.Tasks;
using System.IO.Ports;

const byte ARG_COUNT = 2;

if(args.Length != ARG_COUNT){
    Console.WriteLine($"Require {ARG_COUNT} arguments");
    Environment.Exit(10);
}

try{
    int.Parse(args[1]);
}
catch(Exception){
    Console.WriteLine("Invalid arguments.");
    Environment.Exit(11);
}

using var sp = createSerialPort(args[0], int.Parse(args[1]));

try{
    sp.Open();
}
catch(Exception){
    Console.WriteLine("Could not open serialport.");
    Environment.Exit(12);
}

using var t1 = Task.Run(async()=>{
    var buf = new char[32768];
    var n = 0;

    while(true){
        try{
            n = await Console.In.ReadAsync(buf, 0, buf.Length);
            sp.Write(buf, 0, n);
        }
        catch(Exception){
            break;
        }
    }
});

using var t2 = Task.Run(async()=>{
    var buf = new char[32768];
    var n = 0;

    while(true){
        try{
            n = sp.Read(buf, 0, buf.Length);
            await Console.Out.WriteAsync(buf, 0, n);
        }
        catch(Exception){
            break;
        }
    }
});

t1.Wait();
t2.Wait();

SerialPort createSerialPort(string device, int speed){
    using var sp = new SerialPort(device, speed, Parity.None, 8, StopBits.One);
    sp.DtrEnable = true;
    sp.RtsEnable = true;
    sp.ReadBufferSize = 16384;
    sp.WriteBufferSize = 16384;

    return sp;
}