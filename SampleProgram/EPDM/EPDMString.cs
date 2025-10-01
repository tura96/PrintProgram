using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    class EPDMString : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_STRING_1
        {
            public uint dwVersion;
            public uint dwSize;
            public uint dwStrSize;
            public uint dwCommand;
            public uint dwID;
            public IntPtr lpString;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_STRING_1 _struct;

        #endregion

        #region Property

        public uint Size
        {
            get
            {
                return _struct.dwSize;
            }
        }

        public uint StrSize
        {
            get
            {
                return _struct.dwStrSize;
            }
        }

        public uint Command
        {
            get
            {
                return _struct.dwCommand;
            }
            set
            {
                _struct.dwCommand = value;
            }
        }

        public uint ID
        {
            get
            {
                return _struct.dwID;
            }
            set
            {
                _struct.dwID = value;
            }
        }

        public String String
        {
            get
            {
                try
                {
                    // Get the code page using the OS.  
                    int Codepage = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage;
                    Encoding _Enc = Encoding.GetEncoding(Codepage);

                    byte[] StringData = new byte[_struct.dwStrSize];

                    // Deploy the memory to array.
                    int i = 0;
                    for (i = 0; i < _struct.dwStrSize; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpString.ToInt64() + i);
                        StringData[i] = (byte)Marshal.PtrToStructure(current, typeof(byte));

                        if (StringData[i] == 0)
                        {
                            break;
                        }
                    }

                    return _Enc.GetString(StringData, 0, i);
                }
                catch (Exception)
                {
                    // Error handling.                
                    throw;
                }
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMString
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMString()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem( Marshal.SizeOf( _struct ) );

            _struct = new EPDM_STRING_1();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_STRING_1;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.dwStrSize = 0;
            _struct.dwCommand = 0;
            _struct.dwID = 0;
            _struct.lpString = IntPtr.Zero;
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
            _struct = (EPDM_STRING_1)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_STRING_1));
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
                Marshal.FreeCoTaskMem(_struct.lpString);

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        public void Alloc()
        {
            try
            {
                // m_Struct.dwStrSize == 0 is EPDM_ERR_FAIL.
                if (_struct.dwStrSize == 0)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }
                
                // Free the memory.
                Marshal.FreeCoTaskMem(_struct.lpString);

                // Allocate the memory. - EPDMString.lpString
                _struct.lpString = Marshal.AllocCoTaskMem((int)_struct.dwStrSize);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }       

        #endregion
    }
}