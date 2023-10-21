using System.Threading;
using bottlenoselabs.C2CS.Runtime;
using static Tracy.PInvoke;

internal sealed class Program
{
    internal static unsafe void Main(string[] args)
    {
        while (true)
        {
            TracyEmitFrameMark((CString)("FrameUpdate"));
            Thread.Sleep(16);
        }
    }
}