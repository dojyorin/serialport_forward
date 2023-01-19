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

using var txTask = Task.Run(async()=>{
    var buf = new char[32768];
    var n = 0;

    while(true){
        try{
            n = await Console.In.ReadAsync(buf, 0, buf.Length);

            if(n == 0){
                continue;
            }

            sp.Write(buf, 0, n);
        }
        catch(Exception){
            break;
        }
    }
});

using var rxTask = Task.Run(async()=>{
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

txTask.Wait();
rxTask.Wait();

SerialPort createSerialPort(string device, int speed){
    using var ctx = new SerialPort(device, speed);
    ctx.DataBits = 8;
    ctx.StopBits = StopBits.One;
    ctx.Parity = Parity.None;
    ctx.DtrEnable = true;
    ctx.RtsEnable = true;
    ctx.ReadBufferSize = 16384;
    ctx.WriteBufferSize = 16384;

    return ctx;
}