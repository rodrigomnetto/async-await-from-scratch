
namespace Async.Await.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintThreads.Print();

            //initialize state machine
            var stateMachine = new NaiveAsyncStateMachine();
            stateMachine.MoveNext();

            Console.WriteLine("thread free, return to the thread pool");
            Console.ReadLine();
        }
    }
}