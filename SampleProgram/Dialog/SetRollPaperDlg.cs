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
    /// This is the class of SetRollPaperDlg.
    /// </summary>
    public partial class SetRollPaperDlg : Form
    {
        #region Fields

        private List<MEDIA_POSITION> _mediaPositionList;
        private String _devName;
        private String _portName;
        private bool _isEPDMOpen = true;

        #endregion

        #region Constructors/Destructors

        /// <summary>
        /// This is the constructor of SetRollPaperDlg.
        /// </summary>
        /// <param name="devName">Device name of the selected printer.</param>
        /// <param name="portName">Port name of the selected printer.</param>
        public SetRollPaperDlg(String devName, String portName)
        {
            InitializeComponent();

            _devName = devName;
            _portName = portName;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// This is the event handler that load SetRollPaperDlg. 
        /// </summary>
        private void SetRollPaperDlg_Load(object sender, EventArgs e)
        {
            try
            {
                EPDMControl.GetInstance().Open(_devName, _portName);
                UpdateMediaPositionComboBox();
            }
            catch (EPDMException ex)
            {
                _isEPDMOpen = false;
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_POSITION_ERROR);
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
        /// This is the event handler that implement ChangeButton action.
        /// Set the media position detection setting to selected printer.
        /// </summary>
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                EPDMControl obj = EPDMControl.GetInstance(); 
                int index = MediaPositionComboBox.SelectedIndex;
                obj.SetMediaPosition(_mediaPositionList[index].MediaPositionID);
                obj.UpdateDevMode();
            }
            catch (EPDMException ex)
            {
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_POSITION_ERROR);
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
        /// This is the method that update the list of media position combobox.
        /// </summary>
        private void UpdateMediaPositionComboBox()
        {
            try
            {
                int index = 0;

                _mediaPositionList = EPDMControl.GetInstance().GetMediaPositionList(out index);
                if (_mediaPositionList == null)
                {
                    return;
                }

                MediaPositionComboBox.Items.Clear();

                foreach (MEDIA_POSITION mediaPosition in _mediaPositionList)
                {
                    MediaPositionComboBox.Items.Add(mediaPosition.StrMediaPosition);
                    this.Controls.Add(MediaPositionComboBox);
                }

                MediaPositionComboBox.SelectedIndex = index;
            }
            catch (EPDMException ex)
            {
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_POSITION_ERROR);
                EPDMClose();
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
            }
        }

        /// <summary>
        /// This is the method that close SetRollPaperDlg. 
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
                    MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_POSITION_ERROR);
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
