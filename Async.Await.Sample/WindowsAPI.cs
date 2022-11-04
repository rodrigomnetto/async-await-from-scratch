using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Async.Await.Sample
{
    static public class WindowsAPI
    {
        public const uint Overlapped = 0x40000000;

        [Flags]
        public enum AccessRights : uint
        {
            GENERIC_READ = (0x80000000),
            GENERIC_WRITE = (0x40000000),
            GENERIC_EXECUTE = (0x20000000),
            GENERIC_ALL = (0x10000000)
        }
        [Flags]
        public enum ShareModes : uint
        {
            FILE_SHARE_READ = 0x00000001,
            FILE_SHARE_WRITE = 0x00000002,
            FILE_SHARE_DELETE = 0x00000004
        }
        public enum CreationDispositions
        {
            CREATE_NEW = 1,
            CREATE_ALWAYS = 2,
            OPEN_EXISTING = 3,
            OPEN_ALWAYS = 4,
            TRUNCATE_EXISTING = 5
        }
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile
        );
        [DllImport("kernel32.dll")]
        unsafe public static extern bool WriteFile(
            SafeFileHandle hFile, 
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite, 
            out uint lpNumberOfBytesWritten,
            NativeOverlapped* lpOverlapped
        );
    }
}
