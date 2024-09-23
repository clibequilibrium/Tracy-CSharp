using System;
using static Tracy.PInvoke;

public readonly struct ProfilerZone : IDisposable
{
    public readonly TracyCZoneCtx Context;

    public uint Id => Context.Data.Id;

    public int Active => Context.Data.Active;

    internal ProfilerZone(TracyCZoneCtx context)
    {
        Context = context;
    }

    public void EmitName(string name)
    {
        using var namestr = Profiler.GetCString(name, out var nameln);
        TracyEmitZoneName(Context, namestr, nameln);
    }

    public void EmitColor(uint color)
    {
        TracyEmitZoneColor(Context, color);
    }

    public void EmitText(string text)
    {
        using var textstr = Profiler.GetCString(text, out var textln);
        TracyEmitZoneText(Context, textstr, textln);
    }

    public void Dispose()
    {
        TracyEmitZoneEnd(Context);
    }
}
