using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    class EPDMMediaLayoutAdd : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTADD
        {
            public uint dwVersion;
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] szName;
            public uint dwOverWrite;
            public EPDMMediaLayoutInf.EPDM_MEDIALAYOUTINF tagMediaLayoutInf;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIALAYOUTADD _struct;

        #endregion

        #region Property

        public String Name
        {
            get
            {
                // Get the code page using the OS.  
                int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                Encoding _Enc = Encoding.GetEncoding(Codepage);

                return new String(_Enc.GetString(_struct.szName).ToCharArray());
            }
            set
            {

                // Get the code page using the OS.  
                int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                Encoding _Enc = Encoding.GetEncoding(Codepage);

                String strValue = value;
                byte[] strData = _Enc.GetBytes(strValue);

                int nLen = strData.Length;

                if (nLen > 128)
                {
                    nLen = 128;
                }
                for (int i = 0; i < nLen; i++)
                {
                    _struct.szName[i] = strData[i];
                }
            }
        }

        public uint Overwrite
        {
            get
            {
                return _struct.dwOverWrite;
            }
            set
            {
                _struct.dwOverWrite = value;
            }
        }

        public EPDMMediaLayoutInf.EPDM_MEDIALAYOUTINF MediaLayoutInf
        {
            get
            {
                return _struct.tagMediaLayoutInf;
            }
            set
            {
                _struct.tagMediaLayoutInf = value;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutAdd
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutAdd()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));
            _struct = new EPDM_MEDIALAYOUTADD();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTADD;
            _struct.dwSize = (uint)(Marshal.SizeOf(_struct));
            _struct.szName = null;
            _struct.dwOverWrite = (uint)OverWriteID.EPDM_OVERWRITE_ON;
            _struct.tagMediaLayoutInf = new EPDMMediaLayoutInf.EPDM_MEDIALAYOUTINF();
            StructureToPtr();
            PtrToStructure();
        }

        #endregion

        #region Methods

        //-------------------------------------------------------------------
        // PtrToStructure
        // Comments		Updates the pointer of the member variable to obtain the latest information.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void PtrToStructure()
		{
            _struct = (EPDM_MEDIALAYOUTADD)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTADD));
		}

        //-------------------------------------------------------------------
        // StructureToPtr
        // Comments		Struct the pointer member variable.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void StructureToPtr()
		{
            Marshal.StructureToPtr( _struct, _ptrToStruct, false );
        }

        //-------------------------------------------------------------------
        // Dispose
        // Comments		Dispose the resources allocated.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        #endregion
    }

    class EPDMMediaLayoutAdd_3 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTADD_3
        {
            public uint dwVersion;
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] szName;
            public uint dwOverWrite;
            public EPDMMediaLayoutInf_3.EPDM_MEDIALAYOUTINF_3 tagMediaLayoutInf_3;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIALAYOUTADD_3 _struct;

        #endregion

        #region Property

        public String Name
        {
            get
            {
                // Get the code page using the OS.  
                int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                Encoding _Enc = Encoding.GetEncoding(Codepage);

                return new String(_Enc.GetString(_struct.szName).ToCharArray());
            }
            set
            {

                // Get the code page using the OS.  
                int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                Encoding _Enc = Encoding.GetEncoding(Codepage);

                String strValue = value;
                byte[] strData = _Enc.GetBytes(strValue);

                int nLen = strData.Length;

                if (nLen > 128)
                {
                    nLen = 128;
                }
                for (int i = 0; i < nLen; i++)
                {
                    _struct.szName[i] = strData[i];
                }
            }
        }

        public uint Overwrite
        {
            get
            {
                return _struct.dwOverWrite;
            }
            set
            {
                _struct.dwOverWrite = value;
            }
        }

        public EPDMMediaLayoutInf_3.EPDM_MEDIALAYOUTINF_3 MediaLayoutInf_3
        {
            get
            {
                return _struct.tagMediaLayoutInf_3;
            }
            set
            {
                _struct.tagMediaLayoutInf_3 = value;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutAdd
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutAdd_3()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));
            _struct = new EPDM_MEDIALAYOUTADD_3();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTADD_3;
            _struct.dwSize = (uint)(Marshal.SizeOf(_struct));
            _struct.szName = null;
            _struct.dwOverWrite = (uint)OverWriteID.EPDM_OVERWRITE_ON;
            _struct.tagMediaLayoutInf_3 = new EPDMMediaLayoutInf_3.EPDM_MEDIALAYOUTINF_3();
            StructureToPtr();
            PtrToStructure();
        }

        #endregion

        #region Methods

        //-------------------------------------------------------------------
        // PtrToStructure
        // Comments		Updates the pointer of the member variable to obtain the latest information.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void PtrToStructure()
        {
            _struct = (EPDM_MEDIALAYOUTADD_3)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTADD_3));
        }

        //-------------------------------------------------------------------
        // StructureToPtr
        // Comments		Struct the pointer member variable.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void StructureToPtr()
        {
            Marshal.StructureToPtr(_struct, _ptrToStruct, false);
        }

        //-------------------------------------------------------------------
        // Dispose
        // Comments		Dispose the resources allocated.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        #endregion
    }

    class EPDMMediaLayoutAdd_4 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTADD_4
        {
            public uint dwVersion;
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] szName;
            public uint dwOverWrite;
            public EPDMMediaLayoutInf_4.EPDM_MEDIALAYOUTINF_4 tagMediaLayoutInf_4;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIALAYOUTADD_4 _struct;

        #endregion

        #region Property

        public String Name
        {
            get
            {
                // Get the code page using the OS.  
                int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                Encoding _Enc = Encoding.GetEncoding(Codepage);

                return new String(_Enc.GetString(_struct.szName).ToCharArray());
            }
            set
            {

                // Get the code page using the OS.  
                int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                Encoding _Enc = Encoding.GetEncoding(Codepage);

                String strValue = value;
                byte[] strData = _Enc.GetBytes(strValue);

                int nLen = strData.Length;

                if (nLen > 128)
                {
                    nLen = 128;
                }
                for (int i = 0; i < nLen; i++)
                {
                    _struct.szName[i] = strData[i];
                }
            }
        }

        public uint Overwrite
        {
            get
            {
                return _struct.dwOverWrite;
            }
            set
            {
                _struct.dwOverWrite = value;
            }
        }

        public EPDMMediaLayoutInf_4.EPDM_MEDIALAYOUTINF_4 MediaLayoutInf_4
        {
            get
            {
                return _struct.tagMediaLayoutInf_4;
            }
            set
            {
                _struct.tagMediaLayoutInf_4 = value;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutAdd
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutAdd_4()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));
            _struct = new EPDM_MEDIALAYOUTADD_4();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTADD_4;
            _struct.dwSize = (uint)(Marshal.SizeOf(_struct));
            _struct.szName = null;
            _struct.dwOverWrite = (uint)OverWriteID.EPDM_OVERWRITE_ON;
            _struct.tagMediaLayoutInf_4 = new EPDMMediaLayoutInf_4.EPDM_MEDIALAYOUTINF_4();
            StructureToPtr();
            PtrToStructure();
        }

        #endregion

        #region Methods

        //-------------------------------------------------------------------
        // PtrToStructure
        // Comments		Updates the pointer of the member variable to obtain the latest information.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void PtrToStructure()
        {
            _struct = (EPDM_MEDIALAYOUTADD_4)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTADD_4));
        }

        //-------------------------------------------------------------------
        // StructureToPtr
        // Comments		Struct the pointer member variable.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void StructureToPtr()
        {
            Marshal.StructureToPtr(_struct, _ptrToStruct, false);
        }

        //-------------------------------------------------------------------
        // Dispose
        // Comments		Dispose the resources allocated.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        #endregion
    }

}
