using System;
using static Tracy.PInvoke;

public readonly struct ProfilerZone : IDisposable
{
    public readonly TracyCZoneCtx Context;

    internal ProfilerZone(TracyCZoneCtx context)
    {
        Context = context;
    }

    public void EmitName(string name)
    {
        var (namestr, nameln) = Profiler.GetCString(name);
        TracyEmitZoneName(Context, namestr, nameln);
    }

    public void EmitColor(uint color)
    {
        TracyEmitZoneColor(Context, color);
    }

    public void EmitText(string text)
    {
        var (textstr, textln) = Profiler.GetCString(text);
        TracyEmitZoneText(Context, textstr, textln);
    }

    public void Dispose()
    {
        TracyEmitZoneEnd(Context);
    }
}
