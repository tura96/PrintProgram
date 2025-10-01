using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    class ENSWrapper
    {
#if WOW64
        // X64 EpsonNetSDK.DLL InstallPath
        private const String strLibrary = @"C:\Program Files (x86)\EpsonNet\EpsonNetSDK\EpsonNetSDK.dll";
#else
        // X86 EpsonNetSDK.DLL InstallPath
        private const String strLibrary = @"C:\Program Files\EpsonNet\EpsonNetSDK\EpsonNetSDK.dll";
#endif
        
        [DllImport((strLibrary), EntryPoint = "ENSInitialize",
            SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ENSInitialize();

        [DllImport((strLibrary), EntryPoint = "ENSRelease",
            SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ENSRelease();

        [DllImport((strLibrary), EntryPoint = "ENSGetDeviceID",
            SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ENSGetDeviceID
            (int PathType, String PrnPath,
             IntPtr IdBuff, out int BuffLen,
             int StructVersion);

        [DllImport((strLibrary), EntryPoint = "ENSOpenCommunication",
            SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ENSOpenCommunication
            (int PathType, String PrnPath,
             IntPtr IdBuff, out IntPtr PtrHandle);

        [DllImport((strLibrary), EntryPoint = "ENSCloseCommunication",
            SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ENSCloseCommunication
            (IntPtr PtrHandle);

        [DllImport((strLibrary), EntryPoint = "ENSGetInformation",
            SetLastError = true, ExactSpelling = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ENSGetInformation
            (IntPtr PtrHandle, String Command,
             IntPtr GetParm, IntPtr GetBuff,
             out Int32 BuffLen);
    }
}