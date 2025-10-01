using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    class ENSDeviceID : ENS
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct DEVICEID01
        {
            private const int LENGTH = 256;

            public int Size;
            public int Version;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LENGTH)]
            public char[] MFG;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LENGTH)]
            public char[] CMD;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LENGTH)]
            public char[] MDL;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = LENGTH)]
            public char[] CLS;
            public int PrnType;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private DEVICEID01 _struct;

        #endregion

        #region Property

        public int Size
        {
            get
            {
                return _struct.Size;
            }
        }
        public String Mfg
        {
            get
            {
                return new String(_struct.MFG);
            }
        }

        public String Cmd
        {
            get
            {
                return new String(_struct.CMD);
            }
        }

        public String Mdl
        {
            get
            {
                return new String(_struct.MDL);
            }
        }

        public String Cls
        {
            get
            {
                return new String(_struct.CLS);
            }
        }

        public int PrnType
        {
            get
            {
                return _struct.PrnType;
            }
        }

        #endregion

        #region Constructors/Destructors

        /////////////////////////////////////////////////////////////////////
        // ENSDeviceID
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public ENSDeviceID()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new DEVICEID01();
            _struct.Size = Marshal.SizeOf(_struct);
            _struct.Version = 0x1;
            _struct.PrnType = (int)ENSPathType.TYPE_REMOTE;
            StructureToPtr();
            PtrToStructure();
        }

        #endregion

        #region Methods

        /////////////////////////////////////////////////////////////////////
        // PtrToStructure
        // Comments		Updates the pointer of the member variable to obtain the latest information.
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public override void PtrToStructure()
        {
            _struct = (DEVICEID01)Marshal.PtrToStructure(_ptrToStruct, typeof(DEVICEID01));
        }

        /////////////////////////////////////////////////////////////////////
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

        /////////////////////////////////////////////////////////////////////
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
