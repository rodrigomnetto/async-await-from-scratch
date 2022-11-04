using Microsoft.Win32.SafeHandles;
using System.Text;

namespace Async.Await.Sample
{
    public class File
    {
        public static void WriteFileAsync(string fileName, string content, NaiveAsyncStateMachine stateMachine)
        {
            unsafe
            {
                //creates the file and returns a handle wich is used to identify the file
                SafeFileHandle fileHandle = WindowsAPI.CreateFile(
                fileName,
                (uint)(WindowsAPI.AccessRights.GENERIC_ALL),
                (uint)(WindowsAPI.ShareModes.FILE_SHARE_READ | WindowsAPI.ShareModes.FILE_SHARE_WRITE),
                IntPtr.Zero,
                (uint)(WindowsAPI.CreationDispositions.CREATE_NEW),//create new file
                WindowsAPI.Overlapped, //this flag indicates async I/O
                IntPtr.Zero);

                //The CLR maintains its own I/O completion port, and can bind any handle to it (via the ThreadPool.BindHandle API).
                ThreadPool.BindHandle(fileHandle);

                //creates the callback structure accepted by the OS
                Overlapped overlapped = new();
                NativeOverlapped* nativeOverlapped = overlapped.Pack(Callback, null);

                //write the file content (asynchronously) and passes the callback to be executed when the IO finishes
                WindowsAPI.WriteFile(fileHandle, Encoding.ASCII.GetBytes(content), Convert.ToUInt32(content.Length), out _, nativeOverlapped);

                //the callback calls the state machine again
                void Callback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP)
                {
                    PrintThreads.Print();
                    fileHandle.Close();
                    stateMachine.MoveNext();
                }
            }
        }
    }
}
