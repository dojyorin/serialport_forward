# **SerialPort Forward**
![actions:test](https://github.com/dojyorin/serialport_forward/actions/workflows/test.yaml/badge.svg)
![actions:release](https://github.com/dojyorin/serialport_forward/actions/workflows/release.yaml/badge.svg)

A simple console application that forwards serialport I/O to standard I/O.

# Example

**As console terminal**

```sh
./spfw /dev/ttyS0 115200
```

**As subprocess of external application**

E.g. [Deno](https://deno.land)

```ts
const process = new Deno.Command("./spfw.exe", {
    args: ["COM1", "115200"],
    stdin: "pipe",
    stdout: "pipe",
    stderr: "null"
}).spawn();
```

# Details
This application uses [.NET](https://dotnet.microsoft.com) serialport package [`System.IO.Ports`](https://www.nuget.org/packages/System.IO.Ports) to forwards serialport I/O to standard I/O.

It is built as [self-contained](https://learn.microsoft.com/ja-jp/dotnet/core/deploying) file, so it can work even in environments where the .NET runtime is not installed.

# Usage

```sh
# for Linux and Mac
./spfw device_file baud_rate

# for Windows
./spfw.exe device_file baud_rate
```

## `spfw`
- Arguments
    1. `device_file` (required) ... Device file names of serialports that exist on each platform (e.g. `/dev/ttyS0`, `COM1`)
    2. `baud_rate` (required) ... Transfer rate of connected serialport.
- Errors (return code)
    - `10` ... Incorrect number of arguments. Make sure that the 2 arguments, device file name and transfer rate, are set.
    - `11` ... The transfer rate specification is incorrect. Check if the string can be parsed as an int type number.
    - `12` ... Failed to open serial port. Check if the device file name is correct or if a communication device is actually connected to the serial port.