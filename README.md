# **SerialPort IO Pipe**
![Actions-Test](https://github.com/dojyorin/serialport_pipe/actions/workflows/test.yaml/badge.svg)
![Actions-Release](https://github.com/dojyorin/serialport_pipe/actions/workflows/release.yaml/badge.svg)

A simple console application that forwards serialport I/O to standard I/O.

# Example

**Console Terminal**

```sh
./sppipe /dev/ttyS1 115200
```

**Subprocess of External**

```ts
const process = new Deno.Command("sppipe.exe", {
    args: ["COM1", "115200"],
    stdin: "pipe",
    stdout: "pipe",
    stderr: "null"
}).spawn();
```

# Details

# Usage