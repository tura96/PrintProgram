using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct STATUSVERSION
    {
        public short MajorVersion;
        public short MinerVersion;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct PAPERPATHINFO
    {
        public byte Type;
        public byte Path;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct CHARASTATUSINFO01
    {
        public byte Version;
        public int StartDate;
        public int TotalPrintedLine;
        public short TotalTime;
        public short CSFBin1PaperSize;
        public short CSFBin2PaperSize;
        public short CSFBin3PaperSize;
        public short SeriesPaperWidth;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct CHARASTATUSINFO02
    {
        public byte Version;
        public int StartDate;
        public int TotalPrintedLine;
        public short TotalTime;
        public int ChangeRibonLine;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct CHARASTATUSINFO03
    {
        public byte Version;
        public short CSFBin1PaperSize;
        public byte CSFBin1PaperType;
        public byte CSFBin1PaperRest;
        public short CSFBin2PaperSize;
        public byte CSFBin2PaperType;
        public byte CSFBin2PaperRest;
        public byte SIMMVolume;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct TRAYFIELDINFO
    {
        public byte FieldType;
        public byte Position;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct PAPERTRAYINFO
    {
        public byte TrayType;
        public byte TrayVolume;
        public TRAYFIELDINFO TrayField;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct JOBNAMEINFO
    {
        public int JobID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public char[] Jobname;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct CARTRIDGEANDINKINFO
    {
        public byte CartridgeType;
        public int ColorType;
        public byte InkRest;
        public byte InkDimension;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct HEATERTEMPERATUREINFO
    {
        public byte TemperatureUnit;
        public byte TemperatureSetForPreHeater;
        public byte PreHeaterTemperature;
        public byte PreHeaterInfo;
        public byte TemperatureSetForPlatenHeater;
        public byte PlatenHeaterTemperature;
        public byte PlatenHeaterInfo;
        public byte TemperatureSetForAfterHeater;
        public byte AfterHeaterTemperature;
        public byte AfterHeaterInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct ROMCODEINFO
    {
        public byte Size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public byte[] Data;
    }

    class ENSStatus : ENS
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct INKSIDMSTATUS_02
        {
            public int StatusSize;
            public STATUSVERSION Version;
            public byte StatusCode;
            public byte ErrorCode;
            public byte SelfPrintCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] WarmingCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] RestCSFPaper;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] ReserveRestCSFPaper;
            public PAPERPATHINFO PaperPath;
            public short PaperSizeError;
            public byte PaperTypeError;
            public PAPERPATHINFO PaperPathError;
            public CHARASTATUSINFO01 CharaStatus01;
            public CHARASTATUSINFO02 CharaStatus02;
            public CHARASTATUSINFO03 CharaStatus03;
            public short CopyPrintNumber;
            public byte InkColorNumber;
            public byte MicroWeaveInfo;
            public short CleaningTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public byte[] PaperSelectStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] ReservePaperSelectStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public CARTRIDGEANDINKINFO[] CartridgeInk;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public PAPERTRAYINFO[] PaperTray;
            public byte PrintTec;
            public int ReplaceCartridge;
            public short RomCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] InkRemainInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] ReserveInkRemainInfo;
            public byte CancelCode;
            public byte CutterInfo;
            public byte PaperJamInfo;
            public byte RouteChangeLeverInfo;
            public JOBNAMEINFO JobNameInfo;
            public int ColorimetricCalibrationIDSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
            public byte[] ColorimetricCalibrationID;
            public byte PaperRestUnit;
            public int PaperRestValue;
            public short PaperWidth;
            public byte PaperInfoType;
            public byte PaperInfoValue;
            public byte InkSelect;
            public HEATERTEMPERATUREINFO HeaterTemperature;
            public byte AvailableWarningCode2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] WarningCode2;
            public byte PrinterMachineInfo2SetFlag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] PrinterMachineInfo2;
            public byte FatalErrorCodeSetFlag;
            public byte FatalErrorCode;
            public byte ReprintInfo;
            public byte StatusReplyType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] SerialNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] InkMaintenanceLimit;
            public ROMCODEINFO RomCode2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 194)]
            public byte[] Reserved;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        public INKSIDMSTATUS_02 _struct;

        #endregion

        #region Property

        public byte StatusCode
        {
            get
            {
                return _struct.StatusCode;
            }
        }
        public byte ErrorCode
        {
            get
            {
                return _struct.ErrorCode;
            }
        }
        public byte[] WarningCode
        {
            get
            {
                return _struct.WarningCode2;
            }
        }

        #endregion

        #region Constructors/Destructors

        /////////////////////////////////////////////////////////////////////
        // ENSStatus
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public ENSStatus()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem( Marshal.SizeOf(_struct) );

            _struct = new INKSIDMSTATUS_02();
            _struct.StatusSize = Marshal.SizeOf(_struct);
            _struct.Version = new STATUSVERSION();
            _struct.StatusCode = 0;
            _struct.ErrorCode = 0;
            _struct.SelfPrintCode = 0;
            _struct.PaperPath = new PAPERPATHINFO();
            _struct.PaperSizeError = 0;
            _struct.PaperTypeError = 0;
            _struct.PaperPathError = new PAPERPATHINFO();
            _struct.CharaStatus01 = new CHARASTATUSINFO01();
            _struct.CharaStatus02 = new CHARASTATUSINFO02();
            _struct.CharaStatus03 = new CHARASTATUSINFO03();
            _struct.CopyPrintNumber = 0;
            _struct.InkColorNumber = 0;
            _struct.MicroWeaveInfo = 0;
            _struct.CleaningTime = 0;
            _struct.PrintTec = 0;
            _struct.ReplaceCartridge = 0;
            _struct.RomCode = 0;
            _struct.CancelCode = 0;
            _struct.CutterInfo = 0;
            _struct.PaperJamInfo = 0;
            _struct.RouteChangeLeverInfo = 0;
            _struct.JobNameInfo = new JOBNAMEINFO();
            _struct.ColorimetricCalibrationIDSize = 0;
            _struct.PaperRestUnit = 0;
            _struct.PaperRestValue = 0;
            _struct.PaperWidth = 0;
            _struct.PaperInfoType = 0;
            _struct.PaperInfoValue = 0;
            _struct.InkSelect = 0;
            _struct.HeaterTemperature = new HEATERTEMPERATUREINFO();
            _struct.AvailableWarningCode2 = 0;
            _struct.PrinterMachineInfo2SetFlag = 0;
            _struct.FatalErrorCodeSetFlag = 0;
            _struct.FatalErrorCode = 0;            
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
            _struct = (INKSIDMSTATUS_02)Marshal.PtrToStructure(_ptrToStruct, typeof(INKSIDMSTATUS_02));
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
