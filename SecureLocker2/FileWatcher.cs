using System;
using System.Threading;
using System.Threading.Tasks;

public class FileWatcher
{
    public EventHandler OnChange;
    private string file;
    private string lastHash;
    private CancellationTokenSource cancellationTokenSource;

    public FileWatcher(string file)
    {
        this.file = file;
        lastHash = Hash.Md5File(file);
    }

    public bool Check()
    {
        var currentHash = Hash.Md5File(file);
        if (currentHash != lastHash)
        {
            lastHash = currentHash;
            return false;
        }

        return true;
    }

    public void Start()
    {
        cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        Task.Run(async () =>
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                if (!Check())
                {
                    OnChange?.Invoke(this, null);
                }

                await Task.Delay(1000);
            }
        });
    }

    public void Stop()
    {
        cancellationTokenSource.Cancel();
    }
}