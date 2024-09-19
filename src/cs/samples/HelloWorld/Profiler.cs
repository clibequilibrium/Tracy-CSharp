using System.Runtime.CompilerServices;
using bottlenoselabs.C2CS.Runtime;
using static Tracy.PInvoke;

public static class Profiler
{
    /// <summary>
    /// Begins a new <see cref="ProfilerZone"/> and returns the handle to that zone. Time
    /// spent inside a zone is calculated by Tracy and shown in the profiler. A zone is
    /// ended when <see cref="ProfilerZone.Dispose"/> is called either automatically via 
    /// disposal scope rules or by calling it manually.
    /// </summary>
    /// <param name="zoneName">A custom name for this zone.</param>
    /// <param name="active">Is the zone active. An inactive zone wont be shown in the profiler.</param>
    /// <param name="color">An <c>RRGGBB</c> color code that Tracy will use to color the zone in the profiler.</param>
    /// <param name="text">Arbitrary text associated with this zone.</param>
    /// <param name="lineNumber">
    /// The source code line number that this zone begins at.
    /// If this param is not explicitly assigned the value will provided by <see cref="CallerLineNumberAttribute"/>.
    /// </param>
    /// <param name="filePath">
    /// The source code file path that this zone begins at.
    /// If this param is not explicitly assigned the value will provided by <see cref="CallerFilePathAttribute"/>.
    /// </param>
    /// <param name="memberName">
    /// The source code member name that this zone begins at.
    /// If this param is not explicitly assigned the value will provided by <see cref="CallerMemberNameAttribute"/>.
    /// </param>
    /// <returns></returns>
    public static ProfilerZone BeginZone(
        string zoneName = null,
        bool active = true,
        uint color = 0,
        string text = null,
        [CallerLineNumber] uint lineNumber = 0,
        [CallerFilePath] string filePath = null,
        [CallerMemberName] string memberName = null)
    {
        var (filestr, fileln) = GetCString(filePath);
        var (memberstr, memberln) = GetCString(memberName);
        var (namestr, nameln) = GetCString(zoneName);
        var srcLocId = TracyAllocSrclocName(lineNumber, filestr, fileln, memberstr, memberln, namestr, nameln);
        var context = TracyEmitZoneBeginAlloc(srcLocId, active ? 1 : 0);

        if (color != 0)
        {
            TracyEmitZoneColor(context, color);
        }

        if (text != null)
        {
            var (textstr, textln) = GetCString(text);
            TracyEmitZoneText(context, textstr, textln);
        }

        return new ProfilerZone(context);
    }

    /// <summary>
    /// Add a <see cref="double"/> value to a plot.
    /// </summary>
    public static void Plot(string name, double val)
    {
        var (namestr, _) = GetCString(name);
        TracyEmitPlot(namestr, val);
    }

    /// <summary>
    /// Add a <see cref="int"/> value to a plot.
    /// </summary>
    public static void Plot(string name, int val)
    {
        var (namestr, _) = GetCString(name);
        TracyEmitPlotInt(namestr, val);
    }

    /// <summary>
    /// Add a <see cref="float"/> value to a plot.
    /// </summary>
    public static void Plot(string name, float val)
    {
        var (namestr, _) = GetCString(name);
        TracyEmitPlotFloat(namestr, val);
    }

    /// <summary>
    /// Emit the top-level frame marker.
    /// </summary>
    /// <remarks>
    /// Tracy Cpp API and docs refer to this as the <c>FrameMark</c> macro.
    /// </remarks>
    public static void EmitFrameMark()
    {
        TracyEmitFrameMark(null);
    }

    /// <summary>
    /// Creates a <seealso cref="CString"/> for use by Tracy. Also returns the
    /// length of the string for interop convenience.
    /// </summary>
    public static (CString cstring, ulong clength) GetCString(string? fromString)
    {
        if (fromString == null)
        {
            return (new CString(0), 0);
        }
        return (CString.FromString(fromString), (ulong)fromString.Length);
    }
}
