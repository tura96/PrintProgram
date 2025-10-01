using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.epson.label.driver;

namespace SampleProgram
{
    /// <summary>
    /// This is the class of PrintSettingsDlg.
    /// </summary>
    public partial class PrintSettingsDlg : Form
    {
        #region Fields

        private List<MEDIA_LAYOUT> _mediaLayoutList;
        private List<MEDIA_TYPE> _mediaTypeList;
        private List<PRINT_QUALITY> _printQualityList;
        private String _devName;
        private String _portName;
        private bool _isEPDMOpen = true;

        #endregion

        #region Constructors/Destructors

        /// <summary>
        /// This is the constructor of PrintSettingsDlg.
        /// </summary>
        /// <param name="devName">Device name of the selected printer.</param>
        /// <param name="portName">Port name of the selected printer.</param>
        public PrintSettingsDlg(String devName, String portName)
        {
            InitializeComponent();

            _devName = devName;
            _portName = portName;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// This is the event handler that load PrintSettingsDlg.
        /// </summary>
        private void PrintSettings_Load(object sender, EventArgs e)
        {
            try
            {
                EPDMControl.GetInstance().Open(_devName, _portName);
                UpdateMediaLayoutComboBox();
                UpdateMediaTypeComboBox();
                UpdatePrintQualityComboBox();
            }
            catch (EPDMException ex)
            {
                _isEPDMOpen = false;
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_PRINT_SETTING_ERROR);
                EPDMClose();
            }
            catch (DllNotFoundException)
            {
                _isEPDMOpen = false;
                MessageString.GetSystemError((int)MessageString.STATE_EPDI_DLL_NOT_FOUND, _devName);
                EPDMClose();
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
            }
        }

        /// <summary>
        /// This is the event handler that implement updating print quality list on MediaTypeComboBox change action.
        /// </summary>
        private void MediaTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EPDMControl obj = EPDMControl.GetInstance();
                int index = MediaTypeComboBox.SelectedIndex;

                obj.SetMediaType(_mediaTypeList[index].MediaTypeID);

                _printQualityList = obj.GetPrintQualityList(out index);
                if (_printQualityList == null)
                {
                    return;
                }

                PrintQualityComboBox.Items.Clear();

                foreach (PRINT_QUALITY printQuality in _printQualityList)
                {
                    PrintQualityComboBox.Items.Add(printQuality.StrPrintQuality);
                    this.Controls.Add(PrintQualityComboBox);
                }

                PrintQualityComboBox.SelectedIndex = index;
            }
            catch (EPDMException ex)
            {
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_PRINT_SETTING_ERROR);
                EPDMClose();
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
            }
        }

        /// <summary>
        /// This is the event handler that implement ApplyButton action.
        /// Set media layout, media type and print quality to selected printer.
        /// </summary>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            EPDMControl obj = EPDMControl.GetInstance();
            try
            {
                int index = 0;

                index = MediaLayoutComboBox.SelectedIndex;
                obj.SetMediaLayout(_mediaLayoutList[index].MediaLayoutID);

                index = MediaTypeComboBox.SelectedIndex;
                obj.SetMediaType(_mediaTypeList[index].MediaTypeID);

                index = PrintQualityComboBox.SelectedIndex;
                if (index >= 0)
                {
                    obj.SetPrintQuality(_printQualityList[index].PrintQualityID);
                }
                else
                {
                    PrintQualityComboBox.Enabled = false;
                }
                obj.UpdateDevMode();
            }
            catch (EPDMException ex)
            {
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_PRINT_SETTING_ERROR);
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
            }
            finally
            {
                EPDMClose();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is the method that update the list of media layout combobox.
        /// </summary>
        private void UpdateMediaLayoutComboBox()
        {
            try
            {
                int index = 0;

                _mediaLayoutList = EPDMControl.GetInstance().GetMediaLayoutList(out index);
                if (_mediaLayoutList == null)
                {
                    return;
                }

                MediaLayoutComboBox.Items.Clear();

                foreach (MEDIA_LAYOUT mediaLayout in _mediaLayoutList)
                {
                    MediaLayoutComboBox.Items.Add(mediaLayout.StrMediaLayout);
                    this.Controls.Add(MediaLayoutComboBox);
                }

                MediaLayoutComboBox.SelectedIndex = index;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that update the list of media type combobox.
        /// </summary>
        private void UpdateMediaTypeComboBox()
        {
            try
            {
                int index = 0;

                _mediaTypeList = EPDMControl.GetInstance().GetMediaTypeList(out index);
                if (_mediaTypeList == null)
                {
                    return;
                }

                MediaTypeComboBox.Items.Clear();

                foreach (MEDIA_TYPE mediaType in _mediaTypeList)
                {
                    MediaTypeComboBox.Items.Add(mediaType.StrMediaType);
                    this.Controls.Add(MediaTypeComboBox);
                }

                MediaTypeComboBox.SelectedIndex = index;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that update the list of print quality combobox.
        /// </summary>
        private void UpdatePrintQualityComboBox()
        {
            try
            {
                int index = 0;

                _printQualityList = EPDMControl.GetInstance().GetPrintQualityList(out index);
                if (_printQualityList == null)
                {
                    PrintQualityComboBox.Enabled = false;
                    return;
                }

                PrintQualityComboBox.Items.Clear();

                foreach (PRINT_QUALITY printQuality in _printQualityList)
                {
                    PrintQualityComboBox.Items.Add(printQuality.StrPrintQuality);
                    this.Controls.Add(PrintQualityComboBox);
                }

                PrintQualityComboBox.SelectedIndex = index;
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        /// <summary>
        /// This is the method that close PrintSettingsDlg. 
        /// </summary>
        private void EPDMClose()
        {
            if (_isEPDMOpen == true)
            {
                try
                {
                    EPDMControl.GetInstance().Close();
                }
                catch (EPDMException ex)
                {
                    MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_PRINT_SETTING_ERROR);
                }
                catch (Exception)
                {
                    // Error handling.
                    this.Close();
                    this.Dispose();
                    Environment.Exit(0);
                }
            }                
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// This is the method that close application.
        /// </summary>
        private void ExceptionClose()
        {
            EPDMClose();
            Environment.Exit(0);
        }

        #endregion
    }
}
