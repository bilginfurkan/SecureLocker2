using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class Extensions
{
    public static async Task WaitForExitAsync(this Process process)
    {/*
        var tcs = new TaskCompletionSource<object>();
        process.EnableRaisingEvents = true;
        process.Exited += (sender, args) => tcs.TrySetResult(null);
        if (cancellationToken != default(CancellationToken))
            cancellationToken.Register(tcs.SetCanceled);

        return tcs.Task;
    */
        while (Process.GetProcessesByName(process.ProcessName).Length > 0)
        {
            await Task.Delay(500);
        }
    }

    public static T[][] SplitToChunks<T>(T[] array, int chunkSize)
    {
        var i = 0;
        return array.GroupBy(s => i++ / chunkSize).Select(g => g.ToArray()).ToArray();
    }
}