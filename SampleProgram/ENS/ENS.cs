using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    public abstract class ENS : IDisposable
    {
        #region Fields

        protected IntPtr _ptrToStruct = IntPtr.Zero;
        private bool _disposed = false;

        #endregion

        #region Constructors/Destructors

        ~ENS()
        {
            Dispose(false);
        }
        
        #endregion

        #region Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.
                Marshal.FreeCoTaskMem(_ptrToStruct);

                _disposed = true;
            }
        }

        public abstract void PtrToStructure();
        public abstract void StructureToPtr();

        public IntPtr StructurePointer
        {
            get
            {
                StructureToPtr();
                return _ptrToStruct;
            }
        }

        #endregion
    }
}
