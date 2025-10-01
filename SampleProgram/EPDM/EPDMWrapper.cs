using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    class EPDMWrapper
    {
#if WOW64
        //x64
        public const String strLibrary = @"C:\Windows\System32\spool\drivers\x64\3\E_BEPSET32.DLL";
#else
        //x86
        public const String strLibrary = @"C:\Windows\System32\spool\drivers\W32X86\3\E_BEPSET32.DLL";
#endif

        [DllImport((strLibrary), EntryPoint = "EPDM_Open")]
        public static extern int EPDM_Open
            (String DevName, String PortName, int Type, IntPtr DMAdd,  out IntPtr PrnHandleAdd);

        [DllImport((strLibrary), EntryPoint = "EPDM_GetRange")]
        public static extern int EPDM_GetRange
            (IntPtr PrnHandle, int Command, IntPtr Param);

        [DllImport((strLibrary), EntryPoint = "EPDM_GetData")]
        public static extern int EPDM_GetData
            (IntPtr PrnHandle, int Command, IntPtr Param);

        [DllImport((strLibrary), EntryPoint = "EPDM_SetData")]
        public static extern int EPDM_SetData
            (IntPtr PrnHandle, int Command, int Param);

        [DllImport((strLibrary), EntryPoint = "EPDM_AddData")]
        public static extern int EPDM_AddData
            (IntPtr PrnHandle, int Command, IntPtr Param);

        [DllImport((strLibrary), EntryPoint = "EPDM_ImportData")]
        public static extern int EPDM_ImportData
            (IntPtr PrnHandle, int Command, IntPtr Param);

        [DllImport((strLibrary), EntryPoint = "EPDM_UpdateDevMode")]
        public static extern bool EPDM_UpdateDevMode
            (IntPtr PrnHandle);

        [DllImport((strLibrary), EntryPoint = "EPDM_Close")]
        public static extern bool EPDM_Close
            (IntPtr PrnHandle);
    }
}
