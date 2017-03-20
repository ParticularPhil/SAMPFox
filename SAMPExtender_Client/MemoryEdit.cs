using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using System.Threading.Tasks;

namespace SAMPExtender_Client
{
    public class MemoryEdit
    {
        class MemoryEditTools
        {
            [Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 0x001F0FFF,
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VMOperation = 0x00000008,
                VMRead = 0x00000010,
                VMWrite = 0x00000020,
                DupHandle = 0x00000040,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                Synchronize = 0x00100000
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

            [DllImport("kernel32.dll")]
            private static extern Int32 CloseHandle(IntPtr hProcess);

            public static byte[] ReadMemory(Process process, int address, int numOfBytes, out int bytesRead)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] buffer = new byte[numOfBytes];
                ReadProcessMemory(hProc, new IntPtr(address), buffer, numOfBytes, out bytesRead);
                return buffer;
            }

            public static bool NOPMemory_T(Process process, int address, int length, out int bytesWritten)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] val = new byte[length];
                for (int i = 0; i != length; i++)
                    val[i] = 0x90;
                bool s = WriteProcessMemory(hProc, new IntPtr(address), val, (uint)length, out bytesWritten);
                CloseHandle(hProc);
                return s;
            }
            public static bool WriteMemory(Process process, int address, int value, out int bytesWritten)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] val = BitConverter.GetBytes(value);
                bool s = WriteProcessMemory(hProc, new IntPtr(address), val, (uint)val.LongLength, out bytesWritten);
                CloseHandle(hProc);
                return s;
            }
            public static bool WriteMemory(Process process, int address, uint value, out int bytesWritten)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] val = BitConverter.GetBytes(value);
                bool s = WriteProcessMemory(hProc, new IntPtr(address), val, (uint)val.LongLength, out bytesWritten);
                CloseHandle(hProc);
                return s;
            }
            public static bool WriteMemory(Process process, int address, short value, out int bytesWritten)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] val = BitConverter.GetBytes(value);
                bool s = WriteProcessMemory(hProc, new IntPtr(address), val, (uint)val.LongLength, out bytesWritten);
                CloseHandle(hProc);
                return s;
            }
            public static bool WriteMemory(Process process, int address, float value, out int bytesWritten)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] val = BitConverter.GetBytes(value);
                bool s = WriteProcessMemory(hProc, new IntPtr(address), val, (uint)val.LongLength, out bytesWritten);
                CloseHandle(hProc);
                return s;
            }
            public static bool WriteMemory(Process process, int address, byte value, out int bytesWritten)
            {
                IntPtr hProc = OpenProcess(ProcessAccessFlags.All, false, process.Id);
                byte[] val = BitConverter.GetBytes(value);
                bool s = WriteProcessMemory(hProc, new IntPtr(address), val, (uint)val.LongLength, out bytesWritten);
                CloseHandle(hProc);
                return s;
            }
        }

        Process p;
        public bool processActive = false;

        public MemoryEdit()
        {   
        }

        public int getModuleAddress(string name)
        {
            foreach (ProcessModule m in p.Modules)
            {
                if (m.ModuleName == "samp.dll")
                    return (int)m.BaseAddress;
            }
            return 0;
        }

        public void checkProcess()
        {
            if (p != null)
            {
                if (p.HasExited)
                    processActive = false;
                else
                    processActive = true;
            }
        }

        public bool getProcess(string name)
        {
            Process[] pList = Process.GetProcessesByName(name);
            if (pList.Length == 0)
                return false;
            p = pList.First();
            processActive = true;
            return true;
        }
        public bool writeToMemory(int address, int value)
        {
            int bytesWritten;
            bool s = MemoryEditTools.WriteMemory(p, address, value, out bytesWritten);
            return s;
        }
        public bool writeToMemory(int address, short value)
        {
            int bytesWritten;
            bool s = MemoryEditTools.WriteMemory(p, address, value, out bytesWritten);
            return s;
        }
        public bool writeToMemory(int address, uint value)
        {
            int bytesWritten;
            bool s = MemoryEditTools.WriteMemory(p, address, value, out bytesWritten);
            return s;
        }
        public bool writeToMemory(int address, float value)
        {
            int bytesWritten;
            bool s = MemoryEditTools.WriteMemory(p, address, value, out bytesWritten);
            return s;
        }
        public bool writeToMemory(int address, byte value)
        {
            int bytesWritten;
            bool s = MemoryEditTools.WriteMemory(p, address, value, out bytesWritten);
            return s;
        }
        public byte[] readFromMemory(int address,int length)
        {
            int read = 0;
            byte[] a=MemoryEditTools.ReadMemory(p, address, length, out read);
            return a;
        }
        public int readIntFromMemory(int address)
        {
            byte[] a = readFromMemory(address, sizeof(int));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a);
            return BitConverter.ToInt32(a, 0);
        }
        public uint readUIntFromMemory(int address)
        {
            byte[] a = readFromMemory(address, sizeof(uint));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(a);
            return BitConverter.ToUInt32(a, 0);
        }
        public float readFloatFromMemory(int address)
        {
            byte[] a = readFromMemory(address, sizeof(float));
            /*if (BitConverter.IsLittleEndian)
                Array.Reverse(a);*/
            float f = BitConverter.ToSingle(a, 0);
            return BitConverter.ToSingle(a, 0);
        }
        public ushort readUShortFromMemory(int address)
        {
            byte[] a = readFromMemory(address, sizeof(ushort));
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(a);
            return BitConverter.ToUInt16(a, 0);
        }
        public byte readByteFromMemory(int address)
        {
            byte[] a = readFromMemory(address, sizeof(byte));
            return a[0];
        }
        public bool NOPMemory(int address, int length)
        {
            int bytesWritten;
            bool s = MemoryEditTools.NOPMemory_T(p, address, length, out bytesWritten);
            return s;
        }
    }
}
