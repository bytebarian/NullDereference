using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace NullDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            char[] tab = null;
            foreach(var c in tab)
            {
                Console.Write(c);
            }

            Console.ReadKey();
        }

        #region 

        [DllImport("ntdll.dll")]
        public static extern uint NtAllocateVirtualMemory(
            IntPtr ProcessHandle,
            ref IntPtr BaseAddress,
            uint ZeroBits,
            ref uint AllocationSize,
            uint AllocationType,
            uint Protect);

        static void Init()
        {
            uint uSize = 2048;

            IntPtr baseAddress = (IntPtr)1;
            var name = Process.GetCurrentProcess().ProcessName;
            NtAllocateVirtualMemory(Process.GetCurrentProcess().Handle, 
                ref baseAddress, 
                0, 
                ref uSize, 
                0x3000, 
                0x40);

            string test = "Hello i am NULL";

            byte[] sbytes = Encoding.Unicode.GetBytes(test);
            byte[] bLength = BitConverter.GetBytes(sbytes.Length);

            Marshal.Copy(bLength, 0, (IntPtr)4, bLength.Length);
            Marshal.Copy(sbytes, 0, (IntPtr)8, sbytes.Length);
        }

        #endregion
    }
}
