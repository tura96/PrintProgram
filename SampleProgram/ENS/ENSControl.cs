using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    /// <summary>
    /// This is the class that defines the flag of warning code.
    /// </summary>
    public class WARNING_INFO
    {
        public bool InkLowWarning = false;
        public bool OtherWarning = false;
    }

    /// <summary>
    /// This is the class that controls of EpsonNetSDK modules.
    /// </summary>
    class ENSControl
    {
        #region Fields

        private static ENSControl _singleton = new ENSControl();
        private String _devName, _portName;
        private IntPtr _ptrHandle;

        #endregion

        #region Constants

        private const String INFO_COMMAND = "STATUS";

        #endregion

        #region Constructors/Destructors

        private ENSControl()
        {
        }

        #endregion

        #region Methods

        public static ENSControl GetInstance()
        {
            return _singleton;
        }

        /// <summary>
        /// This is the method that implement ENSInitialize.
        /// </summary>
        /// <param name="devName">Device name of the selected printer.</param>
        /// <param name="portName">Port name of the selected printer.</param>
        public void Initialize(String devName, String portName)
        {
            try
            {
                _devName = devName;
                _portName = portName;

                ENSErrorCode errCode = (ENSErrorCode)ENSWrapper.ENSInitialize();
                if (errCode != ENSErrorCode.ERR_BASE)
                {
                    throw new ENSException(errCode);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the staus of selected printer.
        /// </summary>
        /// <param name="structINKSIDM">INKSIDMSTATUS_02 structure object.(out)</param>
        public void GetPrinterStatus(out ENSStatus.INKSIDMSTATUS_02 structINKSIDM)
        {
            IntPtr ptrInt = IntPtr.Zero;
            IntPtr ptrTemp = IntPtr.Zero;
            IntPtr ptrGetBuff = IntPtr.Zero;
            try
            {
                // Handling of to call ENSGetDeviceID.
                int buffLen = 0;

                // ENSGetDeviceID 1st call. Get the required buffLen.
                ENSErrorCode errCode = (ENSErrorCode)ENSWrapper.ENSGetDeviceID((int)ENSType.TYPE_PRINTER, _devName, IntPtr.Zero, out buffLen, 0x1);
                if (errCode != ENSErrorCode.ERR_BUFFERSIZE)
                {
                    throw new ENSException(errCode);
                }

                ptrTemp = Marshal.AllocCoTaskMem(buffLen);

                // ENSGetDeviceID 2nd call.
                errCode = (ENSErrorCode)ENSWrapper.ENSGetDeviceID((int)ENSType.TYPE_PRINTER, _devName, ptrTemp, out buffLen, 0x1);
                if (errCode != ENSErrorCode.ERR_BASE)
                {
                    throw new ENSException(errCode);
                }

                // Handling of to call ENSOpenCommunication.
                errCode = (ENSErrorCode)ENSWrapper.ENSOpenCommunication((int)ENSType.TYPE_PRINTER, _devName, ptrTemp, out _ptrHandle);
                if (errCode != ENSErrorCode.ERR_BASE)
                {
                    throw new ENSException(errCode);
                }

                // Get printer status information.
                structINKSIDM = new ENSStatus.INKSIDMSTATUS_02();
                String str = "STRUCTURE=INKSIDMSTATUS_02";
                ptrInt = Marshal.StringToHGlobalAnsi(str);


                // Handling of to call ENSGetInformation.
                buffLen = 0;

                // ENSGetInformation 1st call. Get the required buffLen.
                errCode = (ENSErrorCode)ENSWrapper.ENSGetInformation(_ptrHandle, INFO_COMMAND, ptrInt, IntPtr.Zero, out buffLen);
                if (errCode != ENSErrorCode.ERR_BUFFERSIZE)
                {
                    throw new ENSException(errCode);
                }

                ptrGetBuff = Marshal.AllocCoTaskMem(buffLen);

                // ENSGetInformation 2nd call.
                errCode = (ENSErrorCode)ENSWrapper.ENSGetInformation(_ptrHandle, INFO_COMMAND, ptrInt, ptrGetBuff, out buffLen);
                if (errCode != ENSErrorCode.ERR_BASE)
                {
                    throw new ENSException(errCode);
                }

                // Get INKSIDMSTATUS_02 structure object. 
                structINKSIDM = (ENSStatus.INKSIDMSTATUS_02)Marshal.PtrToStructure(ptrGetBuff, typeof(ENSStatus.INKSIDMSTATUS_02));

                // Handling of to call ENSCloseCommunication.
                errCode = (ENSErrorCode)ENSWrapper.ENSCloseCommunication(_ptrHandle);
                if (errCode != ENSErrorCode.ERR_BASE)
                {
                    throw new ENSException(errCode);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrInt);
                Marshal.FreeCoTaskMem(ptrTemp);
                Marshal.FreeCoTaskMem(ptrGetBuff);
            }
        }

        /// <summary>
        /// This is the method that get the StatusCode.
        /// </summary>
        /// <returns>StatusCode information.</returns>
        public StatusCode GetStatusInformation()
        {
            try
            {
                ENSStatus.INKSIDMSTATUS_02 structINKSIDM = new ENSStatus.INKSIDMSTATUS_02();
                GetPrinterStatus(out structINKSIDM);
                StatusCode sc = (StatusCode)structINKSIDM.StatusCode;
                return sc;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the ErrorCode.
        /// </summary>
        /// <returns>ErrorCode information.</returns>
        public ErrorCode GetErrorInformation()
        {
            try
            {
                ENSStatus.INKSIDMSTATUS_02 structINKSIDM = new ENSStatus.INKSIDMSTATUS_02();
                GetPrinterStatus(out structINKSIDM);
                ErrorCode ec = (ErrorCode)structINKSIDM.ErrorCode;
                return ec;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the WarningCode.
        /// Judge whether inklow warning and other warning occur.
        /// </summary>
        /// <returns>WARNING_INFO class object</returns>
        public WARNING_INFO GetWarningInformation()
        {
            try
            {
                ENSStatus.INKSIDMSTATUS_02 structINKSIDM = new ENSStatus.INKSIDMSTATUS_02();
                GetPrinterStatus(out structINKSIDM);

                int awc = structINKSIDM.AvailableWarningCode2;
                WARNING_INFO warningInfo = new WARNING_INFO();
 
                if (awc != 0)
                {
                    for (int i = 0; i < awc; i++)
                    {
                        if ((structINKSIDM.WarningCode2[i] == (int)WarningCode.WAR_InkLow_B) ||
                            (structINKSIDM.WarningCode2[i] == (int)WarningCode.WAR_InkLow_C) ||
                            (structINKSIDM.WarningCode2[i] == (int)WarningCode.WAR_InkLow_M) ||
                            (structINKSIDM.WarningCode2[i] == (int)WarningCode.WAR_InkLow_Y))
                        {
                            warningInfo.InkLowWarning = true;
                        }
                        else
                        {
                            warningInfo.OtherWarning = true;
                        }
                    }
                }
                return warningInfo;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement ENSRelease.
        /// </summary>
        public void Release()
        {
            try
            {
                ENSErrorCode errCode = (ENSErrorCode)ENSWrapper.ENSRelease();
                if (errCode != ENSErrorCode.ERR_BASE)
                {
                    throw new ENSException(errCode);
                }
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
