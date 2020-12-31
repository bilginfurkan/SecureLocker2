using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MiniTask
{
    public long id;
    public Func<object> action;
}

public static class TaskController
{
    public static ManualResetEvent mre = new ManualResetEvent(false);
    static List<Task> tasks = new List<Task>();
    static Queue<MiniTask> actions = new Queue<MiniTask>();
    static Dictionary<long, object> performedActions = new Dictionary<long, object>();

    static bool cancelTasks;
    static long currentId;

    static TaskController()
    {
        for (int i = 0; i < (Environment.ProcessorCount > 0 ? Environment.ProcessorCount : 6); i++)
        {
            StartThread(Worker);
        }

        mre.Set();
        //Resume();
    }
#if UNITY_EDITOR

    [MenuItem("Tasks/Resume")]
    public static void Resume()
    {
        mre.Set();
    }

    [MenuItem("Tasks/Pause")]
    public static void Pause()
    {
        mre.Reset();
    }
#endif

    public static void StartThread(Action action)
    {
        var task = Task.Run(action);
        task.ConfigureAwait(false);

        tasks.Add(task);
    }

    public static void Terminate()
    {
        cancelTasks = true;
        foreach (var task in tasks)
        {
            task.Wait();
            task.Dispose();
        }
        tasks.Clear();
    }

    public static async void WorkAsync<T>(Func<T> func)
    {
        Func<object> _func = () => // Convert Func that returns T to Func that returns object
        {
            return func();
        };
        var taskId = currentId;
        var result = default(T);

        var task = new MiniTask()
        {
            action = _func,
            id = taskId
        };
        currentId++;

        lock (actions)
        {
            actions.Enqueue(task);
        }
        /*
        await JobDone(taskId);

        lock (performedActions)
        {
            result = (T)performedActions[taskId];
            performedActions.Remove(taskId);
        }

        return result;*/
    }

    static async Task JobDone(long taskId)
    {
        while (true)
        {
            lock (performedActions)
            {
                if (performedActions.ContainsKey(taskId))
                {
                    break;
                }
            }
            await Task.Delay(100);
        }
    }

    static int i = 0;
    static async void Worker()
    {
        var ti = ++i;
        var targets = new List<MiniTask>();
        while (true)
        {
            if (cancelTasks)
            {
                return;
            }

            try
            {
                lock (actions)
                {
                    if (actions.Count > 0)
                    {
                        targets.Add(actions.Dequeue()); // Get all Tasks and stop locking actions list.
                    }
                }

                for (int i = 0; i < targets.Count; i++)
                {
                    var target = targets[i];
                    var result = target.action(); // Work and callback

                    //lock (performedActions)
                    //    performedActions[target.id] = result;

                    target = null;
                }

                targets.Clear();
            }
            catch (Exception ex)
            {
                targets.Remove(targets[0]);
            }

            await Task.Delay(100);
        }
    }
}