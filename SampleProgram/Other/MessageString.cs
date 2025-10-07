using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.epson.label.driver;

namespace SampleProgram
{
    class MessageString
    {
        #region Constants

        public const int STATE_ENS_ERROR = 1;
        public const int STATE_MEDIA_LAYOUT_ERROR = 2;
        public const int STATE_MEDIA_POSITION_ERROR = 3;
        public const int STATE_PRINT_SETTING_ERROR = 4; 
        public const int STATE_EPDI_DLL_NOT_FOUND = 5;
        public const int STATE_ENS_DLL_NOT_FOUND = 6;
        public const int STATE_DRIVER_NOT_FOUND = 7;
        public const int STATE_SYSTEM_PARAM_ERROR = 8;
        public const int STATE_DRAWIMAGE_ERROR = 9;

        public const String STR_ENS_ERROR = "Can't get the printer status.\n\nEpsonNet SDK error code : ";
        public const String STR_MEDIA_LAYOUT_ERROR = "Fail to add the media layout.\r\n\r\nEPDI error code : ";
        public const String STR_MEDIA_POSITION_ERROR = "Fail to change the media position detection setting.\r\n\r\nEPDI error code : ";
        public const String STR_PRINT_SETTING_ERROR = "Fail to change the print settings.\r\n\r\nEPDI error code : ";
        public const String STR_STATUS_BUSY = "Printer is busy.\r\nStop the printing...";
        public const String STR_STATUS_PRINTING = "Printer is printing now.\r\nStop the printing...";
        public const String STR_STATUS_CLEANING = "Printer is cleaning the print head.\r\nStop the printing...";
        public const String STR_STATUS_OTHER = "Printer is busy.\r\nStop the printing...";
        public const String STR_ERROR_PAPERJAM = "Paper jam error.\r\nStop the printing...";
        public const String STR_ERROR_PAPEROUT = "Paper out error.\r\nStop the printing...";
        public const String STR_ERROR_INKEND = "Ink cartridge error.\r\nStop the printing...";
        public const String STR_ERROR_OTHER = "Other error.\r\nStop the printing...";
        public const String STR_WARNING_INKLOW = "Ink is low.\r\n";
        public const String STR_WARNING_OTHER = "Other warning.\r\n";
        public const String STR_EPDI_DLL_NOT_FOUND = "doesn't support this function.\r\nOr check if program architecture(x86/x64) and OS architecture are correct.";
        public const String STR_ENS_DLL_NOT_FOUND = "EpsonNet SDK isn't installed in this PC.\r\nPerfoming the printing without printer status check.";
        public const String STR_DRIVER_NOT_FOUND = "Epson printer doesn't exist. \r\n\r\nPlease install the printer driver before starting the sample program.";
        public const String STR_SYSTEM_PARAM_ERROR = "Specified value is out of range.";
        public const String STR_DRAWIMAGE_ERROR = "Failed to print image.";
 
        #endregion

        #region Methods

