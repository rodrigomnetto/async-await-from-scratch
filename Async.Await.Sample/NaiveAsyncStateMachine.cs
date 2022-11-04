
namespace Async.Await.Sample
{
    public class NaiveAsyncStateMachine
    {
        private int _state = -1;

        public void MoveNext()
        {
            if (_state == -1) //first state
            {
                Console.WriteLine("first state");

                var text = "Hello world!";
                File.WriteFileAsync("teste.txt", text, this);
                
                _state = 0;
            } else if (_state == 0) //last state
            {
                Console.WriteLine("IO finished, executes last state");
            }
        }
    }
}
