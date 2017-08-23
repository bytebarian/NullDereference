﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NullFuncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            funPointer ASM_Function = null;
            var v = ASM_Function();

            Console.ReadKey();
        }

        #region 

        static public byte[] _ASM_Code_Calc = new byte[]
        {
            0x31, 0xF6, 0x56, 0x64, 0x8B, 0x76, 0x30, 0x8B, 0x76, 0x0C, 0x8B,
            0x76, 0x1C, 0x8B, 0x6E, 0x08, 0x8B, 0x36, 0x8B, 0x5D, 0x3C, 0x8B,
            0x5C, 0x1D, 0x78, 0x01, 0xEB, 0x8B, 0x4B, 0x18, 0x67, 0xE3, 0xEC,
            0x8B, 0x7B, 0x20, 0x01, 0xEF, 0x8B, 0x7C, 0x8F, 0xFC, 0x01, 0xEF,
            0x31, 0xC0, 0x99, 0x32, 0x17, 0x66, 0xC1, 0xCA, 0x01, 0xAE, 0x75,
            0xF7, 0x66, 0x81, 0xFA, 0x10, 0xF5, 0xE0, 0xE2, 0x75, 0xCC, 0x8B,
            0x53, 0x24, 0x01, 0xEA, 0x0F, 0xB7, 0x14, 0x4A, 0x8B, 0x7B, 0x1C,
            0x01, 0xEF, 0x03, 0x2C, 0x97, 0x68, 0x2E, 0x65, 0x78, 0x65, 0x68,
            0x63, 0x61, 0x6C, 0x63, 0x54, 0x87, 0x04, 0x24, 0x50, 0xFF, 0xD5,
            0xC3
        };

        public delegate int funPointer();

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

            Marshal.Copy(_ASM_Code_Calc, 0, (IntPtr)1, _ASM_Code_Calc.Length);
        }

        #endregion
    }
}
