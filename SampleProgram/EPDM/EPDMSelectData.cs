using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace com.epson.label.driver
{
    public class EPDMSelectData : EPDM
    {
        #region Structs

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct EPDM_SELECTDATA
        {
            public short iCount;
            public short iSize;
            public IntPtr lpData;
        };

        #endregion

        #region Fields

        private bool _disposed = false;
        private EPDM_SELECTDATA _struct;

        #endregion

        #region property

        public short Count
        {
            get
            {
                return _struct.iCount;
            }
        }

        public short Size
        {
            get
            {
                return _struct.iSize;
            }
        }

        public Int64[] Data
        {
            get
            {
                try
                {
                    if (_struct.lpData == IntPtr.Zero || _struct.iSize > sizeof(Int64))
                    {
                        return null;
                    }

                    Int64[] arr = new Int64[_struct.iCount];

                    for (int i = 0; i < _struct.iCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpData.ToInt64() + (_struct.iSize * i));
                        arr[i] = GetSelectData(current);
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

        public EPDMMediaTypeRange_3.EPDM_MEDIATYPERANGE_3[] MEDIATYPERANGE_3
        {
            get
            {
                try
                {
                    if (_struct.lpData == IntPtr.Zero)
                    {
                        return null;
                    }

                    EPDMMediaTypeRange_3.EPDM_MEDIATYPERANGE_3[] arr = 
                        new EPDMMediaTypeRange_3.EPDM_MEDIATYPERANGE_3[_struct.iCount];

                    for (int i = 0; i < _struct.iCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpData.ToInt64() + (_struct.iSize * i));
                        arr[i] = (EPDMMediaTypeRange_3.EPDM_MEDIATYPERANGE_3)
                            Marshal.PtrToStructure(current, typeof(EPDMMediaTypeRange_3.EPDM_MEDIATYPERANGE_3));
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

        public EPDMMediaTypeRange_4.EPDM_MEDIATYPERANGE_4[] MEDIATYPERANGE_4
        {
            get
            {
                try
                {
                    if (_struct.lpData == IntPtr.Zero)
                    {
                        return null;
                    }

                    EPDMMediaTypeRange_4.EPDM_MEDIATYPERANGE_4[] arr =
                        new EPDMMediaTypeRange_4.EPDM_MEDIATYPERANGE_4[_struct.iCount];

                    for (int i = 0; i < _struct.iCount; i++)
                    {
                        IntPtr current = new IntPtr(_struct.lpData.ToInt64() + (_struct.iSize * i));
                        arr[i] = (EPDMMediaTypeRange_4.EPDM_MEDIATYPERANGE_4)
                            Marshal.PtrToStructure(current, typeof(EPDMMediaTypeRange_4.EPDM_MEDIATYPERANGE_4));
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

        #endregion

        #region Constructors/Destructors

        //-------------------------------------------------------------------
        // EPDMSelectData
        // Comments		Constructor
        //
        // Modify History
        //-------------------------------------------------------------------
        //
        public EPDMSelectData()
        {
            _ptrToStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf( _struct ));

            _struct.iCount = 0;
            _struct.iSize = 0;
            _struct.lpData = IntPtr.Zero;
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
            _struct = (EPDM_SELECTDATA)Marshal.PtrToStructure(_ptrToStruct, typeof(EPDM_SELECTDATA));
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
                Marshal.FreeCoTaskMem(_struct.lpData);

                _disposed = true;
            }
            base.Dispose(disposing);
        }

        public void Alloc()
        {
            try
            {
                // iIDCount、iSize = 0 is EPDM_ERR_FAIL.
                if (_struct.iCount == 0 || _struct.iSize == 0)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }

                // Free the memory.
                Marshal.FreeCoTaskMem(_struct.lpData);

                // Allocate the memory. - EPDMSelectData.lpData
                _struct.lpData = Marshal.AllocCoTaskMem(_struct.iCount * _struct.iSize);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        private Int64 GetSelectData(IntPtr p)
        {
            try
            {
                Int64 data = 0;
                switch (_struct.iSize)
                {
                    case 8:
                        data = (Int64)Marshal.PtrToStructure(p, typeof(Int64));
                        break;
                    case 4:
                        data = (Int32)Marshal.PtrToStructure(p, typeof(Int32));
                        break;
                    case 2:
                    default:
                        data = (Int16)Marshal.PtrToStructure(p, typeof(Int16));
                        break;
                }
                return data;
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