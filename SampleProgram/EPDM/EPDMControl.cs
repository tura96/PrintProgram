using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace com.epson.label.driver
{
    /// <summary>
    /// This is the class that defines the media position information.
    /// </summary>
    public class MEDIA_POSITION
    {
        public String StrMediaPosition;
        public int MediaPositionID;
    }

    /// <summary>
    /// This is the class that defines the media layout information.
    /// </summary>
    public class MEDIA_LAYOUT
    {
        public String StrMediaLayout;
        public int MediaLayoutID;
    }

    /// <summary>
    /// This is the class that defines the media type information.
    /// </summary>
    public class MEDIA_TYPE
    {
        public String StrMediaType;
        public int MediaTypeID;
    }

    /// <summary>
    /// This is the class that defines the print quality information.
    /// </summary>
    public class PRINT_QUALITY
    {
        public String StrPrintQuality;
        public int PrintQualityID;
    }

    /// <summary>
    /// This is the class that defines value of media layout range.
    /// </summary>
    public class MEDIA_LAYOUT_RANGE
    {
        public short MinPaperWid;
        public short MaxPaperWid;
        public short MinLabelWid;
        public short MaxLabelWid;
        public short MinLabelHig;
        public short MaxLabelHig;
        public short MinLabelGap;
        public short MaxLabelGap;
    }

    /// <summary>
    /// This is the class that controls of EPDI modules.
    /// </summary>
    public class EPDMControl
    {
        #region Fields

        private static EPDMControl _singleton = new EPDMControl();
        private IntPtr _ptrGetParm = IntPtr.Zero;

        #endregion

        #region Constants

        private const String STR_PRINTQUALITY_360X180 = "360x180dpi";
        private const String STR_PRINTQUALITY_360X360 = "360x360dpi";
        private const String STR_PRINTQUALITY_720X360 = "720x360dpi";

        #endregion

        #region Constructor / Destructor

        private EPDMControl()
        {

        }

        #endregion

        #region Methods

        public static EPDMControl GetInstance()
        {
            return _singleton;
        }

        /// <summary>
        /// This is the method that implement EPDM_Open.
        /// </summary>
        /// <param name="devName">Device name of the selected printer.</param>
        /// <param name="portName">Port name of the selected printer.</param>
        public void Open(String devName, String portName)
        {
            try
            {
                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_Open(devName, portName, 0, IntPtr.Zero, out _ptrGetParm);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }
            }
            catch(Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the minimum value and the maximum value of MediaLayoutRange. 
        /// </summary>
        /// <returns>The class containing the getting value.</returns>
        public MEDIA_LAYOUT_RANGE GetMediaLayoutRangeValue()
        {
            try
            {
                using (EPDMMediaLayoutRange epdmMediaLayoutRange = GetMediaLayoutRange())
                {
                    // Get the MediaLayoutRange value.
                    int index = epdmMediaLayoutRange.GetMediaTypeIndex(MediaTypeID.EPDM_MEDIATYPE_DIECUT);

                    MEDIA_LAYOUT_RANGE mediaLayoutRange = new MEDIA_LAYOUT_RANGE();
                    mediaLayoutRange.MinPaperWid = epdmMediaLayoutRange.MediaTypeRange[index].iMinPaperWid;
                    mediaLayoutRange.MaxPaperWid = epdmMediaLayoutRange.MediaTypeRange[index].iMaxPaperWid;

                    mediaLayoutRange.MinLabelWid = epdmMediaLayoutRange.MediaTypeRange[index].iMinLabelWid;
                    mediaLayoutRange.MaxLabelWid = epdmMediaLayoutRange.MediaTypeRange[index].iMaxLabelWid;

                    mediaLayoutRange.MinLabelHig = epdmMediaLayoutRange.MediaTypeRange[index].iMinLabelHig;
                    mediaLayoutRange.MaxLabelHig = epdmMediaLayoutRange.MediaTypeRange[index].iMaxLabelHig;

                    mediaLayoutRange.MinLabelGap = epdmMediaLayoutRange.MediaTypeRange[index].iMinLabelGap;
                    mediaLayoutRange.MaxLabelGap = epdmMediaLayoutRange.MediaTypeRange[index].iMaxLabelGap;

                    return mediaLayoutRange;
                }
            }
            catch(Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the minimum value and the maximum value of MediaLayoutRange. 
        /// </summary>
        /// <returns>The class containing the getting value.</returns>
        public MEDIA_LAYOUT_RANGE GetMediaLayoutRangeValue_3()
        {
            try
            {
                using (EPDMSelectData epdmSelectData = GetSelectDataRange(Command.EPDM_CMD_MEDIATYPE))
                {

                    // Get the MediaTypeRange value.
                    EPDMMediaTypeRange_3.EPDM_MEDIATYPERANGE_3[] MEDIATYPERANGE_3 = epdmSelectData.MEDIATYPERANGE_3;
                    MEDIA_LAYOUT_RANGE mediaLayoutRange = new MEDIA_LAYOUT_RANGE();
                    for (int i = 0; i < epdmSelectData.Count; i++)
                    {
                        if ( epdmSelectData.MEDIATYPERANGE_3[i].iTypeID == (int)MediaTypeID.EPDM_MEDIATYPE_DIECUT)
                        {
                            mediaLayoutRange.MinLabelWid = (short)epdmSelectData.MEDIATYPERANGE_3[i].iMinLabelWid;
                            mediaLayoutRange.MaxLabelWid = (short)epdmSelectData.MEDIATYPERANGE_3[i].iMaxLabelWid;

                            mediaLayoutRange.MinLabelHig = (short)epdmSelectData.MEDIATYPERANGE_3[i].iMinLabelHig;
                            mediaLayoutRange.MaxLabelHig = (short)epdmSelectData.MEDIATYPERANGE_3[i].iMaxLabelHig;
                            break;
                        }
                    }

                    return mediaLayoutRange;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the minimum value and the maximum value of MediaLayoutRange. 
        /// </summary>
        /// <returns>The class containing the getting value.</returns>
        public MEDIA_LAYOUT_RANGE GetMediaLayoutRangeValue_4()
        {
            try
            {
                using (EPDMSelectData epdmSelectData = GetSelectDataRange(Command.EPDM_CMD_MEDIATYPE))
                {

                    // Get the MediaTypeRange value.
                    EPDMMediaTypeRange_4.EPDM_MEDIATYPERANGE_4[] MEDIATYPERANGE_4 = epdmSelectData.MEDIATYPERANGE_4;
                    MEDIA_LAYOUT_RANGE mediaLayoutRange = new MEDIA_LAYOUT_RANGE();
                    for (int i = 0; i < epdmSelectData.Count; i++)
                    {
                        if (epdmSelectData.MEDIATYPERANGE_4[i].iTypeID == (int)MediaTypeID.EPDM_MEDIATYPE_DIECUT)
                        {
                            mediaLayoutRange.MinLabelWid = (short)epdmSelectData.MEDIATYPERANGE_4[i].iMinLabelWid;
                            mediaLayoutRange.MaxLabelWid = (short)epdmSelectData.MEDIATYPERANGE_4[i].iMaxLabelWid;

                            mediaLayoutRange.MinLabelHig = (short)epdmSelectData.MEDIATYPERANGE_4[i].iMinLabelHig;
                            mediaLayoutRange.MaxLabelHig = (short)epdmSelectData.MEDIATYPERANGE_4[i].iMaxLabelHig;
                            break;
                        }
                    }

                    return mediaLayoutRange;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that add the media layout.
        /// </summary>
        /// <param name="name">The inputted media layout name.</param>
        /// <param name="mediaWid">The inputted media width value.</param>
        /// <param name="labelWid">The inputted label width value.</param>
        /// <param name="labelLen">The inputted label length value.</param>
        /// <param name="labelGap">The inputted labels gap value.</param>
        public void AddMediaLayout(String name, short mediaWid, short labelWid, short labelLen, short labelGap)
        {
            try
            {
                using(EPDMMediaLayoutAdd epdmMeiaLayoutAdd = new EPDMMediaLayoutAdd())
                using (EPDMMediaLayoutInf epdmMediaLayoutInf = new EPDMMediaLayoutInf())
                {
                    // Set the parameter to structure.
                    epdmMediaLayoutInf.MediaTypeID = (int)MediaTypeID.EPDM_MEDIATYPE_DIECUT;
                    epdmMediaLayoutInf.PaperWid = mediaWid;
                    epdmMediaLayoutInf.LabelWid = labelWid;
                    epdmMediaLayoutInf.LabelHig = labelLen;
                    epdmMediaLayoutInf.LabelGap = labelGap;

                    epdmMeiaLayoutAdd.Name = name;
                    epdmMeiaLayoutAdd.Overwrite = (uint)OverWriteID.EPDM_OVERWRITE_ON;
                    epdmMeiaLayoutAdd.MediaLayoutInf = (EPDMMediaLayoutInf.EPDM_MEDIALAYOUTINF)Marshal.PtrToStructure(epdmMediaLayoutInf.StructurePointer, typeof(EPDMMediaLayoutInf.EPDM_MEDIALAYOUTINF));

                    // EPDM_AddData call.
                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_AddData(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMeiaLayoutAdd.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }

                    epdmMeiaLayoutAdd.PtrToStructure();
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that add the media layout.
        /// </summary>
        /// <param name="name">The inputted media layout name.</param>
        /// <param name="labelWid">The inputted label width value.</param>
        /// <param name="labelLen">The inputted label length value.</param>
        public void AddMediaLayout_3(String name, short labelWid, short labelLen)
        {
            try
            {
                using (EPDMMediaLayoutAdd_3 epdmMeiaLayoutAdd_3 = new EPDMMediaLayoutAdd_3())
                using (EPDMMediaLayoutInf_3 epdmMediaLayoutInf_3 = new EPDMMediaLayoutInf_3())
                {
                    // Set the parameter to structure.
                    epdmMediaLayoutInf_3.MediaTypeID = (int)MediaTypeID.EPDM_MEDIATYPE_DIECUT;
                    epdmMediaLayoutInf_3.LabelWid = labelWid;
                    epdmMediaLayoutInf_3.LabelHig = labelLen;

                    epdmMediaLayoutInf_3.MediaID = (ushort)MediaType.EPDM_MEDIA_FINE_MEDIA_LABEL;
                    epdmMediaLayoutInf_3.QualityID = (ushort)PrintQualityID.EPDM_QUALITY_360X360;
                    epdmMediaLayoutInf_3.AutoCutID = (ushort)AutocutID.EPDM_AUTOCUT_ON_AFTER_EVERY_PAGE;
                    epdmMediaLayoutInf_3.AutoCutNum = (short)AutocutNum.EPDM_AUTOCUTNUMBER_DEFAULT;
                    epdmMediaLayoutInf_3.MediaSavingID = (ushort)MediaSavingID.EPDM_MEDIASAVING_DISABLE;
                    epdmMediaLayoutInf_3.BiDiPrinting = (ushort)BiDiPrinting.EPDM_BIDIPRINTING_DEFAULT;
                    epdmMediaLayoutInf_3.BandingReduction = (ushort)BandingReduction.EPDM_BANDINGREDUCTION_DEFAULT;
                    epdmMediaLayoutInf_3.ColorManagement = (ushort)ColorManagement.EPDM_COLORMANAGEMENT_DEFAULT;
                    epdmMediaLayoutInf_3.ColorMode = (ushort)ColorMode.EPDM_COLOR_MODE_DEFAULT;
                    epdmMediaLayoutInf_3.Gamma = (short)Gamma.EPDM_GAMMA_DEFAULT;
                    epdmMediaLayoutInf_3.InkProfileAndBrightness = 
                        (ushort)InkProfileAndBrightness.EPDM_INKPROFILEANDBRIGHTNESS_DEFAULT;

                    epdmMeiaLayoutAdd_3.Name = name;
                    epdmMeiaLayoutAdd_3.Overwrite = (uint)OverWriteID.EPDM_OVERWRITE_ON;
                    epdmMeiaLayoutAdd_3.MediaLayoutInf_3 = (EPDMMediaLayoutInf_3.EPDM_MEDIALAYOUTINF_3)Marshal.PtrToStructure(epdmMediaLayoutInf_3.StructurePointer, typeof(EPDMMediaLayoutInf_3.EPDM_MEDIALAYOUTINF_3));

                    // EPDM_AddData call.
                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_AddData(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMeiaLayoutAdd_3.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }

                    epdmMeiaLayoutAdd_3.PtrToStructure();
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that add the media layout.
        /// </summary>
        /// <param name="name">The inputted media layout name.</param>
        /// <param name="labelWid">The inputted label width value.</param>
        /// <param name="labelLen">The inputted label length value.</param>
        public void AddMediaLayout_4(String name, short labelWid, short labelLen)
        {
            try
            {
                using (EPDMMediaLayoutAdd_4 epdmMeiaLayoutAdd_4 = new EPDMMediaLayoutAdd_4())
                using (EPDMMediaLayoutInf_4 epdmMediaLayoutInf_4 = new EPDMMediaLayoutInf_4())
                {
                    // Set the parameter to structure.
                    epdmMediaLayoutInf_4.MediaLayoutID = 0;
                    epdmMediaLayoutInf_4.LabelWid = labelWid;
                    epdmMediaLayoutInf_4.LabelHig = labelLen;
                    epdmMediaLayoutInf_4.LabelInterval = 30;    /* In the example, fixed to 3mm */

                    epdmMediaLayoutInf_4.MediaTypeID = (ushort)MediaTypeID.EPDM_MEDIATYPE_DIECUT;
                    epdmMediaLayoutInf_4.MediaSavingID = (ushort)MediaSavingID.EPDM_MEDIASAVING_DISABLE;
                    epdmMediaLayoutInf_4.MediaID = (ushort)MediaType.EPDM_MEDIA_FINE_MEDIA_LABEL;
                    epdmMediaLayoutInf_4.QualityID = (ushort)PrintQualityID.EPDM_QUALITY_NORMAL;
                    epdmMediaLayoutInf_4.AutoCutID = (ushort)AutocutID.EPDM_AUTOCUT_ON_AFTER_EVERY_PAGE;    /* AutoCut Model Only */
                    epdmMediaLayoutInf_4.AutoCutNum = (short)AutocutNum.EPDM_AUTOCUTNUMBER_DEFAULT;
                    epdmMediaLayoutInf_4.InkProfileEnable = (ushort)InkProfileEnable.EPDM_INKPROFILEENABLE_DEFAULT;
                    epdmMediaLayoutInf_4.InkProfile = (short)InkProfile.EPDM_INKPROFILE_LEVEL_DEFAULT;
                    epdmMediaLayoutInf_4.BlackRatioEnable = (ushort)BlackRatioEnable.EPDM_BLACKRATIOENABLE_DEFAULT;
                    epdmMediaLayoutInf_4.BlackRatio = (short)BlackRatio.EPDM_INKPROFILE_BLACK_LEVEL_DEFAULT;
                    epdmMediaLayoutInf_4.BiDiPrinting = (ushort)BiDiPrinting.EPDM_BIDIPRINTING_DEFAULT;

                    epdmMeiaLayoutAdd_4.Name = name;
                    epdmMeiaLayoutAdd_4.Overwrite = (uint)OverWriteID.EPDM_OVERWRITE_ON;
                    epdmMeiaLayoutAdd_4.MediaLayoutInf_4 = (EPDMMediaLayoutInf_4.EPDM_MEDIALAYOUTINF_4)Marshal.PtrToStructure(epdmMediaLayoutInf_4.StructurePointer, typeof(EPDMMediaLayoutInf_4.EPDM_MEDIALAYOUTINF_4));

                    // EPDM_AddData call.
                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_AddData(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMeiaLayoutAdd_4.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }

                    epdmMeiaLayoutAdd_4.PtrToStructure();
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that create the list of media position combobox.
        /// </summary>
        /// <param name="index">Number of chosen combobox sequence.(out)</param>
        /// <returns>The list of media position.</returns>
        public List<MEDIA_POSITION> GetMediaPositionList(out int index)
        {
            try
            {
                List<MEDIA_POSITION> mediaPositionList = new List<MEDIA_POSITION>();
                MEDIA_POSITION mediaPosition;

                // GetRange EPDM_CMD_MEDIAPOSITION
                using (EPDMSelectData epdmSelectData = GetMediaPositionRange())
                {
                    // GetData EPDM_CMD_MEDIAPOSITION & Get combo box index.
                    index = Array.IndexOf(epdmSelectData.Data, GetMediaPositionID());

                    // GetData EPDM_CMD_STRING
                    for (int i = 0; i < epdmSelectData.Count; i++)
                    {
                        String strData = GetString((uint)StringID.EPDM_STRING_MEDIAPOSITION, (uint)epdmSelectData.Data[i]);

                        //　Add the mediapositionlist
                        mediaPosition = new MEDIA_POSITION();
                        mediaPosition.StrMediaPosition = strData;
                        mediaPosition.MediaPositionID = (int)epdmSelectData.Data[i];
                        mediaPositionList.Add(mediaPosition);
                    }
                    return mediaPositionList;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the media position range.
        /// </summary>
        /// <returns>EPDM_SelectData structure object of EPDM_CMD_MEDIAPOSITION.</returns>
        public EPDMSelectData GetMediaPositionRange()
        {
            try
            {
                return GetSelectDataRange(Command.EPDM_CMD_MEDIAPOSITION);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get present media position detection setting ID.
        /// </summary>
        /// <returns>Media position detection setting ID.</returns>
        public short GetMediaPositionID()
        {
            try
            {
                return GetShortData(Command.EPDM_CMD_MEDIAPOSITION);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that set the media position detection setting.
        /// </summary>
        /// <param name="mediaPositionID">Selected media position detection setting ID.</param>
        public void SetMediaPosition(int mediaPositionID)
        {
            try
            {
                SetData((int)Command.EPDM_CMD_MEDIAPOSITION, mediaPositionID);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that create the list of media layout combobox.
        /// </summary>
        /// <param name="index">Number of chosen combobox sequence.(out)</param>
        /// <returns>The list of media layout.</returns>
        public List<MEDIA_LAYOUT> GetMediaLayoutList(out int index)
        {
            try
            {
                List<MEDIA_LAYOUT> mediaLayoutList = new List<MEDIA_LAYOUT>();
                MEDIA_LAYOUT mediaLayout;

                // GetRange EPDM_CMD_MEDIALAYOUT
                using (EPDMMediaLayoutRange epdmMediaLayoutRange = GetMediaLayoutRange())
                {
                    // GetData EPDM_CMD_MEDIALAYOUT & Get combo box index.
                    index = Array.IndexOf(epdmMediaLayoutRange.MediaLayoutID, GetMediaLayoutID());

                    // GetData EPDM_CMD_STRING
                    for (int i = 0; i < epdmMediaLayoutRange.IDCount; i++)
                    {
                        String strData = GetString((uint)StringID.EPDM_STRING_MEDIALAYOUT, (uint)epdmMediaLayoutRange.MediaLayoutID[i]);

                        //　Add the mediaLayoutlist
                        mediaLayout = new MEDIA_LAYOUT();
                        mediaLayout.StrMediaLayout = strData;
                        mediaLayout.MediaLayoutID = (int)epdmMediaLayoutRange.MediaLayoutID[i];
                        mediaLayoutList.Add(mediaLayout);
                    }
                    return mediaLayoutList;
                }
            }
            catch(Exception)
            {
                // Error handling.
                throw;
            }         
        }

        /// <summary>
        /// This is the method that create the list of media layout combobox.
        /// </summary>
        /// <param name="index">Number of chosen combobox sequence.(out)</param>
        /// <returns>The list of media layout.</returns>
        public List<MEDIA_LAYOUT> GetMediaLayoutList_3(out int index)
        {
            try
            {
                List<MEDIA_LAYOUT> mediaLayoutList = new List<MEDIA_LAYOUT>();
                MEDIA_LAYOUT mediaLayout;

                // GetRange EPDM_CMD_MEDIALAYOUT
                using (EPDMMediaLayoutRange_3 epdmMediaLayoutRange_3 = GetMediaLayoutRange_3())
                {
                    // GetData EPDM_CMD_MEDIALAYOUT & Get combo box index.
                    index = Array.IndexOf(epdmMediaLayoutRange_3.MediaLayoutID, GetMediaLayoutID_3());

                    // GetData EPDM_CMD_STRING
                    for (int i = 0; i < epdmMediaLayoutRange_3.IDCount; i++)
                    {
                        String strData = GetString((uint)StringID.EPDM_STRING_MEDIALAYOUT, (uint)epdmMediaLayoutRange_3.MediaLayoutID[i]);

                        //　Add the mediaLayoutlist
                        mediaLayout = new MEDIA_LAYOUT();
                        mediaLayout.StrMediaLayout = strData;
                        mediaLayout.MediaLayoutID = (int)epdmMediaLayoutRange_3.MediaLayoutID[i];
                        mediaLayoutList.Add(mediaLayout);
                    }
                    return mediaLayoutList;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that create the list of media layout combobox.
        /// </summary>
        /// <param name="index">Number of chosen combobox sequence.(out)</param>
        /// <returns>The list of media layout.</returns>
        public List<MEDIA_LAYOUT> GetMediaLayoutList_4(out int index)
        {
            try
            {
                List<MEDIA_LAYOUT> mediaLayoutList = new List<MEDIA_LAYOUT>();
                MEDIA_LAYOUT mediaLayout;

                // GetRange EPDM_CMD_MEDIALAYOUT
                using (EPDMMediaLayoutRange_4 epdmMediaLayoutRange_4 = GetMediaLayoutRange_4())
                {
                    // GetData EPDM_CMD_MEDIALAYOUT & Get combo box index.
                    index = Array.IndexOf(epdmMediaLayoutRange_4.MediaLayoutID, GetMediaLayoutID_4());

                    // GetData EPDM_CMD_STRING
                    for (int i = 0; i < epdmMediaLayoutRange_4.IDCount; i++)
                    {
                        String strData = GetString((uint)StringID.EPDM_STRING_MEDIALAYOUT, (uint)epdmMediaLayoutRange_4.MediaLayoutID[i]);

                        //　Add the mediaLayoutlist
                        mediaLayout = new MEDIA_LAYOUT();
                        mediaLayout.StrMediaLayout = strData;
                        mediaLayout.MediaLayoutID = (int)epdmMediaLayoutRange_4.MediaLayoutID[i];
                        mediaLayoutList.Add(mediaLayout);
                    }
                    return mediaLayoutList;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get present media layout ID.
        /// </summary>
        /// <returns>Media layout ID.</returns>
        public short GetMediaLayoutID()
        {
            try
            {
                using (EPDMMediaLayoutInf epdmMeiaLayoutInf = new EPDMMediaLayoutInf())
                {
                    // GetData EPDM_CMD_mediaLayout
                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetData(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMeiaLayoutInf.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }
                    epdmMeiaLayoutInf.PtrToStructure();

                    short mediaLayoutID = (short)epdmMeiaLayoutInf.MediaLayoutID;

                    return mediaLayoutID;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get present media layout ID.
        /// </summary>
        /// <returns>Media layout ID.</returns>
        public short GetMediaLayoutID_3()
        {
            try
            {
                using (EPDMMediaLayoutInf_3 epdmMeiaLayoutInf_3 = new EPDMMediaLayoutInf_3())
                {
                    // GetData EPDM_CMD_mediaLayout
                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetData(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMeiaLayoutInf_3.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }
                    epdmMeiaLayoutInf_3.PtrToStructure();

                    short mediaLayoutID = (short)epdmMeiaLayoutInf_3.MediaLayoutID;

                    return mediaLayoutID;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get present media layout ID.
        /// </summary>
        /// <returns>Media layout ID.</returns>
        public short GetMediaLayoutID_4()
        {
            try
            {
                using (EPDMMediaLayoutInf_4 epdmMeiaLayoutInf_4 = new EPDMMediaLayoutInf_4())
                {
                    // GetData EPDM_CMD_mediaLayout
                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetData(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMeiaLayoutInf_4.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }
                    epdmMeiaLayoutInf_4.PtrToStructure();

                    short mediaLayoutID = (short)epdmMeiaLayoutInf_4.MediaLayoutID;

                    return mediaLayoutID;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that set the media layout.
        /// </summary>
        /// <param name="mediaLayoutID">Selected media layout ID.</param>
        public void SetMediaLayout(int mediaLayoutID)
        {
            try
            {
                SetData((int)Command.EPDM_CMD_MEDIALAYOUT, mediaLayoutID);
            }
            catch (Exception)
            {
                // Error handling. 
                throw;
            }
        }
        
        /// <summary>
        /// This is the method that create the list of media type combobox.
        /// </summary>
        /// <param name="index">Number of chosen combobox sequence(out)</param>
        /// <returns>The list of media type.</returns>
        public List<MEDIA_TYPE> GetMediaTypeList(out int index)
        {
            try
            {
                List<MEDIA_TYPE> mediaTypeList = new List<MEDIA_TYPE>();
                MEDIA_TYPE mediaType;

                // GetRange EPDM_CMD_MEDIA
                using (EPDMSelectData epdmSelectData = GetMediaTypeRange())
                {
                    // GetData EPDM_CMD_MEDIA
                    short mediaTypeID = GetMediaTypeID();

                    // Get combo box index.
                    index = Array.IndexOf(epdmSelectData.Data, mediaTypeID);

                    // GetData EPDM_CMD_STRING
                    for (int i = 0; i < epdmSelectData.Count; i++)
                    {
                        String strData = GetString((uint)StringID.EPDM_STRING_MEDIA, (uint)epdmSelectData.Data[i]);

                        //　Add the mediatypelist
                        mediaType = new MEDIA_TYPE();
                        mediaType.StrMediaType = strData;
                        mediaType.MediaTypeID = (int)epdmSelectData.Data[i];
                        mediaTypeList.Add(mediaType);
                    }
                    return mediaTypeList;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the media type range.
        /// </summary>
        /// <returns>EPDM_SelectData structure object of EPDM_CMD_MEDIA.</returns>
        public EPDMSelectData GetMediaTypeRange()
        {
            try
            {
                return GetSelectDataRange(Command.EPDM_CMD_MEDIA);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get present media type ID.
        /// </summary>
        /// <returns>Media type ID.</returns>
        public short GetMediaTypeID()
        {
            try
            {
                return GetShortData(Command.EPDM_CMD_MEDIA);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that set the media type.
        /// </summary>
        /// <param name="mediaTypeID">Selected media type ID.</param>
        public void SetMediaType(int mediaTypeID)
        {
            try
            {
                SetData((int)Command.EPDM_CMD_MEDIA, mediaTypeID);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that create the list of print quality combobox.
        /// </summary>
        /// <param name="index">Number of chosen combobox sequence(out) 
        /// When there is not selectable print quality, -1 returns to index.</param>
        /// <returns>The list of print quality.</returns>
        public List<PRINT_QUALITY> GetPrintQualityList(out int index)
        {
            try
            {
                List<PRINT_QUALITY> printQualityList = new List<PRINT_QUALITY>();
                PRINT_QUALITY printQuality;

                // GetData EPDM_CMD_MEDIA
                using (EPDMSelectData epdmSelectData = GetPrintQualityRange())
                {
                    if (epdmSelectData == null)
                    {
                        index = -1;
                        return null;
                    }

                    // GetData EPDM_CMD_MEDIA & Get combo box index.
                    index = Array.IndexOf(epdmSelectData.Data, GetPrintQualityID());

                    // GetPrintQualityString
                    String strData = "";
                    for (int i = 0; i < epdmSelectData.Count; i++)
                    {
                        int ID = (int)epdmSelectData.Data[i];
                        if (ID == (int)PrintQualityID.EPDM_QUALITY_360X180)
                        {
                            strData = STR_PRINTQUALITY_360X180;
                        }
                        else if (ID == (int)PrintQualityID.EPDM_QUALITY_360X360)
                        {
                            strData = STR_PRINTQUALITY_360X360;
                        }
                        else if (ID == (int)PrintQualityID.EPDM_QUALITY_720X360)
                        {
                            strData = STR_PRINTQUALITY_720X360;
                        }

                        //　Add the mediapositionlist
                        printQuality = new PRINT_QUALITY();
                        printQuality.StrPrintQuality = strData;
                        printQuality.PrintQualityID = ID;
                        printQualityList.Add(printQuality);
                    }
                    return printQualityList;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get the print quality range.
        /// </summary>
        /// <returns>EPDM_SelectData structure object of EPDM_CMD_QUALITY.</returns>
        public EPDMSelectData GetPrintQualityRange()
        {
            try
            {
                return GetSelectDataRange(Command.EPDM_CMD_QUALITY);
            }
            catch (EPDMException ex)
            {
                if (ex.ErrCode == EPDMErrorCode.EPDM_ERR_CMD)
                {
                    return null;
                }
                // Error handling.
                throw;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that get present print quality ID.
        /// </summary>
        /// <returns>Print quality ID.</returns>
        public short GetPrintQualityID()
        {
            try
            {
                return GetShortData(Command.EPDM_CMD_QUALITY);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that set the print quality.
        /// </summary>
        /// <param name="printQualityID">Selected print quality ID.</param>
        public void SetPrintQuality(int printQualityID)
        {
            try
            {
                SetData((int)Command.EPDM_CMD_QUALITY, printQualityID);
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_GetRange of EPDM_MEDIALAYOUTRANGE structure.
        /// </summary>
        /// <returns>EPDM_MEDIALAYOUTRANGE structure object.</returns>
        public EPDMMediaLayoutRange GetMediaLayoutRange()
        {
            try
            {
                EPDMMediaLayoutRange epdmMediaLayoutRange = new EPDMMediaLayoutRange();

                // EPDM_GetRange 1st call. Get the required memory size.
                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMediaLayoutRange.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmMediaLayoutRange.PtrToStructure();

                // Allocate the memory.
                epdmMediaLayoutRange.Alloc();

                // EPDM_GetRange 2nd call.
                errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMediaLayoutRange.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmMediaLayoutRange.PtrToStructure();

                return epdmMediaLayoutRange;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_GetRange of EPDM_MEDIALAYOUTRANGE_3 structure.
        /// </summary>
        /// <returns>EPDM_MEDIALAYOUTRANGE_3 structure object.</returns>
        public EPDMMediaLayoutRange_3 GetMediaLayoutRange_3()
        {
            try
            {
                EPDMMediaLayoutRange_3 epdmMediaLayoutRange_3 = new EPDMMediaLayoutRange_3();

                // EPDM_GetRange 1st call. Get the required memory size.
                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMediaLayoutRange_3.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmMediaLayoutRange_3.PtrToStructure();

                // Allocate the memory.
                epdmMediaLayoutRange_3.Alloc();

                // EPDM_GetRange 2nd call.
                errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMediaLayoutRange_3.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmMediaLayoutRange_3.PtrToStructure();

                return epdmMediaLayoutRange_3;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_GetRange of EPDM_MEDIALAYOUTRANGE_4 structure.
        /// </summary>
        /// <returns>EPDM_MEDIALAYOUTRANGE_4 structure object.</returns>
        public EPDMMediaLayoutRange_4 GetMediaLayoutRange_4()
        {
            try
            {
                EPDMMediaLayoutRange_4 epdmMediaLayoutRange_4 = new EPDMMediaLayoutRange_4();

                // EPDM_GetRange 1st call. Get the required memory size.
                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMediaLayoutRange_4.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmMediaLayoutRange_4.PtrToStructure();

                // Allocate the memory.
                epdmMediaLayoutRange_4.Alloc();

                // EPDM_GetRange 2nd call.
                errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)Command.EPDM_CMD_MEDIALAYOUT, epdmMediaLayoutRange_4.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmMediaLayoutRange_4.PtrToStructure();

                return epdmMediaLayoutRange_4;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_GetRange of EPDM_SELECTDATA structure.
        /// </summary>
        /// <param name="cmd">Command to acquire a set range.</param>
        /// <returns>EPDM_SELECTDATA structure object.</returns>
        public EPDMSelectData GetSelectDataRange(Command cmd)
        {
            try
            {
                EPDMSelectData epdmSelectData = new EPDMSelectData();

                // EPDM_GetRange 1st call. Get the required memory size.
                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)cmd, epdmSelectData.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmSelectData.PtrToStructure();

                // Allocate the memory. - EPDMSelectData.Data
                epdmSelectData.Alloc();

                // EPDM_GetRange 2nd call.
                errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetRange(_ptrGetParm, (int)cmd, epdmSelectData.StructurePointer);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                epdmSelectData.PtrToStructure();

                return epdmSelectData;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_GetData.
        /// </summary>
        /// <param name="cmd">Command to acquire a data.</param>
        /// <returns>The value acquired by EPDM_GetData.</returns>
        public short GetShortData(Command cmd)
        {
            IntPtr ptrPosition = IntPtr.Zero;
            try
            {
                ptrPosition = Marshal.AllocCoTaskMem(sizeof(short));

                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetData(_ptrGetParm, (int)cmd, ptrPosition);
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }

                short data = (short)Marshal.PtrToStructure(ptrPosition, typeof(short));

                return data;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
            finally
            {
                Marshal.FreeCoTaskMem(ptrPosition);
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_GetData of EPDM_STRING_1 structure.
        /// </summary>
        /// <param name="stringParam">Command of the string to acquire.</param>
        /// <param name="id">ID of the string to acquire.</param>
        /// <returns>String which the EPDM_STRING_1 structure acquired.</returns>
        public String GetString(uint stringParam, uint id)
        {
            try
            {
                using (EPDMString epdmString = new EPDMString())
                {
                    epdmString.Command = stringParam;
                    epdmString.ID = id;

                    EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetData(_ptrGetParm, (int)Command.EPDM_CMD_STRING, epdmString.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }

                    epdmString.PtrToStructure();

                    // Allocate the memory. - EPDMString.String                    
                    epdmString.Alloc();

                    errCode = (EPDMErrorCode)EPDMWrapper.EPDM_GetData(_ptrGetParm, (int)Command.EPDM_CMD_STRING, epdmString.StructurePointer);
                    if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                    {
                        throw new EPDMException(errCode);
                    }

                    epdmString.PtrToStructure();

                    return epdmString.String;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_SetData.
        /// </summary>
        /// <param name="cmd">Command to acquire a set data.</param>
        /// <param name="param">Data parameter.</param>
        public void SetData(int cmd, int param)
        {
            try
            {
                EPDMErrorCode errCode = (EPDMErrorCode)EPDMWrapper.EPDM_SetData(_ptrGetParm, cmd, param);
                
                if (errCode != EPDMErrorCode.EPDM_ERR_NORMAL)
                {
                    throw new EPDMException(errCode);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_UpdateDevMode.
        /// </summary>
        public void UpdateDevMode()
        {
            try
            {
                if (EPDMWrapper.EPDM_UpdateDevMode(_ptrGetParm) == false)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that implement EPDM_Close.
        /// </summary>
        public void Close()
        {
            try
            {
                if (EPDMWrapper.EPDM_Close(_ptrGetParm) == false)
                {
                    throw new EPDMException(EPDMErrorCode.EPDM_ERR_FAIL);
                }
            }
            catch(Exception)
            {
                // Error handling.
                throw;
            }
        }
        #endregion
    }
}
