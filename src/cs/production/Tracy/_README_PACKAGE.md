# Tracy-CSharp

C# bindings for https://github.com/wolfpld/tracy with native dynamic link libraries based on [`imgui-cs`](https://github.com/bottlenoselabs/imgui-cs).

## How to use

Sample [`C# project`](https://github.com/clibequilibrium/Tracy-CSharp/tree/main/src/cs/samples/HelloWorld)

```CSharp
using (Profiler.BeginEvent())
{
    // Code to profile
}
Profiler.ProfileFrame("Main"); // Put this after you have completed rendering the frame. 
                                   // Ideally that would be right after the swap buffers command. 
                                   // Note that this step is optional, as some applications 
                                   // (for example: a compression utility) 
                                   // do not have the concept of a frame
```


## Developers: Documentation

For more information on how C# bindings work, see [`C2CS`](https://github.com/lithiumtoast/c2cs), the tool that generates the bindings for `Tracy` and other C libraries.

To learn how to use `Tracy`, check out the [official readme](https://github.com/wolfpld/tracy).

## License

`Tracy-CSharp` is licensed under the MIT License (`MIT`) - see the [LICENSE file](LICENSE) for details.

`Tracy` itself is licensed under BSD license (`BSD`) - see https://github.com/wolfpld/tracy/blob/master/LICENSE for more details.

