using System;
using System.Threading;

internal sealed class Program
{
    static void Delay()
    {
        int i, end;
        double j = 0;

        Profiler.PlotConfig("Random", step: true);

        using (Profiler.BeginZone("My Custom Event Name"))
        {
            Random random = new Random();

            var randomValue = (int)random.NextInt64() / 100;
            for (i = 0, end = randomValue; i < end; ++i)
            {
                j += Math.Sin(i);
            }

            Profiler.Plot("Random", randomValue);
        }
    }

    static void ColoredEvent()
    {
        using (Profiler.BeginZone(color: (uint)ColorType.Aqua))
        {
            Thread.Sleep(16);
        }
    }

    internal static void Main(string[] args)
    {
        while (true)
        {
            Delay();
            ColoredEvent();

            Profiler.EmitFrameMark(); // Put this after you have completed rendering the frame. 
                                      // Ideally that would be right after the swap buffers command. 
                                      // Note that this step is optional, as some applications (for example: a compression utility) do not have the concept of a frame
        }
    }
}