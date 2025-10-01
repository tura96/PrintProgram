using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace SampleProgram
{
    class PRINTER_INFO
    {
        public String devName;
        public String portName;
    }

    class SelectPrinterInfo
    {
        #region Methods

        public static List<PRINTER_INFO> GetPrinterInfoList()
        {
            ManagementObjectSearcher searchObj = null;
            ManagementObjectCollection collectObj = null;
            
            //paramater initialize
            List<PRINTER_INFO> printerInfoList = new List<PRINTER_INFO>();
            PRINTER_INFO printerInfo;

            try
            {
                // get printerlist
                searchObj = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
                collectObj = searchObj.Get();

                foreach (ManagementObject mngObj in collectObj)
                {
                    printerInfo = new PRINTER_INFO();
                    // get devname
                    printerInfo.devName = mngObj["Name"].ToString();
                    // get portname
                    printerInfo.portName = mngObj["PortName"].ToString();

                    if (printerInfo.devName.Contains("EPSON") == true)
                        // add table
                        printerInfoList.Add(printerInfo);
                }                
                return printerInfoList;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
            finally
            {
                if (collectObj != null)
                {
                    collectObj.Dispose();
                }
                if (searchObj != null)
                {
                    searchObj.Dispose();
                }
            }
        }

        #endregion
    }
}

