using System;
using System.Threading.Tasks;
using static StaticUtility;

const byte ARG_COUNT = 2;

if(args.Length != ARG_COUNT){
    failExit(false, $"Require {ARG_COUNT} arguments", REASON.INVALID_ARGUMENT_COUNT);
}

try{
    int.Parse(args[1]);
}
catch(Exception){
    failExit(false, "Invalid arguments.", REASON.INVALID_ARGUMENT_VALUE);
}

using var sp = createSerial(args[0], int.Parse(args[1]));

try{
    sp.Open();
}
catch(Exception){
    failExit(false, "Could not open serialport.", REASON.FAILED_OPEN_SERIALPORT);
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