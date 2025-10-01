using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    public class EPDMMediaLayoutRange : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTRANGE
        {
            public uint dwVersion;
            public uint dwSize;
            public short iIDCount;
            public short iMediaTypeCount;
            public IntPtr lpMediaLayoutID;
            public IntPtr lpMediaTypeRange;
            public short iMaxString;
            public short iMaxCount;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIALAYOUTRANGE _struct;

        #endregion

        #region Property

        public StructVersion Version
        {
            get
            {
                return (StructVersion)_struct.dwVersion;
            }
        }

        public short IDCount
        {
            get
            {
                return _struct.iIDCount;
            }
        }

        public short[] MediaLayoutID
        {
            get
            {
                try
                {
                    short[] arr = new short[_struct.iIDCount];

                    for (int i = 0; i < _struct.iIDCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpMediaLayoutID.ToInt64() + (sizeof(short) * i));
                        arr[i] = (short)Marshal.PtrToStructure(current, typeof(short));
                    }
                    return arr;
                }
                catch (Exception)
                {
                    // Error handling.
                    throw;
                }
            }
        }

        public EPDMMediaTypeRange.EPDM_MEDIATYPERANGE[] MediaTypeRange
        {
            get
            {
                try
                {
                    EPDMMediaTypeRange.EPDM_MEDIATYPERANGE[] arr = new EPDMMediaTypeRange.EPDM_MEDIATYPERANGE[_struct.iMediaTypeCount];

                    for (int i = 0; i < _struct.iMediaTypeCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpMediaTypeRange.ToInt64() + (Marshal.SizeOf(typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE)) * i));
                        arr[i] = ((EPDMMediaTypeRange.EPDM_MEDIATYPERANGE)(Marshal.PtrToStructure(current, typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE))));
                    }
                    return arr;
                }
                catch (Exception)
                {
                    // Error handling.
                    throw;
                }
            }
        }

        public short MediaTypeCount
        {
            get
            {
                return _struct.iMediaTypeCount;
            }
        }

        public short MaxString
        {
            get
            {
                return _struct.iMaxString;
            }
        }

        public short MaxCount
        {
            get
            {
                return _struct.iMaxCount;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutRange
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutRange()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem( Marshal.SizeOf( _struct ) );

            _struct = new EPDM_MEDIALAYOUTRANGE();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTRANGE;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iIDCount = 0;
            _struct.iMediaTypeCount = 0;
            _struct.lpMediaLayoutID = IntPtr.Zero;
            _struct.lpMediaTypeRange = IntPtr.Zero;
            _struct.iMaxString = 0;
            _struct.iMaxCount = 0;
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
            _struct = (EPDM_MEDIALAYOUTRANGE)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTRANGE));
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
                Marshal.FreeCoTaskMem(_struct.lpMediaLayoutID);
                Marshal.FreeCoTaskMem(_struct.lpMediaTypeRange);

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        public void Alloc()
        {
            try
            {
                // iIDCount、iMediaTypeCount = 0 is EPDM_ERR_FAIL.
                if (_struct.iIDCount == 0 || _struct.iMediaTypeCount == 0)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }

                // Free the memory.
                Marshal.FreeCoTaskMem(_struct.lpMediaLayoutID);
                Marshal.FreeCoTaskMem(_struct.lpMediaTypeRange);

                // Allocate the memory. - EPDMLayoutRange.mediaLayoutID
                _struct.lpMediaLayoutID = Marshal.AllocCoTaskMem(_struct.iIDCount * sizeof(short));

                // Allocate the memory. - EPDMLayoutRange.mediaTypeRange
                _struct.lpMediaTypeRange = Marshal.AllocCoTaskMem(_struct.iMediaTypeCount * Marshal.SizeOf(typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE)));

                // Update the unmanaged memory. - EPDMLayoutRange.mediaTypeRange
                EPDMMediaTypeRange.EPDM_MEDIATYPERANGE[] epdmMediaTypeRange = new EPDMMediaTypeRange.EPDM_MEDIATYPERANGE[_struct.iMediaTypeCount];
                for (int i = 0; i < _struct.iMediaTypeCount; i++)
                {
                    IntPtr current = new IntPtr(_struct.lpMediaTypeRange.ToInt64() + (Marshal.SizeOf(typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE)) * i));
                    epdmMediaTypeRange[i] = (EPDMMediaTypeRange.EPDM_MEDIATYPERANGE)Marshal.PtrToStructure(current, typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE));

                    epdmMediaTypeRange[i].dwVersion = (uint)StructVersion.EPDM_STVER_MEDIATYPERANGE;
                    epdmMediaTypeRange[i].dwSize = (uint)Marshal.SizeOf(typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE));

                    IntPtr current2 = new IntPtr(_struct.lpMediaTypeRange.ToInt64() + (Marshal.SizeOf(typeof(EPDMMediaTypeRange.EPDM_MEDIATYPERANGE)) * i));
                    Marshal.StructureToPtr(epdmMediaTypeRange[i], current2, false);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        public int GetMediaTypeIndex(MediaTypeID mediaTypeID)
        {
            try
            {
                int i = -1;

                if (_struct.iMediaTypeCount <= 0)
                {
                    return i;
                }

                for (i = 0; i < _struct.iMediaTypeCount; i++)
                {
                    ushort typeID = MediaTypeRange[i].iTypeID;
                    if (typeID == (ushort)mediaTypeID)
                    {
                        break;
                    }
                }
                return i;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        #endregion
    }

    public class EPDMMediaLayoutRange_3 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTRANGE_3
        {
            public uint dwVersion;
            public uint dwSize;
            public short iIDCount;
            public IntPtr lpMediaLayoutID;
            public short iMaxString;
            public short iMaxCount;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIALAYOUTRANGE_3 _struct;

        #endregion

        #region Property

        public StructVersion Version
        {
            get
            {
                return (StructVersion)_struct.dwVersion;
            }
        }

        public short IDCount
        {
            get
            {
                return _struct.iIDCount;
            }
        }

        public short[] MediaLayoutID
        {
            get
            {
                try
                {
                    short[] arr = new short[_struct.iIDCount];

                    for (int i = 0; i < _struct.iIDCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpMediaLayoutID.ToInt64() + (sizeof(short) * i));
                        arr[i] = (short)Marshal.PtrToStructure(current, typeof(short));
                    }
                    return arr;
                }
                catch (Exception)
                {
                    // Error handling.
                    throw;
                }
            }
        }

        public short MaxString
        {
            get
            {
                return _struct.iMaxString;
            }
        }

        public short MaxCount
        {
            get
            {
                return _struct.iMaxCount;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutRange
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutRange_3()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new EPDM_MEDIALAYOUTRANGE_3();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTRANGE_3;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iIDCount = 0;
            _struct.lpMediaLayoutID = IntPtr.Zero;
            _struct.iMaxString = 0;
            _struct.iMaxCount = 0;
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
            _struct = (EPDM_MEDIALAYOUTRANGE_3)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTRANGE_3));
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
                Marshal.FreeCoTaskMem(_struct.lpMediaLayoutID);

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        public void Alloc()
        {
            try
            {
                // iIDCount = 0 is EPDM_ERR_FAIL.
                if (_struct.iIDCount == 0)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }

                // Free the memory.
                Marshal.FreeCoTaskMem(_struct.lpMediaLayoutID);

                // Allocate the memory. - EPDMLayoutRange.mediaLayoutID
                _struct.lpMediaLayoutID = Marshal.AllocCoTaskMem(_struct.iIDCount * sizeof(short));

             }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        #endregion
    }

    public class EPDMMediaLayoutRange_4 : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_MEDIALAYOUTRANGE_4
        {
            public uint dwVersion;
            public uint dwSize;
            public short iIDCount;
            public IntPtr lpMediaLayoutID;
            public short iMaxString;
            public short iMaxCount;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_MEDIALAYOUTRANGE_4 _struct;

        #endregion

        #region Property

        public StructVersion Version
        {
            get
            {
                return (StructVersion)_struct.dwVersion;
            }
        }

        public short IDCount
        {
            get
            {
                return _struct.iIDCount;
            }
        }

        public short[] MediaLayoutID
        {
            get
            {
                try
                {
                    short[] arr = new short[_struct.iIDCount];

                    for (int i = 0; i < _struct.iIDCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpMediaLayoutID.ToInt64() + (sizeof(short) * i));
                        arr[i] = (short)Marshal.PtrToStructure(current, typeof(short));
                    }
                    return arr;
                }
                catch (Exception)
                {
                    // Error handling.
                    throw;
                }
            }
        }

        public short MaxString
        {
            get
            {
                return _struct.iMaxString;
            }
        }

        public short MaxCount
        {
            get
            {
                return _struct.iMaxCount;
            }
        }

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMMediaLayoutRange
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMMediaLayoutRange_4()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(_struct));

            _struct = new EPDM_MEDIALAYOUTRANGE_4();
            _struct.dwVersion = (uint)StructVersion.EPDM_STVER_MEDIALAYOUTRANGE_4;
            _struct.dwSize = (uint)Marshal.SizeOf(_struct);
            _struct.iIDCount = 0;
            _struct.lpMediaLayoutID = IntPtr.Zero;
            _struct.iMaxString = 0;
            _struct.iMaxCount = 0;
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
            _struct = (EPDM_MEDIALAYOUTRANGE_4)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_MEDIALAYOUTRANGE_4));
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
                Marshal.FreeCoTaskMem(_struct.lpMediaLayoutID);

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        public void Alloc()
        {
            try
            {
                // iIDCount = 0 is EPDM_ERR_FAIL.
                if (_struct.iIDCount == 0)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }

                // Free the memory.
                Marshal.FreeCoTaskMem(_struct.lpMediaLayoutID);

                // Allocate the memory. - EPDMLayoutRange.mediaLayoutID
                _struct.lpMediaLayoutID = Marshal.AllocCoTaskMem(_struct.iIDCount * sizeof(short));

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