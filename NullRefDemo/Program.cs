﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace NullRefDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            Console.ReadKey();

            char[] tab = null;
            for (var i = 0; i < tab.Length; i++)
            {
                Console.Write(tab[i]);
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
#if NETFX_461

            uint uSize = 2048;

            IntPtr baseAddress = (IntPtr)1;
            var name = Process.GetCurrentProcess().ProcessName;
            NtAllocateVirtualMemory(Process.GetCurrentProcess().Handle,
                ref baseAddress,
                0,
                ref uSize,
                0x3000,
                0x40);

            string test = "8====D karny kutas za odwolanie do nulla";

            byte[] sbytes = Encoding.Unicode.GetBytes(test);
            byte[] bLength = BitConverter.GetBytes(sbytes.Length);

            Marshal.Copy(bLength, 0, (IntPtr)4, bLength.Length);
            Marshal.Copy(sbytes, 0, (IntPtr)8, sbytes.Length);
#endif
        }

        #endregion
    }
}
