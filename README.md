# **SerialPort Forward**
![actions:test](https://github.com/dojyorin/serialport_forward/actions/workflows/test.yaml/badge.svg)
![actions:release](https://github.com/dojyorin/serialport_forward/actions/workflows/release.yaml/badge.svg)

Forward serialport I/O to standard I/O.

# Details
This application use [.NET](https://dotnet.microsoft.com) serialport package [`System.IO.Ports`](https://www.nuget.org/packages/System.IO.Ports) to forward serialport I/O to standard I/O.

Built as [self-contained](https://learn.microsoft.com/ja-jp/dotnet/core/deploying) binary, so can work even in environments where .NET runtime is not installed.

# Usage
**As console terminal**

```sh
./spfw /dev/ttyS0 115200
./spfw.exe COM1 115200
```

**As subprocess of external application**

E.g. [Deno](https://deno.land)

```ts
const process = new Deno.Command("./spfw", {
    args: ["/dev/ttyS0", "115200"],
    stdin: "pipe",
    stdout: "pipe"
}).spawn();
```

- Arguments
    1. `path` ... Device file path of serialport that each platform. (e.g. `/dev/ttyS0`, `COM1`)
    2. `speed` ... Baud rate of connected serialport.
- Errors (exit code)
    - `10` ... Invalid arguments. Make sure that two arguments, device file path and baud rate are set.
    - `11` ... Could not open serialport. Check if device file path is correct or if communication device is actually connected to serialport.