        public static bool GetSDKError(int i, int state)
        {
            bool err = true;
            String strMessage = "";
            String strSDKErrorCode;

            try
            {
                if (i != (int)ENSErrorCode.ERR_BASE)
                {
                    switch (state)
                    {
                        case (STATE_ENS_ERROR):
                        default:
                            {
                                strMessage = STR_ENS_ERROR;
                                err = false;
                                break;
                            }
                    }
                    if (strMessage.Length > 0)
                    {
                        strSDKErrorCode = i.ToString();
                        MessageBox.Show((strMessage) + strSDKErrorCode, "", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {
                // Error handling.
            }
            return err;
        }

        public static bool GetEPDMError(int i, int state)
        {
            bool err = true;
            String strMessage = "";
            String strEPDMErrorCode;

            try
            {
                if (i != (int)EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    switch(state)
                    {
                        case (STATE_MEDIA_LAYOUT_ERROR):
                            {
                                strMessage = STR_MEDIA_LAYOUT_ERROR;
                                err = false;
                                break;
                            }
                        case (STATE_MEDIA_POSITION_ERROR):
                            {
                                strMessage = STR_MEDIA_POSITION_ERROR;
                                err = false;
                                break;
                            }
                        case (STATE_PRINT_SETTING_ERROR):
                            {
                                strMessage = STR_PRINT_SETTING_ERROR;
                                err = false;
                                break;
                            }
                        default:
                            {
                                strMessage = "";
                                err = false;
                                break;
                            }
                     }
                    if (strMessage.Length > 0)
                    {
                        strEPDMErrorCode = i.ToString();
                        MessageBox.Show((strMessage) + strEPDMErrorCode, "", MessageBoxButtons.OK);  
                    }
                }
            }
            catch (Exception)
            {
                // Error handling.
            }
            return err;
        }

        public static bool GetPrinterStatusError(StatusCode sc, ErrorCode ec, WARNING_INFO wc)
        {
            bool err = true;
            String strMessage = "";

            try
            {
                switch (sc)
                {
                    case (StatusCode.ST_Busy):
                        {
                            strMessage = STR_STATUS_BUSY;
                            err = false;
                            break;
                        }
                    case (StatusCode.ST_Wait):
                        {
                            strMessage = STR_STATUS_PRINTING;
                            err = false;
                            break;
                        }
                    case (StatusCode.ST_Cleaning):
                        {
                            strMessage = STR_STATUS_CLEANING;
                            err = false;
                            break;
                        }
                    case ((int)StatusCode.ST_Error):
                        {
                            switch (ec)
                            {
                                case (ErrorCode.ERR_PaperJam):
                                    {
                                        strMessage = STR_ERROR_PAPERJAM;
                                        err = false;
                                        break;
                                    }
                                case (ErrorCode.ERR_Paperout):
                                    {
                                        strMessage = STR_ERROR_PAPEROUT;
                                        err = false;
                                        break;
                                    }
                                case (ErrorCode.ERR_Inkout):
                                    {
                                        strMessage = STR_ERROR_INKEND;
                                        err = false;
                                        break;
                                    }
                                default:
                                    {
                                        strMessage = STR_ERROR_OTHER;
                                        err = false;
                                        break;
                                    }
                            }
                            break;
                        }
                    case (StatusCode.ST_Idle):
                        {
                            if (wc.InkLowWarning == true)
                            {
                                strMessage += STR_WARNING_INKLOW;
                                err = true;
                            }
                            if (wc.OtherWarning == true)
                            {
                                strMessage += STR_WARNING_OTHER;
                                err = true;
                            }
                            break;
                        }
                    default:
                        {
                            strMessage = STR_STATUS_OTHER;
                            err = false;
                            break;
                        }
                }
                if (strMessage.Length > 0)
                {
                    MessageBox.Show((strMessage), "", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                // Error handling.
            }
            return err;
        }

        public static bool GetSystemError(int state, String devName)
        {
            bool err = true;
            String strMessage = "";

            if ( devName == null)
            {
                devName = "";
            }
            try
            {
                switch (state)
                {
                    case (STATE_EPDI_DLL_NOT_FOUND):
                        {
                            strMessage = String.Format("{0} {1}",devName, STR_EPDI_DLL_NOT_FOUND);
                            err = false;
                            break;
                        }
                    default:
                        {
                            strMessage = "";
                            err = false;
                            break;
                        }
                }
                if (strMessage.Length > 0)
                {
                    MessageBox.Show((strMessage), "", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                // Error handling.
            }
            return err;
        }

        public static bool GetSystemError(int state)
        {
            bool err = true;
            String strMessage = "";

            try
            {
                switch (state)
                {
                    //case (STATE_ENS_DLL_NOT_FOUND):
                    //    {
                    //        strMessage = STR_ENS_DLL_NOT_FOUND;
                    //        err = false;
                    //        break;
                    //    }
                    case (STATE_DRIVER_NOT_FOUND):
                        {
                            strMessage = STR_DRIVER_NOT_FOUND;
                            err = false;
                            break;
                        }
                    case (STATE_SYSTEM_PARAM_ERROR):
                        {
                            strMessage = STR_SYSTEM_PARAM_ERROR;
                            err = false;
                            break;
                        }
                    case (STATE_DRAWIMAGE_ERROR):
                        {
                            strMessage = STR_DRAWIMAGE_ERROR;
                            err = false;
                            break;
                        }
                    default:
                        {
                            strMessage = "";
                            err = false;
                            break;
                        }
                }
                if (strMessage.Length > 0)
                {
                    MessageBox.Show((strMessage), "", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                // Error handling.
            }
            return err;
        }

        #endregion
    }
}

