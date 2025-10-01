using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.epson.label.driver
{
    [Serializable]
    class ENSException : Exception
    {
        #region Property

        public ENSErrorCode ErrCode { get; set; }

        #endregion

        #region Methods

        public ENSException(ENSErrorCode e)
        {
            ErrCode = e;
        }

        #endregion
    }
}
