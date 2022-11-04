
namespace Sample1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Print();
            //first state
            Console.WriteLine("first state");

            await File.WriteAllTextAsync("teste.txt", "Hello world!", encoding: System.Text.Encoding.ASCII);
            Print();
            //last state
            Console.WriteLine("IO finished, executes last state");
        }

        public static void Print()
        {
            ThreadPool.GetAvailableThreads(out var workerThread, out var ioThreads);
            Console.WriteLine($"IO threads: {ioThreads} Worker Threads: {workerThread}");
        }
    }
}