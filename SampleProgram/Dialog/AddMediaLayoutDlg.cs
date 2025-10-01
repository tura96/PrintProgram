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
    /// This is the class of AddMediaLayoutDlg.
    /// </summary>
    public partial class AddMediaLayoutDlg : Form
    {
        #region Fields

        private String _devName;
        private String _portName;
        private bool _isEPDMOpen = true;

        #endregion

        #region Constructors/Destructors

        /// <summary>
        /// This is the constructor of AddMediaLayoutDlg.
        /// </summary>
        /// <param name="devName">Device name of the selected printer.</param>
        /// <param name="portName">Port name of the selected printer.</param>
        public AddMediaLayoutDlg(String devName, String portName)
        {
            InitializeComponent();

            _devName = devName;
            _portName = portName;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// This is the event handler that load AddMediaLayoutDlg. 
        /// </summary>
        private void AddMediaLayoutDlg_Load(object sender, EventArgs e)
        {
            try
            {
                EPDMControl.GetInstance().Open(_devName, _portName);
                UpdateMediaLayoutRangeString();
            }
            catch (EPDMException ex)
            {
                _isEPDMOpen = false;
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_LAYOUT_ERROR);
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
        /// This is the event handler that implement AddButton action.
        /// Regist the media layout to selected printer.
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (AddButton_CheckParam() == false)
            {
                return;
            }

            try
            {
                // Change the size( mm to 0.1mm)
                int t = 10;
                short mediaWidth = (short)Math.Round(double.Parse(MediaWidthTextBox.Text) * t, 0, MidpointRounding.AwayFromZero);
                short labelWidth = (short)Math.Round(double.Parse(LabelWidthTextBox.Text) * t, 0, MidpointRounding.AwayFromZero);
                short labelLength = (short)Math.Round(double.Parse(LabelLengthTextBox.Text) * t, 0, MidpointRounding.AwayFromZero);
                short labelGap = (short)Math.Round(double.Parse(LabelsGapTextBox.Text) * t, 0, MidpointRounding.AwayFromZero);
                
                EPDMControl.GetInstance().AddMediaLayout(MediaLayoutNameTextBox.Text, mediaWidth, labelWidth, labelLength, labelGap);
            }
            catch (EPDMException ex)
            {
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_LAYOUT_ERROR);
            }
            catch (FormatException)
            {                
                MessageString.GetSystemError(MessageString.STATE_SYSTEM_PARAM_ERROR);
            }
            catch (OverflowException)
            {
                MessageString.GetSystemError(MessageString.STATE_SYSTEM_PARAM_ERROR);
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
        /// This is the method that update the string of media layout range.
        /// </summary>
        private void UpdateMediaLayoutRangeString()
        {
            try
            {
                MEDIA_LAYOUT_RANGE obj = EPDMControl.GetInstance().GetMediaLayoutRangeValue();

                MediaWidthRangeLabel.Text = GetMediaLayoutRangeString(MediaLayoutRangeToString(obj.MinPaperWid), MediaLayoutRangeToString(obj.MaxPaperWid));
                LabelWidthRangeLabel.Text = GetMediaLayoutRangeString(MediaLayoutRangeToString(obj.MinLabelWid), MediaLayoutRangeToString(obj.MaxLabelWid));
                LabelLengthRangeLabel.Text = GetMediaLayoutRangeString(MediaLayoutRangeToString(obj.MinLabelHig), MediaLayoutRangeToString(obj.MaxLabelHig));
                LabelsGapRangeLabel.Text = GetMediaLayoutRangeString(MediaLayoutRangeToString(obj.MinLabelGap), MediaLayoutRangeToString(obj.MaxLabelGap));
            }
            catch (EPDMException ex)
            {
                MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_LAYOUT_ERROR);
                EPDMClose();
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
            }
        }
        
        /// <summary>
        /// This is the method that get string of Media layout range.
        /// </summary>
        /// <param name="minRangeString">String of minimum range.</param>
        /// <param name="maxRangeString">String of maximum range.</param>
        /// <returns>Stiring of input possible range.</returns>
        private String GetMediaLayoutRangeString(String minRangeString, String maxRangeString)
        {
            try
            {
                String rangeString = "";

                if (minRangeString.Length > 0 && maxRangeString.Length > 0)
                {
                    rangeString = "( " + minRangeString + " - " + maxRangeString + " )";
                }
                return rangeString;
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
                throw;
            }
        }
        
        /// <summary>
        /// This is the method that change media layout range value into string.
        /// </summary>
        /// <param name="size">Media layout range value.(0.1mm unit)</param>
        /// <returns>String of Media layout range .(mm unit)</returns>
        private String MediaLayoutRangeToString(double size)
        {
            try
            {
                // Changed the size. (0.1mm to mm)
                double d = size / 10;

                String _strSize = d.ToString("f1");
                return _strSize;
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
                throw;
            }
        }

        /// <summary>
        /// This is the method that check parameter of textbox is input or not. 
        /// </summary>
        /// <returns>true or false</returns>
        private bool AddButton_CheckParam()
        {
            try
            {
                if ((MediaLayoutNameTextBox.Text.Length == 0) ||
                      (MediaWidthTextBox.Text.Length == 0) ||
                      (LabelWidthTextBox.Text.Length == 0) ||
                      (LabelLengthTextBox.Text.Length == 0) ||
                      (LabelsGapTextBox.Text.Length == 0))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
                throw;
            }
        }

        /// <summary>
        /// This is the method that close AddMediaLayoutDlg. 
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
                    MessageString.GetEPDMError((int)ex.ErrCode, MessageString.STATE_MEDIA_LAYOUT_ERROR);
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
