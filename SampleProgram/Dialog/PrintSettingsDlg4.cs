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
    public partial class PrintSettingsDlg4 : Form
    {
        #region Fields

        private List<MEDIA_LAYOUT> _mediaLayoutList;
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
        public PrintSettingsDlg4(String devName, String portName)
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

                _mediaLayoutList = EPDMControl.GetInstance().GetMediaLayoutList_4(out index);
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
