using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.epson.label.driver
{

    public enum ENSErrorCode
    {
        ERR_BASE = 0,
        ERR_PARAMETER = ERR_BASE - 1,       //Values of necessary arguments have not been set.
        ERR_INITIALIZE = ERR_BASE - 2,      //Failure in initialization of bi-directional communication module.
                                            //Or API has not been initialized.
        ERR_NOTSUPPORT = ERR_BASE - 3,      //Error before the start of communication (i.e. a port specification error etc.).
                                            //Or other error occurred in bi-directional communication module.
        ERR_PRINTER = ERR_BASE - 4,         //Printer has not been registered
        ERR_NOTFOUND = ERR_BASE - 5,        //Communication cannot be opened. 
                                            //Communication trouble. Or there is no device information which can be acquired. 
        ERR_BUFFERSIZE = ERR_BASE - 6,      //Specified buffer size value is too small.
        ERR_TEMPORARY = ERR_BASE - 7,       //Temporary storage memory used in API cannot be secured.
        ERR_COMMUNICATION = ERR_BASE - 8,   //Communication error occurred inside system of bi-directional communication module.
        ERR_INVALIDDATA = ERR_BASE - 9,     //Data acquired by API contains invalid code, so data is not reliable.
        ERR_CHANNEL = ERR_BASE - 10,        //No usable communication channel for packet transmission/reception. 
        ERR_HANDLE = ERR_BASE - 11,         //Handle of specified bi-directional communication module is invalid.
        ERR_BUSY = ERR_BASE - 12,           //Port could not be opened while printer is printing (communicating).
        ERR_LOADDLL = ERR_BASE - 13,        //Failure in loading bi-directional communication module.
        ERR_DEVICEID = ERR_BASE - 14,       //Specified DeviceID information is invalid.
        ERR_PRNHANDLE = ERR_BASE - 15,      //Specified printer handle is invalid.
        ERR_PORT = ERR_BASE - 16,           //Unsupported printer path name was specified.
        ERR_TIMEOUT = ERR_BASE - 17,        //Receive processing stopped due to a time out.
        ERR_JOB1 = ERR_BASE - 18,           //Job management error No. 1.
        ERR_JOB2 = ERR_BASE - 19,           //Job management error No. 2.
        ERR_JOB3 = ERR_BASE - 20,           //Job management error No. 3.
        ERR_INVALIDHANDLE = ERR_BASE - 21,  //Specified discovery handle is invalid.
        ERR_USERCANCEL = ERR_BASE - 22,     //Discovery was canceled by user.
        ERR_THREADSTART = ERR_BASE - 23,    //Failed in starting thread.
        ERR_TRAPRESOURCE = ERR_BASE - 24,   //Insufficient trap resource.
        ERR_SERVICE = ERR_BASE - 25,        //Core service error.
        ERR_JOBNOTSUPPORT = ERR_BASE - 26,  //Job management function is not supported.
        ERR_TRAPNOTSUPPORT = ERR_BASE - 27, //Trap function is not supported.
        ERR_SETTRAP = ERR_BASE - 28,        //Trap setting failure.
        ERR_NOTRAPDATA = ERR_BASE - 29,     //No Trap notification data.
        ERR_GSTNOTSUPPORT = ERR_BASE - 30,  //Unsupported general status.
        ERR_OTHER = ERR_BASE - 31,          //Other error
    }

    public enum ENSType                     //Printer path type
    {
        TYPE_PORT = 0,                      //Port                    
        TYPE_PRINTER = 1,                   //Printer registration name                  
        TYPE_IPX = 2,                       //IPX
    }
    
    public enum ENSPathType
    {
        TYPE_EJL = 0,                       //Printer compatible with EJL commands (Page printer)                     
        TYPE_REMOTE = 1,                    //Printer compatible with remote commands (INK/SIDM printer)                   
        TYPE_UNKNOWN = 100,                 //Type cannot be determined.          
    }

    public enum ENSOption                   //ENSStartDiscovery optional commands.
    {
        OPT_NOCOMMAND = 0,                  //No command.
        OPT_COMMUNITYNAME = 5,              //Community name.
    }

    //-------------------------------------
    // Job management function definitions
    //-------------------------------------
                                            ///////////////////////////////////////
                                            // Search condition flag
                                            ///////////////////////////////////////
    public enum ENSSRCHCOND
    {
        ALL  		= 0x00000000,	        // None
		JOBSETIDX	= 0x00000001,	        // JobSetIndex
		JOBIDX  	= 0x00000002,	        // JobIndex
		HOSTNAME  	= 0x00000004,	        // Host name
		USERNAME  	= 0x00000008,	        // User name
		SUBMISSION = 0x00000010,	        // Submission ID
    }
    //-------------------------------------
    // Job information definitions
    //-------------------------------------
    public enum ENSPAPERINFO_
    {
        MAX = 16,                            //Maximum value of paper size/type information
    }

    public enum ENSTIMESTR
    {
        SIZE = 20,                          // Size of area for time information character string
    }

    public enum ENSREQINFO
    {
        SIZE = 2,                           // Job information flag to be acquired
    }

    										///////////////////////////////////////
											// Job status at end of printing
											///////////////////////////////////////
    public enum ENSPRINTEND
    {
        COMPLETED = 0,		                // Normal end of printing
        CANCELED = 1,		                // Printing terminated by cancel
        ABORTED = 2,		                // Printing terminated by abort
    }

                                            ///////////////////////////////////////
                                            // Window message
                                            /////////////////////////////////////// 
    public enum ENSWM
    {
        WM_APP = 0x8000,
        WM_ENSMSG = WM_APP + 250,           // EpsonNetSDK sent message basic values
        WM_GETJOBINFO = WM_ENSMSG + 1,      // Job information acquisition  notification
        WM_JOBSTATUSNOTICE = WM_ENSMSG + 2, // Job status change notification
        WM_PRINTERALERTNOTICE = WM_ENSMSG + 3,// Printer alert notification
    }

    //-------------------------------------
    // Job status related definitions
    //-------------------------------------
                                            ///////////////////////////////////////
                                            // Job status
                                            ///////////////////////////////////////
    public enum ENS_JOB_STAT
    {
        PENDING = 0x00000001,               // Waiting to print
        HELD = 0x00000002,                  // Held status
        HELD_PAUSE = 0x00000004,            // Held status(JobPause)
        HELD_PRIVATE = 0x00000008,          // Held status(Confidential printing)
        PRINTING = 0x00000010,              // Printing
        PROCESSING = 0x00000020,            // Processing
        STOP_ERR = 0x00000040,              // Job stopped due to error
        STOP_INTERRUPTING = 0x0000008,      // Job stopping due to interrupt
        STOP_INTERRUPTED = 0x00000100,      // Job stopped due to interrupt
        CANCELING = 0x00000200,             // Canceling
        CANCELED = 0x00000400,              // Canceled
        ABORTING = 0x00000800,              // Aborting
        ABORTED = 0x00001000,               // Aborted
        COMPLETED = 0x00002000,             // Printed(Normal termination)
        COMPLETED_WAR = 0x00004000,         // Printed(Warning occurred)
        COMPLETED_ERR = 0x00008000,         // Printed(Error occurred)
        RETAINED = 0x00010000,              // Printed(Retained status)
    }

                                            ///////////////////////////////////////
                                            // Job control type
                                            ///////////////////////////////////////
    public enum ENS_JOB_CTRL
    {
        CANCEL = 0x01,                  	// Cancel
        RELEASE = 0x04,             		// Cancel held status
        CREATE = 0x08,                      // Create copy of job in held status
        REPRINT = 0x10,                     // Reprint retained status job
        HOLD = 0x20,                        // Set job to held status
        PAUSE = 0x40,                       // Job suspension
        RESUME = 0x80,                      // Job resumption
        INTERRUPTE = 0x0100,                // Job suspension (Interrupt printing)
    }

    //-------------------------------------
    // Definitions relating to notification APIs
    //-------------------------------------
                                            ///////////////////////////////////////
                                            // Notification setting, notification type
                                            ///////////////////////////////////////
    public enum ENSNtcType
    {
        NTCTYPE_JOBSTATUS = 0x0,            // Job status change notification
        NTCTYPE_PRINTERALERT = 0x1,         // Printer alert notification
    }

                                            ///////////////////////////////////////
                                            // Notification setting, Command
                                            ///////////////////////////////////////
    public enum ENSNtcCommand
    {
        NTCCMD_START = 0x0,                 // Notification start setting
        NTCCMD_END = 0x1,                   // Notification end setting
    }

                                            ///////////////////////////////////////
                                            // Notification condition, Printer 
                                            // alert notification
                                            ///////////////////////////////////////
    public enum ENSArertLevel
    {
        ALERTLV_ERROR = 0x1,                // Error (Alert where printing stops)
        ALERTLV_WARNING = 0x2,              // Warnings
    }

                                            ///////////////////////////////////////
                                            // Notification setting, notification method
                                            ///////////////////////////////////////
    public enum ENSNtcMethod
    {
        NTCMETHOD_WINMSG = 0x0,             // Notification by window message
    }

                                            ///////////////////////////////////////
                                            // Printer alert notification detailed code
                                            ///////////////////////////////////////
    public enum ENSArertCode
    {
        ALCD_OTHER = 1,
        ALCD_UNKNOWN = 2,
        ALCD_COVEROPEN = 3,
        ALCD_COVERCLOSED = 4,
        ALCD_INTERLOCKOPEN = 5,
        ALCD_INTERLOCKCLOSED = 6,
        ALCD_CONFIGURATIONCHANGE = 7,
        ALCD_JAM = 8,
        ALCD_DOOROPEN = 501,
        ALCD_DOORCLOSED = 502,
        ALCD_POWERUP = 503,
        ALCD_POWERDOWN = 504,
        ALCD_INPUTMEDIATRAYMISSING = 801,
        ALCD_INPUTMEDIASIZECHANGE = 802,
        ALCD_INPUTMEDIAWEIGHTCHANGE = 803,
        ALCD_INPUTMEDIATYPECHANGE = 804,
        ALCD_INPUTMEDIACOLORCHANGE = 805,
        ALCD_INPUTFORMPARTSCHANGE = 806,
        ALCD_INPUTMEDIASUPPLYLOW = 807,
        ALCD_INPUTMEDIASUPPYEMPTY = 808,
        ALCD_OUTPUTMEDIATRAYMISSING = 901,
        ALCD_OUTPUTMEDIATRAYALMOSTFULL = 902,
        ALCD_OUTPUTMEDIATRAYFULL = 903,
        ALCD_MARKERFUSERUNDERTEMPERATURE = 1001,
        ALCD_MARKERFUSEROVERTEMPERATURE = 1002,
        ALCD_MARKERTONERENPTY = 1101,
        ALCD_MARKERINKEMPTY = 1102,
        ALCD_MARKERPRINTRIBBONEMPTY = 1103,
        ALCD_MARKERTONERALMOSTEMPTY = 1104,
        ALCD_MARKERINKALMOSTEMPTY = 1105,
        ALCD_MARKERPRINTRIBBONALMOSTEMPTY = 1106,
        ALCD_MARKERWASTETONERRECEPTACLEALMOSTFULL = 1107,
        ALCD_MARKERWASTEINKRECEPTACLEALMOSTFULL = 1108,
        ALCD_MARKERWASTETONERRECEPTACLEFULL = 1109,
        ALCD_MARKERWASTEINKRECEPTACLEFULL = 1110,
        ALCD_MARKEROPCLIFEALMOSTOVER = 1111,
        ALCD_MARKEROPCLIFEOVER = 1112,
        ALCD_MARKERDEVELOPERALMOSTEMPTY = 1113,
        ALCD_MARKERDEVELOPEREMPTY = 1114,
        ALCD_MEDIAPATHMEDIATRAYMISSING = 1301,
        ALCD_MEDIAPATHMEDIATRAYALMOSTFULL = 1302,
        ALCD_MEDIAPATHMEDIATRAYFULL = 1303,
        ALCD_INTERPRETERMEMORYINCREASE = 1501,
        ALCD_INTERPRETERMEMORYDECREASE = 1502,
        ALCD_INTERPRETERCARTRIDGEADDED = 1503,
        ALCD_INTERPRETERCARTRIDGEDELETED = 1504,
        ALCD_INTERPRETERRESOURCEADDED = 1505,
        ALCD_INTERPRETERRESOURCEDELETED = 1506,
        ALCD_INTERPRETERRESOURCEUNAVAILABLE = 1507,      
    }

    //------------------------------------
    //Definitions for Job State for TM-C3400 
    //------------------------------------
    public enum ENSJobState
    {
        JOBSTATE_PRINTING = 1,
        JOBSTATE_COMPLETED = 2,
        JOBSTATE_CANCELED = 3,
        JOBSTATE_UNKNOWN = 99,
    }

    //------------------------------------
    //Definitions Printer StateCode for TM-C3400/TM-C3500/TM-C7500 (printer type).
    //Please add StatusCode which are not being defined.
    //Some value that is not being defined in enum by specification change of SDK may be returned. 
    //Refer SDK specifications for the latest definition.
    //------------------------------------
    public enum StatusCode
    {
        ST_Error = 0x00,
        ST_TestPrint = 0x01,
        ST_Busy = 0x02,
        ST_Wait = 0x03,
        ST_Idle = 0x04,
        ST_NotPrint = 0x05,
        ST_Inkdrying = 0x06,
        ST_Cleaning = 0x07,
    }

    //------------------------------------
    //Definitions Printer ErrorCode for TM-C3400/TM-C3500/TM-C7500 (printer type).
    //Please add ErrorCode which are not being defined.
    //Some value that is not being defined in enum by specification change of SDK may be returned. 
    //Refer SDK specifications for the latest definition.
    //------------------------------------
    public enum ErrorCode
    {
        ERR_FatalError = 0x00,
        ERR_CoverOpen = 0x02,
        ERR_PaperJam = 0x04,
        ERR_Inkout = 0x05,
        ERR_Paperout = 0x06,
    }

    //------------------------------------
    //Definitions Printer WarningCode for TM-C3400/TM-C3500/TM-C7500 (printer type).
    //Please add WarningCode which are not being defined.
    //Some value that is not being defined in enum by specification change of SDK may be returned. 
    //Refer SDK specifications for the latest definition.
    //------------------------------------
    public enum WarningCode
    {
        WAR_InkLow_B = 0x010,
        WAR_InkLow_C = 0x011,
        WAR_InkLow_M = 0x012,
        WAR_InkLow_Y = 0x013,
    }
}
