using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectMainDab.EasyExploitsInjectionClass
{
    class DLLInjection
    {
		public enum DllInjectionResult
		{
			// Token: 0x04000003 RID: 3
			DllNotFound,
			// Token: 0x04000004 RID: 4
			GameProcessNotFound,
			// Token: 0x04000005 RID: 5
			InjectionFailed,
			// Token: 0x04000006 RID: 6
			Success
		}

		// Token: 0x02000005 RID: 5
		public sealed class DllInjector
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000B RID: 11 RVA: 0x0000242F File Offset: 0x0000062F
			public static DLLInjection.DllInjector GetInstance
			{
				get
				{
					if (DLLInjection.DllInjector._instance == null)
					{
						DLLInjection.DllInjector._instance = new DLLInjection.DllInjector();
					}
					return DLLInjection.DllInjector._instance;
				}
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002454 File Offset: 0x00000654
			private DllInjector()
			{
			}

			// Token: 0x0600000E RID: 14 RVA: 0x0000245C File Offset: 0x0000065C
			private bool bInject(uint pToBeInjected, string sDllPath)
			{
				IntPtr intPtr = DLLInjection.DllInjector.OpenProcess(1082U, 1, pToBeInjected);
				if (intPtr == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				IntPtr procAddress = DLLInjection.DllInjector.GetProcAddress(DLLInjection.DllInjector.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
				if (procAddress == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				IntPtr intPtr2 = DLLInjection.DllInjector.VirtualAllocEx(intPtr, (IntPtr)0, (IntPtr)sDllPath.Length, 12288U, 64U);
				if (intPtr2 == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);
				if (DLLInjection.DllInjector.WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, 0) == 0)
				{
					return false;
				}
				if (DLLInjection.DllInjector.CreateRemoteThread(intPtr, (IntPtr)0, DLLInjection.DllInjector.INTPTR_ZERO, procAddress, intPtr2, 0U, (IntPtr)0) == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				DLLInjection.DllInjector.CloseHandle(intPtr);
				return true;
			}

			// Token: 0x0600000F RID: 15
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern int CloseHandle(IntPtr hObject);

			// Token: 0x06000010 RID: 16
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

			// Token: 0x06000011 RID: 17
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr GetModuleHandle(string lpModuleName);

			// Token: 0x06000012 RID: 18
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

			// Token: 0x06000013 RID: 19 RVA: 0x00002524 File Offset: 0x00000724
			public DLLInjection.DllInjectionResult Inject(string sProcName, string sDllPath)
			{
				if (!File.Exists(sDllPath))
				{
					return DLLInjection.DllInjectionResult.DllNotFound;
				}
				uint num = 0U;
				Process[] processes = Process.GetProcesses();
				for (int i = 0; i < processes.Length; i++)
				{
					if (!(processes[i].ProcessName != sProcName))
					{
						num = (uint)processes[i].Id;
						break;
					}
				}
				if (num == 0U)
				{
					return DLLInjection.DllInjectionResult.GameProcessNotFound;
				}
				if (!this.bInject(num, sDllPath))
				{
					return DLLInjection.DllInjectionResult.InjectionFailed;
				}
				return DLLInjection.DllInjectionResult.Success;
			}

			// Token: 0x06000014 RID: 20
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

			// Token: 0x06000015 RID: 21
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

			// Token: 0x06000016 RID: 22
			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

			// Token: 0x04000007 RID: 7
			private static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

			// Token: 0x04000008 RID: 8
			private static DLLInjection.DllInjector _instance;
		}
	}
}
