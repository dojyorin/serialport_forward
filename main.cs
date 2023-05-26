using System;
using System.Threading.Tasks;
using static _ERROR_;
using static _SERIALPORT_;

var path = "";
var speed = 0;

try{
    path = args[0];
    speed = int.Parse(args[1]);
}
catch(Exception){
    errorExit("Invalid arguments.", EXITCODE.INVALID_ARGUMENT);
}

using var sp = createSerialPort(path, speed);

try{
    sp.Open();
}
catch(Exception){
    errorExit("Could not open serialport.", EXITCODE.FAILED_OPEN);
}

using var t1 = Task.Run(()=>{
    var buf = new char[32768];
    var n = 0;

    while(true){
        try{
            n = Console.In.Read(buf, 0, buf.Length);
            sp.Write(buf, 0, n);
        }
        catch(Exception){
            break;
        }
    }
});

using var t2 = Task.Run(()=>{
    var buf = new char[32768];
    var n = 0;

    while(true){
        try{
            n = sp.Read(buf, 0, buf.Length);
            Console.Out.Write(buf, 0, n);
        }
        catch(Exception){
            break;
        }
    }
});

t1.Wait();
t2.Wait();