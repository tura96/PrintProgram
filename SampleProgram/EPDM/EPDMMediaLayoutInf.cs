using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    class EPDMMediaLayoutInf : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTINF
        {
            public uint dwVersion;
            public uint dwSize;
            public short iPaperWid;
            public short iPaperHig;
            public short iLabelWid;
            public short iLabelHig;
            public short iLabelGap;
            public short iBMGap;
            public short iPrintWid;
            public short iPrintHig;
            public short iPrintX;
            public short iPrintY;
            public ushort iMediaTypeID;
            public ushort iMediaLayoutID;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        public EPDM_MEDIALAYOUTINF _struct;

        #endregion

        #region Property

        public short PaperWid
        {
            get
            {
                return _struct.iPaperWid;
            }
            set
            {
                _struct.iPaperWid = value;
            }
        }

        public short PaperHig
        {
            get
            {
                return _struct.iPaperHig;
            }
            set
            {
                _struct.iPaperHig = value;
            }
        }

        public short LabelWid
        {
            get
            {
                return _struct.iLabelWid;
            }
            set
            {
                _struct.iLabelWid = value;
            }
        }

        public short LabelHig
        {
            get
            {
                return _struct.iLabelHig;
            }
            set
            {
                _struct.iLabelHig = value;
            }
        }

        public short LabelGap
        {
            get
            {
                return _struct.iLabelGap;
            }
            set
            {
                _struct.iLabelGap = value;
            }
        }

        public short BMGap
        {
            get
            {
                return _struct.iBMGap;
            }
            set
            {
                _struct.iBMGap = value;
            }
        }

        public short PrintWid
        {
            get
            {
                return _struct.iPrintWid;
            }
            set
            {
                _struct.iPrintWid = value;
            }
        }

        public short PrintHig
        {
            get
            {
                return _struct.iPrintHig;
            }
            set
            {
                _struct.iPrintHig = value;
            }
        }

        public short PrintX
        {
            get
            {
                return _struct.iPrintX;
            }
            set
            {
                _struct.iPrintX = value;
            }
        }

        public short PrintY
        {
            get
            {
                return _struct.iPrintY;
            }
            set
            {
                _struct.iPrintY = value;
            }
        }

        public ushort MediaTypeID
        {
            get
            {
                return _struct.iMediaTypeID;
            }
            set
            {
                _struct.iMediaTypeID = value;
            }
        }

        public ushort MediaLayoutID
        {
            get
            {
                return _struct.iMediaLayoutID;
            }
            set
            {
                _struct.iMediaLayoutID = value;
            }
        }
        
        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutInf
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutInf()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem( Marshal.SizeOf( _struct ) );

            _struct = new EPDM_MEDIALAYOUTINF();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTINF;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iPaperWid = 0;
            _struct.iPaperHig = 0;
            _struct.iLabelWid = 0;
            _struct.iLabelHig = 0;
            _struct.iLabelGap = 0;
            _struct.iBMGap = 0;
            _struct.iPrintWid = 0;
            _struct.iPrintHig = 0;
            _struct.iPrintX = 0;
            _struct.iPrintY = 0;
            _struct.iMediaTypeID = 0;
            _struct.iMediaLayoutID = 0;
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
            _struct = (EPDM_MEDIALAYOUTINF)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTINF));
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

    class EPDMMediaLayoutInf_3 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTINF_3
        {
            public uint dwVersion;
            public uint dwSize;
            public ushort iMediaLayoutID;
            public ushort iMediaTypeID;
            public ushort iMediaID;
            public short iLabelWid;
            public short iLabelHig;
            public ushort iAutoCutID;
            public short iAutoCutNum;
            public ushort iMediaSavingID;
            public ushort iQualityID;
            public ushort iColorManagement;
            public ushort iColorMode;
            public short iGamma;
            public ushort iInkProfileAndBrightness;
            public short iInkProfileBkCMY;
            public short iInkProfileBk;
            public short iBrightness;
            public ushort iBiDiPrinting;
            public ushort iBandingReduction;
            public ushort iRsv1;
            public ushort iRsv2;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        public EPDM_MEDIALAYOUTINF_3 _struct;

        #endregion

        #region Property

        public short LabelWid
        {
            get
            {
                return _struct.iLabelWid;
            }
            set
            {
                _struct.iLabelWid = value;
            }
        }

        public short LabelHig
        {
            get
            {
                return _struct.iLabelHig;
            }
            set
            {
                _struct.iLabelHig = value;
            }
        }


        public ushort MediaTypeID
        {
            get
            {
                return _struct.iMediaTypeID;
            }
            set
            {
                _struct.iMediaTypeID = value;
            }
        }

        public ushort MediaLayoutID
        {
            get
            {
                return _struct.iMediaLayoutID;
            }
            set
            {
                _struct.iMediaLayoutID = value;
            }
        }


        public ushort MediaID
        {
            get
            {
                return _struct.iMediaID;
            }
            set
            {
                _struct.iMediaID = value;
            }
        }

        public ushort QualityID
        {
            get
            {
                return _struct.iQualityID;
            }
            set
            {
                _struct.iQualityID = value;
            }
        }

        public ushort AutoCutID
        {
            get
            {
                return _struct.iAutoCutID;
            }
            set
            {
                _struct.iAutoCutID = value;
            }
        }

        public short AutoCutNum
        {
            get
            {
                return _struct.iAutoCutNum;
            }
            set
            {
                _struct.iAutoCutNum = value;
            }
        }

        public ushort MediaSavingID
        {
            get
            {
                return _struct.iMediaSavingID;
            }
            set
            {
                _struct.iMediaSavingID = value;
            }
        }

        public ushort BiDiPrinting
        {
            get
            {
                return _struct.iBiDiPrinting;
            }
            set
            {
                _struct.iBiDiPrinting = value;
            }
        }

        public ushort BandingReduction
        {
            get
            {
                return _struct.iBandingReduction;
            }
            set
            {
                _struct.iBandingReduction = value;
            }
        }

        public ushort ColorManagement
        {
            get
            {
                return _struct.iColorManagement;
            }
            set
            {
                _struct.iColorManagement = value;
            }
        }

        public ushort ColorMode
        {
            get
            {
                return _struct.iColorMode;
            }
            set
            {
                _struct.iColorMode = value;
            }
        }

        public short Gamma
        {
            get
            {
                return _struct.iGamma;
            }
            set
            {
                _struct.iGamma = value;
            }
        }

        public ushort InkProfileAndBrightness
        {
            get
            {
                return _struct.iInkProfileAndBrightness;
            }
            set
            {
                _struct.iInkProfileAndBrightness = value;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutInf
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutInf_3()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new EPDM_MEDIALAYOUTINF_3();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTINF_3;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iLabelWid = 0;
            _struct.iLabelHig = 0;
            _struct.iMediaTypeID = 0;
            _struct.iMediaLayoutID = 0;
            _struct.iMediaID = 0;
            _struct.iQualityID = 0;

            _struct.iAutoCutID = 0x0001;
            _struct.iAutoCutNum = 0;
            _struct.iMediaSavingID = 0;
            _struct.iBiDiPrinting = 0xFFFF;
            _struct.iBandingReduction = 0xFFFF;

            _struct.iColorManagement = 0;
            _struct.iColorMode = 0;
            _struct.iGamma = 0;
            _struct.iInkProfileAndBrightness = 0;

            _struct.iInkProfileBkCMY = 0;
            _struct.iInkProfileBk = 0;
            _struct.iBrightness = 0;
            _struct.iRsv1 = 0;
            _struct.iRsv2 = 0;

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
            _struct = (EPDM_MEDIALAYOUTINF_3)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTINF_3));
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

    class EPDMMediaLayoutInf_4 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTINF_4
        {
            public uint dwVersion;
            public uint dwSize;
            public ushort iMediaLayoutID;
            public short iLabelWid;
            public short iLabelHig;
            public short sLabelInterval;
            public ushort iMediaTypeID;
            public ushort iMediaSavingID;
            public ushort iMediaID;
            public ushort iQualityID;
            public ushort iAutoCutID;
            public short iAutoCutNum;
            public ushort iInkProfileEnable;
            public short iInkProfile;
            public ushort iBlackRatioEnable;
            public short iBlackRatio;
            public ushort iBiDiPrinting;
            public ushort iRsv;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        public EPDM_MEDIALAYOUTINF_4 _struct;

        #endregion

        #region Property

        public ushort MediaLayoutID
        {
            get
            {
                return _struct.iMediaLayoutID;
            }
            set
            {
                _struct.iMediaLayoutID = value;
            }
        }

        public short LabelWid
        {
            get
            {
                return _struct.iLabelWid;
            }
            set
            {
                _struct.iLabelWid = value;
            }
        }

        public short LabelHig
        {
            get
            {
                return _struct.iLabelHig;
            }
            set
            {
                _struct.iLabelHig = value;
            }
        }

        public short LabelInterval
        {
            get
            {
                return _struct.sLabelInterval;
            }
            set
            {
                _struct.sLabelInterval = value;
            }
        }

        public ushort MediaTypeID
        {
            get
            {
                return _struct.iMediaTypeID;
            }
            set
            {
                _struct.iMediaTypeID = value;
            }
        }

        public ushort MediaSavingID
        {
            get
            {
                return _struct.iMediaSavingID;
            }
            set
            {
                _struct.iMediaSavingID = value;
            }
        }

        public ushort MediaID
        {
            get
            {
                return _struct.iMediaID;
            }
            set
            {
                _struct.iMediaID = value;
            }
        }

        public ushort QualityID
        {
            get
            {
                return _struct.iQualityID;
            }
            set
            {
                _struct.iQualityID = value;
            }
        }

        public ushort AutoCutID
        {
            get
            {
                return _struct.iAutoCutID;
            }
            set
            {
                _struct.iAutoCutID = value;
            }
        }

        public short AutoCutNum
        {
            get
            {
                return _struct.iAutoCutNum;
            }
            set
            {
                _struct.iAutoCutNum = value;
            }
        }

        public ushort InkProfileEnable
        {
            get
            {
                return _struct.iInkProfileEnable;
            }
            set
            {
                _struct.iInkProfileEnable = value;
            }
        }

        public short InkProfile
        {
            get
            {
                return _struct.iInkProfile;
            }
            set
            {
                _struct.iInkProfile = value;
            }
        }

        public ushort BlackRatioEnable
        {
            get
            {
                return _struct.iBlackRatioEnable;
            }
            set
            {
                _struct.iBlackRatioEnable = value;
            }
        }

        public short BlackRatio
        {
            get
            {
                return _struct.iBlackRatio;
            }
            set
            {
                _struct.iBlackRatio = value;
            }
        }

        public ushort BiDiPrinting
        {
            get
            {
                return _struct.iBiDiPrinting;
            }
            set
            {
                _struct.iBiDiPrinting = value;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutInf
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutInf_4()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new EPDM_MEDIALAYOUTINF_4();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTINF_4;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iMediaLayoutID = 0;
            _struct.iLabelWid = 0;
            _struct.iLabelHig = 0;
            _struct.sLabelInterval = 0;
            _struct.iMediaTypeID = 0;
            _struct.iMediaSavingID = 0;
            _struct.iMediaID = 0;
            _struct.iQualityID = 0;
            _struct.iAutoCutID = 0x0001;
            _struct.iAutoCutNum = 0;
            _struct.iInkProfileEnable = 0;
            _struct.iInkProfile = 0;
            _struct.iBlackRatioEnable = 0;
            _struct.iBlackRatio = 0;
            _struct.iBiDiPrinting = 0xFFFF;
            _struct.iRsv = 0;

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
            _struct = (EPDM_MEDIALAYOUTINF_4)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTINF_4));
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
