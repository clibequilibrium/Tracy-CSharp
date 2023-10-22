using System;
using System.Threading;
using bottlenoselabs.C2CS.Runtime;
using static Tracy.PInvoke;

internal sealed class Program
{
    static void Delay()
    {
        int i, end;
        double j = 0;

        using (Profiler.BeginEvent("My Custom Event Name"))
        {

            Random random = new Random();

            for (i = 0, end = (int)random.NextInt64() / 100; i < end; ++i)
            {
                j += Math.Sin(i);
            }

        }
    }

    static void ColoredEvent()
    {
        using (Profiler.BeginEvent(colorType: Profiler.ColorType.Aqua))
        {

            Thread.Sleep(16);
        }
    }

    internal static unsafe void Main(string[] args)
    {
        while (true)
        {
            Delay();
            ColoredEvent();

            Profiler.ProfileFrame("Main"); // Put this after you have completed rendering the frame. 
                                           // Ideally that would be right after the swap buffers command. 
                                           // Note that this step is optional, as some applications (for example: a compression utility) do not have the concept of a frame
        }

        // Call it at the end of your program to free allocated memory
        Profiler.Dispose();
    }
}