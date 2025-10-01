using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.epson.label.driver;
using Newtonsoft.Json;

namespace SampleProgram
{
    /// <summary>
    /// This is the class of PringDlg.
    /// </summary>
    public partial class PrintDlg : Form
    {
        #region Fields

        private String _devName;
        private String _portName;
        private bool _isENSInitialize = true;
        private Image _image;
        private string _labelData;

        #endregion

        #region Constructors/Destructors

        /// <summary>
        /// This is the constructor of PrintDlg.
        /// </summary>
        /// <param name="devName">Device name of the selected printer.</param>
        /// <param name="portName">Port name of the selected printer.</param>
        public PrintDlg(String devName, String portName)
        {
            InitializeComponent();

            _devName = devName;
            _portName = portName;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// This is the event handler that load PrintDlg.
        /// </summary>
        private void PrintDlg_Load(object sender, EventArgs e)
        {
            try
            {
                this.CopiesNumericUpDown.Value = 1;
            }
            catch (Exception)
            {
                // Error handling.
                ExceptionClose();
            }
        }

        /// <summary>
        /// This is the event handler that implement PrintButton action.
        /// Get printer status and implements printing action.
        /// </summary> 
        private void PrintButton_Click(object sender, EventArgs e)
        {
            ENSControl obj = ENSControl.GetInstance();
            try
            {
                try
                {
                    obj.Initialize(_devName, _portName);
                }
                catch (DllNotFoundException)
                {
                    _isENSInitialize = false;
                    MessageString.GetSystemError((int)MessageString.STATE_ENS_DLL_NOT_FOUND);
                }

                if (_isENSInitialize == true)
                {
                    StatusCode sc = obj.GetStatusInformation();
                    ErrorCode ec = obj.GetErrorInformation();
                    WARNING_INFO wc = obj.GetWarningInformation();
                    if (MessageString.GetPrinterStatusError(sc, ec, wc) == false)
                    {
                        return;
                    }
                }

                // Parse the label data
                LabelData labelData = null;
                if (!string.IsNullOrEmpty(_labelData))
                {
                    System.Diagnostics.Debug.WriteLine($"PrintDlg - Raw label data: {_labelData}");

                    try
                    {
                        // Try to parse as JSON first
                        labelData = JsonConvert.DeserializeObject<LabelData>(_labelData);
                        System.Diagnostics.Debug.WriteLine("Successfully parsed as JSON");
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine("JSON parsing failed, trying form data parsing");

                        // If JSON parsing fails, try parsing as form data
                        try
                        {
                            var parsedData = ParseFormData(_labelData);
                            labelData = new LabelData
                            {
                                title = parsedData.ContainsKey("title") ? parsedData["title"] : "Label",
                                fields = new List<Field>()
                            };

                            // Add content as a field if it exists
                            if (parsedData.ContainsKey("content"))
                            {
                                labelData.fields.Add(new Field
                                {
                                    name = "Content",
                                    value = parsedData["content"]
                                });
                            }

                            // Add any other fields from the form data
                            foreach (var kvp in parsedData)
                            {
                                if (kvp.Key != "title" && kvp.Key != "content")
                                {
                                    labelData.fields.Add(new Field
                                    {
                                        name = kvp.Key,
                                        value = kvp.Value
                                    });
                                }
                            }

                            System.Diagnostics.Debug.WriteLine($"Successfully parsed as form data: {labelData.title}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Form data parsing failed: {ex.Message}");

                            // Final fallback: create a default LabelData
                            labelData = new LabelData
                            {
                                title = "Error",
                                fields = new List<Field>
                                {
                                    new Field { name = "Raw Data", value = _labelData }
                                }
                            };
                        }
                    }
                }
                else
                {
                    // Fallback: create a default LabelData if _labelData is null or empty
                    labelData = new LabelData
                    {
                        title = "Default",
                        fields = new List<Field>
                        {
                            new Field { name = "Field1", value = "Value1" },
                            new Field { name = "Field2", value = "Value2" }
                        }
                    };
                }

                System.Diagnostics.Debug.WriteLine("Final Label Data: " + JsonConvert.SerializeObject(labelData, Formatting.Indented));

                Print print = new Print(_devName, _portName, (int)CopiesNumericUpDown.Value, labelData);
                print.DoPrinting();

            }
            catch (ENSException ex)
            {
                MessageString.GetSDKError((int)ex.ErrCode, MessageString.STATE_ENS_ERROR);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Print error: {ex.Message}\n{ex.StackTrace}");
                // Error handling.
                ExceptionClose();
            }
            finally
            {
                ENSClose();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parse URL-encoded form data
        /// </summary>
        private Dictionary<string, string> ParseFormData(string data)
        {
            var result = new Dictionary<string, string>();

            // Split by & to get key-value pairs
            string[] pairs = data.Split('&');

            foreach (string pair in pairs)
            {
                // Split by = to separate key and value
                string[] keyValue = pair.Split(new[] { '=' }, 2);
                if (keyValue.Length == 2)
                {
                    string key = Uri.UnescapeDataString(keyValue[0]);
                    string value = Uri.UnescapeDataString(keyValue[1]);
                    result[key] = value;
                }
            }

            return result;
        }

        /// <summary>
        /// This is the method that close PrintDlg.
        /// </summary>
        private void ENSClose()
        {
            if (_isENSInitialize == true)
            {
                try
                {
                    ENSControl.GetInstance().Release();
                }
                catch (ENSException ex)
                {
                    MessageString.GetSDKError((int)ex.ErrCode, MessageString.STATE_ENS_ERROR);
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
            ENSClose();
            Environment.Exit(0);
        }

        public void SetImage(Image image)
        {
            _image = image;
            // Use _image in your print logic
        }

        public void SetLabelData(string labelData)
        {
            _labelData = labelData;
            System.Diagnostics.Debug.WriteLine($"SetLabelData called with: {labelData}");
        }

        #endregion
    }
}