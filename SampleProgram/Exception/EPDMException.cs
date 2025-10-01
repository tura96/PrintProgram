using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.epson.label.driver
{
    [Serializable]
    class EPDMException : Exception
    {
        #region Property

        public EPDMErrorCode ErrCode { get; set; }

        #endregion

        #region Methods

        public EPDMException(EPDMErrorCode e)
        {
            ErrCode = e;
        }

        #endregion
    }
}
