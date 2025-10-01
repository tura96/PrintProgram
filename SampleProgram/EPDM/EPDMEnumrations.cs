using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.epson.label.driver
{
    public enum EPDMErrorCode
    {
        EPDM_ERR_NORMAL			= 0,		// The function succeeds.
        EPDM_ERR_CMD			= -1,		// The command is not supported.
        EPDM_ERR_PARAM			= -2,		// The parameter error
        EPDM_ERR_NOTSUPPORT		= -3,		// The specified Printer Driver is not supported.
        EPDM_ERR_DLLNOTFOUND	= -4,		// Not found the printer specific DLL
        EPDM_ERR_NOMEM			= -5,		// Shortage of memory.
        EPDM_ERR_OPENPRT		= -6,		// Open fails
        EPDM_ERR_FAIL			= -7,		// Any error is occurred.
        EPDM_ERR_VERSION		= -8,		// Not Supprot this DLL version.
        EPDM_ERR_NOTVALID		= -9,		// The function can not reply valid value.
        EPDM_ERR_COMMUNICATION	= -10,		// A communication error.
        EPDM_ERR_FATAL			= -11,		// Fatal error.
        EPDM_ERR_NOTFOUND		= -12,		// Not found the input data.
        EPDM_ERR_FILEFORMAT		= -13,		// Import file format error.
        EPDM_ERR_READFILE		= -14,		// The export file is not read.
        EPDM_ERR_WRITEFILE		= -15,		// The export file is not write.
        EPDM_ERR_OVERWRITE		= -16,		// The export file name is exist(Do not overwrite).
        EPDM_ERR_LONGERNAME		= -17,		// The Add Name is longer.
        EPDM_ERR_MAXDATA		= -18,		// The Add Data is max.
        EPDM_ERR_FWVERSION		= -19		// The F/W version is old.    
    }

    public enum OpenType
    {
        EPDM_OPENTYPE_CUR		= 0,		// Open with the printer driver's current default DEVMODE data structure.
        EPDM_OPENTYPE_ORG		= 1,		// Open with the DEVMODE data structure in factory reset.
        EPDM_OPENTYPE_IN		= 2,		// Open with the DEVMODE data structure specified by the lpDM parameter. 
    }

    public enum CommandLevel
    {
        EPDM_LEVEL_1			= 1,		// The command can be set unconditionally. 
        EPDM_LEVEL_2			= 2,		// The command can be set with the range of value returned from a range query.
        EPDM_LEVEL_3			= 3,		// The command may or may not be set depending on the return value from a range query.
        EPDM_LEVEL_4			= 4,		// The command is for special purpose.
    }

    public enum Command
    {
        EPDM_CMD_MEDIA						= 60,		// Media type
        EPDM_CMD_MEDIATYPE					= 61,		// MeidaTypeRange_3 
        EPDM_CMD_STRING						= 213,		// String.
        EPDM_CMD_FAVORITE					= 500,		// My favorite :Comment=STR
        EPDM_CMD_FAVORITEW					= 501,		// My favorite :Comment=WSTR
        EPDM_CMD_MEDIALAYOUT				= 502,		// Media layout :Comment=STR
        EPDM_CMD_MEDIALAYOUTW				= 503,		// Media layout :Comment=WSTR
        EPDM_CMD_QUALITY					= 504,		// Print qulity
        EPDM_CMD_AUTOCUT					= 505,		// Media cut mode
        EPDM_CMD_AUTOCUTNUM					= 506,		// Count of media cut
        EPDM_CMD_MEDIAPOSITION				= 507,		// Search media position
        EPDM_CMD_DRIVERSETTINGS				= 600,		// Driver settings :Comment=STR
        EPDM_CMD_DRIVERSETTINGSW			= 601,		// Driver settings :Comment=WSTR
        EPDM_CMD_PRINTERSETTINGS			= 602,		// Printer settings :Comment=STR
        EPDM_CMD_PRINTERSETTINGSW			= 603,		// Printer settings :Comment=WSTR
        EPDM_CMD_STRINGW					= 1024,	    // UNICODE String.
    }

    public enum StructVersion
    {
        EPDM_STVER_STRING_1					= 1,		// Structure Version define for [EPDM_STRING_1]
        EPDM_STVER_STRINGW_1				= 1,		// Structure Version define for [EPDM_STRINGW_1]
        EPDM_STVER_FAVORITEINF				= 1,		// Structure Version define for [EPDM_FAVORITEINF]
        EPDM_STVER_FAVORITEINFW				= 1,		// Structure Version define for [EPDM_FAVORITEINFW]
        EPDM_STVER_FAVORITERANGE			= 1,		// Structure Version define for [EPDM_FAVORITERANGE]
        EPDM_STVER_MEDIALAYOUTINF			= 1,		// Structure Version define for [EPDM_MEDIALAYOUTINF]
        EPDM_STVER_MEDIALAYOUTINF_2			= 2,		// Structure Version define for [EPDM_MEDIALAYOUTINF_2]
        EPDM_STVER_MEDIALAYOUTINF_3			= 3,		// Structure Version define for [EPDM_MEDIALAYOUTINF_3]
        EPDM_STVER_MEDIALAYOUTINF_4			= 4,		// Structure Version define for [EPDM_MEDIALAYOUTINF_4]
        EPDM_STVER_MEDIATYPERANGE			= 1,		// Structure Version define for [EPDM_MEDIATYPERANGE]
        EPDM_STVER_MEDIATYPERANGE_2			= 2,		// Structure Version define for [EPDM_MEDIATYPERANGE_2]
        EPDM_STVER_MEDIATYPERANGE_3			= 3,		// Structure Version define for [EPDM_MEDIATYPERANGE_3]
        EPDM_STVER_MEDIATYPERANGE_4			= 4,		// Structure Version define for [EPDM_MEDIATYPERANGE_4]
        EPDM_STVER_MEDIALAYOUTRANGE			= 1,		// Structure Version define for [EPDM_MEDIALAYOUTRANGE]
        EPDM_STVER_MEDIALAYOUTRANGE_2		= 2,		// Structure Version define for [EPDM_MEDIALAYOUTRANGE_2]
        EPDM_STVER_MEDIALAYOUTRANGE_3		= 3,		// Structure Version define for [EPDM_MEDIALAYOUTRANGE_3]
        EPDM_STVER_MEDIALAYOUTRANGE_4		= 4,		// Structure Version define for [EPDM_MEDIALAYOUTRANGE_4]
        EPDM_STVER_MEDIALAYOUTADD			= 1,		// Structure Version define for [EPDM_MEDIALAYOUTADD]
        EPDM_STVER_MEDIALAYOUTADD_2			= 2,		// Structure Version define for [EPDM_MEDIALAYOUTADD_2]
        EPDM_STVER_MEDIALAYOUTADD_3			= 3,		// Structure Version define for [EPDM_MEDIALAYOUTADD_3]
        EPDM_STVER_MEDIALAYOUTADD_4			= 4,		// Structure Version define for [EPDM_MEDIALAYOUTADD_4]
        EPDM_STVER_MEDIALAYOUTADDW			= 1,		// Structure Version define for [EPDM_MEDIALAYOUTADDW]
        EPDM_STVER_MEDIALAYOUTADDW_2		= 2,		// Structure Version define for [EPDM_MEDIALAYOUTADDW_2]
        EPDM_STVER_MEDIALAYOUTADDW_3		= 3,		// Structure Version define for [EPDM_MEDIALAYOUTADDW_3]
        EPDM_STVER_MEDIALAYOUTADDW_4		= 4,		// Structure Version define for [EPDM_MEDIALAYOUTADDW_4]
        EPDM_STVER_PRINTERSETTINGS			= 1,		// Structure Version define for [EPDM_PRINTERSETTINGS]
        EPDM_STVER_EXPORTDATA				= 1,		// Structure Version define for [EPDM_EXPORTDATA]
        EPDM_STVER_EXPORTDATAW				= 1,		// Structure Version define for [EPDM_EXPORTDATAW]
        EPDM_STVER_FAVORITEADD				= 1,		// Structure Version define for [EPDM_FAVORITEADD]
        EPDM_STVER_FAVORITEADDW				= 1,		// Structure Version define for [EPDM_FAVORITEADDW]
        EPDM_STVER_EXPORT_DRIVERSETTINGS	= 1,		// Structure Version define for [EPDM_EXPORT_DRIVERSETTINGS]
        EPDM_STVER_EXPORT_DRIVERSETTINGSW	= 1,		// Structure Version define for [EPDM_EXPORT_DRIVERSETTINGSW]
        EPDM_STVER_EXPORT_PRINTERSETTINGS	= 1,		// Structure Version define for [EPDM_EXPORT_PRINTERSETTINGS]
        EPDM_STVER_EXPORT_PRINTERSETTINGSW	= 1,		// Structure Version define for [EPDM_EXPORT_PRINTERSETTINGSW]
    }

    public enum MediaType
    {
        EPDM_MEDIA_PLAIN_MEDIA				= 2000,	// Plain Media
        EPDM_MEDIA_PLAIN_MEDIA_LABEL		= 2001,	// Plain Media Label
        EPDM_MEDIA_SYNTHETIC_MEDIA_LABEL	= 2007,// Synthetic Media Label
        EPDM_MEDIA_FINE_MEDIA				= 2002,	// Fine Media
        EPDM_MEDIA_FINE_MEDIA_LABEL			= 2003,	// Fine Media Label
        EPDM_MEDIA_PET_FILM					= 2009,	// PET Film
        EPDM_MEDIA_SYNTHETIC_MEDIA			= 2008,	// Synthetic Media
        EPDM_MEDIA_WRIST_BAND				= 2010,	// Wrist Band
        EPDM_MEDIA_WRIST_BAND_TYPE_2		= 2011,	// Wrist Band Type 2
        EPDM_MEDIA_GLOSSY_LABEL				= 2012,	// Glossy Media Label
        EPDM_MEDIA_GLOSSYFILM				= 2013,	// Glossy film
        EPDM_MEDIA_HIGH_GLOSSY				= 2014,	// High Glossy Paper
    }

    public enum StringID
    {
        EPDM_STRING_MEDIA					= 3,		// Media Name.
        EPDM_STRING_FAVORITE				= 15,		// Favorite Name
        EPDM_STRING_MEDIALAYOUT				= 16,		// Media Layout Name
        EPDM_STRING_MEDIATYPE				= 17,		// Media Type Name
        EPDM_STRING_AUTOCUT					= 18,		// Autocut Action Type Name
        EPDM_STRING_MEDIAPOSITION			= 19,		// Media Position Kind Name
    }

    public enum MediaTypeID
    {
        EPDM_MEDIATYPE_DIECUT				= 0,		// Die-cut Label(Gap)
        EPDM_MEDIATYPE_BMDIECUT				= 1,		// Die-cut Label(BM)
        EPDM_MEDIATYPE_BMDIECUTGAP			= 2,		// Black Mark Die-cut Label(Gap)
        EPDM_MEDIATYPE_ALLLABEL				= 3,		// Full-page Label
        EPDM_MEDIATYPE_BMALLLABEL			= 4,		// Black Mark Full-page Label
        EPDM_MEDIATYPE_RECEIPT				= 5,		// Continuous Paper
        EPDM_MEDIATYPE_BMRECEIPT			= 6,		// Black Mark Continuous Paper
        EPDM_MEDIATYPE_TPDIECUT				= 7,		// Transparent Die-cut Label
        EPDM_MEDIATYPE_TPALLLABEL			= 8,		// Transparent Full-page Label
    }

    public enum MediaLayoutID
    {
        EPDM_LAYOUT_1080X1748_DIECUT_LABEL		= 2050,	// 108 x 174.8 mm - Die-cut Label
        EPDM_LAYOUT_1050X740_RECEIPT			= 2051,	// 105 x 74    mm - Receipt
        EPDM_LAYOUT_860X540_RECEIPT				= 2052,	// 86  x 54    mm - Receipt
        EPDM_LAYOUT_830X2970_FULLPAGE_LABEL		= 2053,	// 83  x 297   mm - Full-page Label
        EPDM_LAYOUT_800X1000_DIECUT_LABEL		= 2054,	// 80  x 100   mm - Die-cut Label
        EPDM_LAYOUT_500X350_DIECUT_LABEL		= 2055,	// 50  x 35    mm - Die-cut Label
        EPDM_LAYOUT_500X320_DIECUT_LABEL		= 2056,	// 50  x 32    mm - Die-cut Label
        EPDM_LAYOUT_360X2916_BLACK_MARK_RECEIPT	= 2058,	// 36  x 291.6 mm - Black Mark Receipt
        EPDM_LAYOUT_360X1836_BLACK_MARK_RECEIPT	= 2059,	// 36  x 183.6 mm - Black Mark Receipt
        EPDM_LAYOUT_300X3000_RECEIPT			= 2057,	// 30  x 300   mm - Receipt
        EPDM_LAYOUT_300X2880_BLACK_MARK_RECEIPT	= 2060,	// 30  x 288   mm - Black Mark Receipt
        EPDM_LAYOUT_1080X1524_DIECUT_LABEL		= 2061,	// 108 x 152.4 mm - Die-cut Label
    }

    public enum PrintQualityID
    {
        EPDM_QUALITY_360X180				= 0,		// Quality level (360dpi * 180dpi)
        EPDM_QUALITY_360X360				= 1,		// Quality level (360dpi * 360dpi)
        EPDM_QUALITY_720X360				= 2,		// Quality level (720dpi * 360dpi)
        EPDM_QUALITY_HIGH_SPEED             = 1,		// High speed
        EPDM_QUALITY_SPEED                  = 2,		// Speed
        EPDM_QUALITY_NORMAL                 = 3,		// Normal
        EPDM_QUALITY_QUALITY                = 4,		// Quality
        EPDM_QUALITY_MAX_QUALITY            = 5,		// Max quality
    }

    public enum MediaPosionID
    {
        EPDM_MEDIAPOSITION_NO_DETECTION						= 0, // No Detection
        EPDM_MEDIAPOSITION_BLACKMARKS_ON_DIECUT_LABELS		= 1, // Detect Blackmarks on Die-cut Labels
        EPDM_MEDIAPOSITION_BLACKMARKS_ON_CONTINUOUS_MEDIA	= 2, // Detect Blackmarks on Continuous Media
        EPDM_MEDIAPOSITION_MARGINS_BETWEEN_LABELS			= 3, // Detect Margins Between Labels
        EPDM_MEDIAPOSITION_BLACKMARKS						= 4, // Detect Blackmarks
    }

    public enum OverWriteID
    {
        EPDM_OVERWRITE_OFF					= 0,		// Disable over write
        EPDM_OVERWRITE_ON					= 1,		// Enable over write
    }

    public enum AutocutID
    {
        EPDM_AUTOCUT_ON_AFTER_EVERY_PAGE					= 0x0001,
        EPDM_AUTOCUT_ON_ONLY_AFTER_LAST_PAGE				= 0x0002,
        EPDM_AUTOCUT_ON_AFTER_SPECIFIED_NUMBER_OF_PAGES		= 0x0004,
        EPDM_AUTOCUT_ON_LASTPAGE_OF_COLLATEPAGE			    = 0x0008,
        EPDM_AUTOCUT_OFF_FEED_TO_PEEL_OFF_POSITION			= 0x0100,
        EPDM_AUTOCUT_OFF_FEED_TO_CUT_POSITION				= 0x0200,
        EPDM_AUTOCUT_OFF_STOP_AT_THE_PRINT_END_POSITION		= 0x0400,
        EPDM_PEEL_REWIND                                    = 0x1000,
        EPDM_PEEL_MANUAL_APPLY                              = 0x2000,
        EPDM_PEEL_AUTO_APPLY                                = 0x4000,
    }

    public enum AutocutNum
    {
        EPDM_AUTOCUTNUMBER_DEFAULT				= -32768,
    }

    public enum BiDiPrinting
    {
        EPDM_BIDIPRINTING_DISABLE				= 0,
        EPDM_BIDIPRINTING_ENABLE				= 1,
        EPDM_BIDIPRINTING_DEFAULT				= 0xFFFF,
    }


    public enum BandingReduction
    {
        EPDM_BANDINGREDUCTION_DISABLE			= 0,
        EPDM_BANDINGREDUCTION_ENABLE			= 1,
        EPDM_BANDINGREDUCTION_DEFAULT			= 0xFFFF,
    }

    public enum MediaSavingID
    {
        EPDM_MEDIASAVING_DISABLE				= 0,
        EPDM_MEDIASAVING_LOWERMARGIN			= 1,
        EPDM_MEDIASAVING_LOWERUPPERMARGIN		= 2,
    }

    public enum ColorManagement
    {
        EPDM_COLORMANAGEMENT_DISABLE			= 0,
        EPDM_COLORMANAGEMENT_ENABLE				= 1,
        EPDM_COLORMANAGEMENT_DEFAULT			= 0xFFFF,
    }

    public enum ColorMode
    {
        EPDM_COLOR_MODE_NATURAL					= 0,
        EPDM_COLOR_MODE_EPSONCOLOR				= 1,
        EPDM_COLOR_MODE_DEFAULT					= 0xFFFF,
    }

    public enum Gamma
    {
        EPDM_GAMMA_18					= 18,
        EPDM_GAMMA_22					= 22,
        EPDM_GAMMA_DEFAULT				= -1,
    }

    public enum InkProfileAndBrightness
    {
        EPDM_INKPROFILEANDBRIGHTNESS_DISABLE	= 0,
        EPDM_INKPROFILEANDBRIGHTNESS_ENABLE		= 1,
        EPDM_INKPROFILEANDBRIGHTNESS_DEFAULT	= 0xFFFF,
    }

    public enum InkProfileEnable
    {
        EPDM_INKPROFILEENABLE_DISABLE   = 0,
        EPDM_INKPROFILEENABLE_ENABLE    = 1,
        EPDM_INKPROFILEENABLE_DEFAULT   = 0xFFFF,
    }

    public enum InkProfile
    {
        EPDM_INKPROFILE_LEVEL_DEFAULT   = 0		
    }

    public enum BlackRatioEnable
    {
        EPDM_BLACKRATIOENABLE_DISABLE   = 0,
        EPDM_BLACKRATIOENABLE_ENABLE    = 1,
        EPDM_BLACKRATIOENABLE_DEFAULT   = 0xFFFF,
    }

    public enum BlackRatio
    {
        EPDM_INKPROFILE_BLACK_LEVEL_DEFAULT = 0
    }
}
