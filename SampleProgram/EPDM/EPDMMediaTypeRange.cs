using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    public class EPDMMediaTypeRange : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIATYPERANGE
        {
            public uint dwVersion;
            public uint dwSize;
            public short iMaxPaperWid;
            public short iMaxPaperHig;
            public short iMaxLabelWid;
            public short iMaxLabelHig;
            public short iMaxLabelGap;
            public short iMaxBMGap;
            public short iMaxPrintWid;
            public short iMaxPrintHig;
            public short iMaxPrintX;
            public short iMaxPrintY;
            public short iMinPaperWid;
            public short iMinPaperHig;
            public short iMinLabelWid;
            public short iMinLabelHig;
            public short iMinLabelGap;
            public short iMinBMGap;
            public short iMinPrintWid;
            public short iMinPrintHig;
            public short iMinPrintX;
            public short iMinPrintY;
            public ushort iTypeID;
            public ushort iRsv;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIATYPERANGE _struct;

        #endregion

        #region property

        public short MaxPaperWid
        {
            get
            {
                return _struct.iMaxPaperWid;
            }
        }

        public short MaxPaperHig
        {
            get
            {
                return _struct.iMaxPaperHig;
            }
        }

        public short MaxLabelWid
        {
            get
            {
                return _struct.iMaxLabelWid;
            }
        }

        public short MaxLabelHig
        {
            get
            {
                return _struct.iMaxLabelHig;
            }
        }

        public short MaxLabelGap
        {
            get
            {
                return _struct.iMaxLabelGap;
            }
        }

        public short MinPaperWid
        {
            get
            {
                return _struct.iMinPaperWid;
            }
        }

        public short MinPaperHig
        {
            get
            {
                return _struct.iMinPaperHig;
            }
        }

        public short MinLabelWid
        {
            get
            {
                return _struct.iMinLabelWid;
            }
        }

        public short MinLabelHig
        {
            get
            {
                return _struct.iMinLabelHig;
            }
        }

        public short MinLabelGap
        {
            get
            {
                return _struct.iMinLabelGap;
            }
        }

        public ushort TypeID
        {
            get
            {
                return _struct.iTypeID;
            }
        }
             
        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaTypeRange
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaTypeRange()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem( Marshal.SizeOf(_struct) );

            _struct = new EPDM_MEDIATYPERANGE();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIATYPERANGE;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iMaxPaperWid = 0;
            _struct.iMaxPaperHig = 0;
            _struct.iMaxLabelWid = 0;
            _struct.iMaxLabelHig = 0;
            _struct.iMaxLabelGap = 0;
            _struct.iMaxBMGap = 0;
            _struct.iMaxPrintWid = 0;
            _struct.iMaxPrintHig = 0;
            _struct.iMaxPrintX = 0;
            _struct.iMaxPrintY = 0;
            _struct.iMinPaperWid = 0;
            _struct.iMinPaperHig = 0;
            _struct.iMinLabelWid = 0;
            _struct.iMinLabelHig = 0;
            _struct.iMinLabelGap = 0;
            _struct.iMinBMGap = 0;
            _struct.iMinPrintWid = 0;
            _struct.iMinPrintHig = 0;
            _struct.iMinPrintX = 0;
            _struct.iMinPrintY = 0;
            _struct.iTypeID = (ushort)MediaTypeID.EPDM_MEDIATYPE_DIECUT;
            _struct.iRsv = (ushort)0;
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
            _struct = (EPDM_MEDIATYPERANGE)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIATYPERANGE));
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

    public class EPDMMediaTypeRange_3 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIATYPERANGE_3
        {
            public uint dwVersion;
            public uint dwSize;
            public int iMaxLabelWid;
            public int iMaxLabelHig;
            public int iMinLabelWid;
            public int iMinLabelHig;
            public int iTypeID;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIATYPERANGE_3 _struct;

        #endregion

        #region property

        public int MaxLabelWid
        {
            get
            {
                return _struct.iMaxLabelWid;
            }
        }

        public int MaxLabelHig
        {
            get
            {
                return _struct.iMaxLabelHig;
            }
        }

        public int MinLabelWid
        {
            get
            {
                return _struct.iMinLabelWid;
            }
        }

        public int MinLabelHig
        {
            get
            {
                return _struct.iMinLabelHig;
            }
        }

        public int TypeID
        {
            get
            {
                return _struct.iTypeID;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaTypeRange
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaTypeRange_3()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new EPDM_MEDIATYPERANGE_3();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIATYPERANGE_3;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iMaxLabelWid = 0;
            _struct.iMaxLabelHig = 0;
            _struct.iMinLabelWid = 0;
            _struct.iMinLabelHig = 0;
            _struct.iTypeID = (int)MediaTypeID.EPDM_MEDIATYPE_DIECUT;
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
            _struct = (EPDM_MEDIATYPERANGE_3)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIATYPERANGE_3));
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

    public class EPDMMediaTypeRange_4 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIATYPERANGE_4
        {
            public uint dwVersion;
            public uint dwSize;
            public int iMaxLabelWid;
            public int iMaxLabelHig;
            public int iMinLabelWid;
            public int iMinLabelHig;
            public int iTypeID;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIATYPERANGE_4 _struct;

        #endregion

        #region property

        public int MaxLabelWid
        {
            get
            {
                return _struct.iMaxLabelWid;
            }
        }

        public int MaxLabelHig
        {
            get
            {
                return _struct.iMaxLabelHig;
            }
        }

        public int MinLabelWid
        {
            get
            {
                return _struct.iMinLabelWid;
            }
        }

        public int MinLabelHig
        {
            get
            {
                return _struct.iMinLabelHig;
            }
        }

        public int TypeID
        {
            get
            {
                return _struct.iTypeID;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaTypeRange
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaTypeRange_4()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new EPDM_MEDIATYPERANGE_4();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIATYPERANGE_4;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iMaxLabelWid = 0;
            _struct.iMaxLabelHig = 0;
            _struct.iMinLabelWid = 0;
            _struct.iMinLabelHig = 0;
            _struct.iTypeID = (int)MediaTypeID.EPDM_MEDIATYPE_DIECUT;
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
            _struct = (EPDM_MEDIATYPERANGE_4)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIATYPERANGE_4));
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