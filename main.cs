using System;
using System.Threading.Tasks;
using System.IO.Ports;

if(args.Length != 2){
    Console.WriteLine("Require 2 arguments of serialport 'Name' and 'Speed'");
    Environment.Exit(1);
}

using var sp = new SerialPort(args[0], int.Parse(args[1]));

sp.DataBits = 8;
sp.StopBits = StopBits.One;
sp.Parity = Parity.None;
sp.DtrEnable = true;
sp.RtsEnable = true;
sp.ReadBufferSize = 65536;
sp.WriteBufferSize = 65536;

try{
    sp.Open();
}
catch(Exception){
    Console.WriteLine("Could not open serialport.");
    Environment.Exit(1);
}

using var txTask = Task.Run(async()=>{
    var buf = new char[1048576];
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
    var buf = new char[1048576];
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