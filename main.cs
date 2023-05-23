using System;
using System.Threading.Tasks;
using static _ERROR_;
using static _SERIALPORT_;

const byte ARG_COUNT = 2;

if(args.Length != ARG_COUNT){
    errorExit($"Require {ARG_COUNT} arguments", EXIT_CODE.INVALID_ARGUMENT_COUNT);
}

try{
    int.Parse(args[1]);
}
catch(Exception){
    errorExit("Invalid arguments.", EXIT_CODE.INVALID_ARGUMENT_VALUE);
}

using var sp = createSerialPort(args[0], int.Parse(args[1]));

try{
    sp.Open();
}
catch(Exception){
    errorExit("Could not open serialport.", EXIT_CODE.FAILED_OPEN_SERIALPORT);
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