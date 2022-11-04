
namespace Async.Await.Sample
{
    public class PrintThreads
    {
        public static void Print()
        {
            ThreadPool.GetAvailableThreads(out var workerThread, out var ioThreads);
            Console.WriteLine($"IO threads: {ioThreads} Worker Threads: {workerThread}");
        }
    }
}
