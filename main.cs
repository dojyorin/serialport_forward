using System;
using System.Threading.Tasks;
using System.IO.Ports;

const byte ARG_COUNT = 2;
const ushort SERIALPORT_BUFFER_SIZE = 10240;
const ushort STREAM_BUFFER_SIZE = 61440;

if(args.Length != ARG_COUNT){
    Console.WriteLine($"Require {ARG_COUNT} arguments");
    Environment.Exit(1);
}

using var sp = new SerialPort(args[0], int.Parse(args[1]));

sp.DataBits = 8;
sp.StopBits = StopBits.One;
sp.Parity = Parity.None;
sp.DtrEnable = true;
sp.RtsEnable = true;
sp.ReadBufferSize = SERIALPORT_BUFFER_SIZE;
sp.WriteBufferSize = SERIALPORT_BUFFER_SIZE;

try{
    sp.Open();
}
catch(Exception){
    Console.WriteLine("Could not open serialport.");
    Environment.Exit(1);
}

using var txTask = Task.Run(async()=>{
    var buf = new char[STREAM_BUFFER_SIZE];
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
    var buf = new char[STREAM_BUFFER_SIZE];
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