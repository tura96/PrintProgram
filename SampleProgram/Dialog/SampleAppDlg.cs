using com.epson.label.driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleProgram
{
    /// <summary>
    /// This is the main dialog class of application.
    /// </summary>
    public partial class SampleAppDlg : Form
    {
        #region Fields

        private List<PRINTER_INFO> _printerInfoList;
        private HttpRequestReceiver _httpReceiver;
        private string _latestHttpRequestData;
        private const int HTTP_TIMEOUT_SECONDS = 30;

        // Add a field to track auto print state
        private bool _autoPrintEnabled => AutoPrintCheckBox?.Checked == true;

        #endregion

        #region Constructors/Destructors

        public SampleAppDlg()
        {
            InitializeComponent();
            _printSourceComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// This is the event handler that load SampleAppDlg. 
        /// </summary>
        private void SampleAppDlg_Load(object sender, EventArgs e)
        {
            _printerInfoList = SelectPrinterInfo.GetPrinterInfoList();

            if (_printerInfoList.Count == 0)
            {
                MessageString.GetSystemError(MessageString.STATE_DRIVER_NOT_FOUND);
                this.Close();
                this.Dispose();
                return;
            }
            else
            {
                PrinterInfoComboBox.Items.Clear();

                foreach (PRINTER_INFO printerInfo in _printerInfoList)
                {
                    PrinterInfoComboBox.Items.Add(printerInfo.devName);
                    this.Controls.Add(PrinterInfoComboBox);
                }
                PrinterInfoComboBox.SelectedIndex = 0;
            }

            _httpReceiver = new HttpRequestReceiver();
            _httpReceiver.RequestReceived += HttpReceiver_RequestReceived;
            _httpReceiver.Start("http://localhost:8081/");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _httpReceiver?.Stop();
            base.OnFormClosed(e);
        }

        //private void HttpReceiver_RequestReceived(object sender, HttpRequestReceivedEventArgs e)
        //{
        //    Debug.WriteLine($"===== HTTP REQUEST RECEIVED =====");
        //    Debug.WriteLine($"Request data: {e.RequestData}");

        //    _latestHttpRequestData = e.RequestData;

        //    Debug.WriteLine($"Stored in _latestHttpRequestData: {_latestHttpRequestData}");

        //    this.Invoke((Action)(() =>
        //    {
        //        RequestsListBox.Items.Add(e.RequestData);

        //        // Auto-switch to HTTP Request mode when data is received
        //        _printSourceComboBox.SelectedItem = "HTTP Request";

        //        if (_printSourceComboBox.SelectedItem?.ToString() == "HTTP Request")
        //        {
        //            Debug.WriteLine("Calling PreviewLabel...");
        //            //PreviewLabel(e.RequestData);
        //        }

        //        // Auto print if enabled
        //        if (_autoPrintEnabled)
        //        {
        //            int index = PrinterInfoComboBox.SelectedIndex;
        //            if (index >= 0 && index < _printerInfoList.Count)
        //            {
        //                Debug.WriteLine("Calling PrintDlg...");
        //                PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName, autoPrint: true);
        //                printDlg.SetLabelData(e.RequestData);
        //                printDlg.ShowDialog(this);
        //            }
        //            else
        //            {
        //                MessageBox.Show("No printer selected for auto print.", "Auto Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //    }));
        //}

        //private void PreviewLabel(string data)
        //{
        //    LabelPreviewPanel.Paint -= LabelPreviewPanel_Paint;
        //    LabelPreviewPanel.Paint += LabelPreviewPanel_Paint;
        //    LabelPreviewPanel.Tag = data;
        //    LabelPreviewPanel.Invalidate();
        //}

        // Update the HttpReceiver_RequestReceived method in SampleAppDlg.cs

        private void HttpReceiver_RequestReceived(object sender, HttpRequestReceivedEventArgs e)
        {
            Debug.WriteLine($"===== HTTP REQUEST RECEIVED =====");
            Debug.WriteLine($"Request data: {e.RequestData}");

            _latestHttpRequestData = e.RequestData;

            Debug.WriteLine($"Stored in _latestHttpRequestData: {_latestHttpRequestData}");

            this.Invoke((Action)(() =>
            {
                RequestsListBox.Items.Add(e.RequestData);

                // Auto-switch to HTTP Request mode when data is received
                _printSourceComboBox.SelectedItem = "HTTP Request";

                if (_printSourceComboBox.SelectedItem?.ToString() == "HTTP Request")
                {
                    Debug.WriteLine("Calling PreviewLabel...");
                    PreviewLabel(e.RequestData);
                }

                // Auto print if enabled
                if (_autoPrintEnabled)
                {
                    Debug.WriteLine("Auto-print is enabled, triggering automatic print...");
                    PreviewLabel(e.RequestData);
                    AutoPrintFromHttpRequest();
                }
            }));
        }

        // Add this improved method for direct auto-printing
        private void AutoPrintFromHttpRequest()
        {
            try
            {
                int index = PrinterInfoComboBox.SelectedIndex;

                if (index < 0 || index >= _printerInfoList.Count)
                {
                    Debug.WriteLine("No printer selected for auto-print");
                    return;
                }

                if (string.IsNullOrEmpty(_latestHttpRequestData))
                {
                    Debug.WriteLine("No data to auto-print");
                    return;
                }

                string devName = _printerInfoList[index].devName;
                string portName = _printerInfoList[index].portName;

                Debug.WriteLine($"Auto-printing with printer: {devName}");
                Debug.WriteLine($"Raw data: {_latestHttpRequestData}");

                // Parse the label data - can be single or multiple labels
                List<LabelData> labelDataList = ParseLabelData(_latestHttpRequestData);

                if (labelDataList == null || labelDataList.Count == 0)
                {
                    Debug.WriteLine("Failed to parse label data for auto-print");
                    return;
                }

                Debug.WriteLine($"Parsed {labelDataList.Count} label(s) for auto-print");

                // Initialize printer
                ENSControl obj = ENSControl.GetInstance();
                bool isENSInitialized = false;

                try
                {
                    obj.Initialize(devName, portName);
                    isENSInitialized = true;
                }
                catch (DllNotFoundException)
                {
                    Debug.WriteLine("ENS DLL not found");
                    //MessageString.GetSystemError((int)MessageString.STATE_ENS_DLL_NOT_FOUND);
                    //return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to initialize printer: {ex.Message}");
                    return;
                }

                // Check printer status
                if (isENSInitialized)
                {
                    try
                    {
                        StatusCode sc = obj.GetStatusInformation();
                        ErrorCode ec = obj.GetErrorInformation();
                        WARNING_INFO wc = obj.GetWarningInformation();

                        if (!MessageString.GetPrinterStatusError(sc, ec, wc))
                        {
                            Debug.WriteLine("Printer status error detected");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to check printer status: {ex.Message}");
                    }
                }

                // Print each label directly without showing dialog
                try
                {
                    int copies = 1; // Default copies for auto-print
                    int successCount = 0;
                    int failCount = 0;

                    Debug.WriteLine($"Printing {labelDataList.Count} label(s) with {copies} copies each");

                    // Print each label separately
                    foreach (var labelData in labelDataList)
                    {
                        try
                        {
                            Debug.WriteLine($"Printing label: {labelData.title}");
                            Debug.WriteLine(JsonConvert.SerializeObject(labelData, Formatting.Indented));

                            // Create Print instance for single label
                            Print print = new Print(devName, portName, copies, labelData);
                            print.DoPrinting();

                            successCount++;
                            Debug.WriteLine($"Successfully printed label: {labelData.title}");
                        }
                        catch (Exception ex)
                        {
                            failCount++;
                            Debug.WriteLine($"Failed to print label '{labelData.title}': {ex.Message}");
                        }
                    }

                    Debug.WriteLine($"Auto-print completed: {successCount} successful, {failCount} failed");
                }
                catch (ENSException ex)
                {
                    Debug.WriteLine($"ENS error during auto-print: {ex.Message}");
                    MessageString.GetSDKError((int)ex.ErrCode, MessageString.STATE_ENS_ERROR);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during auto-print: {ex.Message}\n{ex.StackTrace}");
                }
                finally
                {
                    // Release printer connection
                    if (isENSInitialized)
                    {
                        try
                        {
                            obj.Release();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error releasing printer: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Auto-print error: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        // Add helper method to parse label data
        private List<LabelData> ParseLabelData(string data)
        {
            List<LabelData> labelDataList = new List<LabelData>();

            if (string.IsNullOrEmpty(data))
            {
                Debug.WriteLine("No label data to parse");
                return labelDataList;
            }

            string trimmedData = data.Trim();
            bool parsed = false;

            // Try to parse as JSON array first
            if (trimmedData.StartsWith("["))
            {
                try
                {
                    var arrayData = JsonConvert.DeserializeObject<List<LabelData>>(trimmedData);
                    if (arrayData != null && arrayData.Count > 0)
                    {
                        labelDataList = arrayData;
                        parsed = true;
                        Debug.WriteLine($"Successfully parsed as JSON array with {arrayData.Count} label(s)");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"JSON array parsing failed: {ex.Message}");
                }
            }

            // Try to parse as single JSON object
            if (!parsed && trimmedData.StartsWith("{"))
            {
                try
                {
                    var singleLabel = JsonConvert.DeserializeObject<LabelData>(trimmedData);
                    if (singleLabel != null)
                    {
                        labelDataList.Add(singleLabel);
                        parsed = true;
                        Debug.WriteLine("Successfully parsed as single JSON object");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Single JSON parsing failed: {ex.Message}");
                }
            }

            // Try parsing as form data if JSON parsing failed
            if (!parsed)
            {
                Debug.WriteLine("JSON parsing failed, trying form data parsing");

                try
                {
                    var parsedData = ParseFormData(data);
                    var labelData = new LabelData
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

                    labelDataList.Add(labelData);
                    parsed = true;
                    Debug.WriteLine($"Successfully parsed as form data: {labelData.title}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Form data parsing failed: {ex.Message}");
                }
            }

            // Final fallback: create an error label with raw data
            if (!parsed)
            {
                labelDataList.Add(new LabelData
                {
                    title = "Parse Error",
                    fields = new List<Field>
            {
                new Field { name = "Raw Data", value = data }
            }
                });
                Debug.WriteLine("All parsing failed, created error label with raw data");
            }

            return labelDataList;
        }

        private void PrintSourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Refresh preview when source changes
            if (!string.IsNullOrEmpty(_latestHttpRequestData))
            {
                PreviewLabel(_latestHttpRequestData);
            }
            else
            {
                LabelPreviewPanel.Invalidate();
            }
        }

        private void LabelPreviewPanel_Paint(object sender, PaintEventArgs pe)
        {
            pe.Graphics.Clear(Color.White);
            string source = _printSourceComboBox.SelectedItem?.ToString();
            Debug.WriteLine($"Start LabelPreviewPanel_Paint: {source}");

            if (source == "Sample Image")
            {
                // Draw a sample image
                pe.Graphics.DrawImage(Properties.Resources.SampleImage, new Point(10, 10));
            }
            else if (source == "HTTP Request")
            {
                string data = LabelPreviewPanel.Tag as string;
                if (!string.IsNullOrEmpty(data))
                {
                    try
                    {
                        Debug.WriteLine($"Raw data: {data}");

                        // Parse URL-encoded form data
                        var parsedData = ParseFormData(data);

                        Debug.WriteLine($"Parsed data count: {parsedData.Count}");
                        foreach (var kvp in parsedData)
                        {
                            Debug.WriteLine($"  Key: '{kvp.Key}', Value: '{kvp.Value}'");
                        }

                        string title = parsedData.ContainsKey("title") ? parsedData["title"] : "Label";
                        Debug.WriteLine($"Title: {title}");

                        pe.Graphics.DrawString(title, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(10, 10));
                        int y = 40;
                        if (parsedData.ContainsKey("content"))
                        {
                            string content = parsedData["content"];
                            Debug.WriteLine($"Content: {content}");
                            pe.Graphics.DrawString(content, new Font("Arial", 12), Brushes.Black, new PointF(10, y));
                        }
                        else
                        {
                            Debug.WriteLine("No 'content' key found in parsed data");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception occurred: {ex.GetType().Name}");
                        Debug.WriteLine($"Message: {ex.Message}");
                        Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                        // Fallback: just print raw data
                        pe.Graphics.DrawString(data, new Font("Arial", 12), Brushes.Black, new PointF(10, 10));
                    }
                }
            }
            else if (source == "JSON Data")
            {
                string data = LabelPreviewPanel.Tag as string;
                if (!string.IsNullOrEmpty(data))
                {
                    try
                    {
                        Debug.WriteLine($"Rendering JSON preview");

                        var labelData = JsonConvert.DeserializeObject<LabelData>(data);

                        if (labelData != null)
                        {
                            // Draw title
                            string title = labelData.title ?? "Label";
                            pe.Graphics.DrawString(title, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(10, 10));

                            // Draw fields
                            int y = 40;
                            if (labelData.fields != null)
                            {
                                foreach (var field in labelData.fields)
                                {
                                    string fieldText = $"{field.name}: {field.value}";
                                    pe.Graphics.DrawString(fieldText, new Font("Arial", 12), Brushes.Black, new PointF(10, y));
                                    y += 25;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception rendering JSON: {ex.Message}");

                        // Fallback: display error message
                        pe.Graphics.DrawString("Invalid JSON format", new Font("Arial", 12), Brushes.Red, new PointF(10, 10));
                        pe.Graphics.DrawString(ex.Message, new Font("Arial", 10), Brushes.Gray, new RectangleF(10, 40, LabelPreviewPanel.Width - 20, LabelPreviewPanel.Height - 50));
                    }
                }
            }
        }

        /// <summary>
        /// This is the event handler that implement PrinterInfoComboBox change action.
        /// </summary>
        private void PrinterInfoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = PrinterInfoComboBox.SelectedIndex;
            if (_printerInfoList[index].devName.StartsWith("EPSON CW-C65") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C60") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C40") ||
                (_printerInfoList[index].devName == "EPSON TM-C3500 Ver2") ||
                (_printerInfoList[index].devName == "EPSON TM-C3510 Ver2") ||
                (_printerInfoList[index].devName == "EPSON TM-C3520 Ver2"))
            {
                Step3Button.Enabled = false;
            }
            else
            {
                Step3Button.Enabled = true;
            }
        }

        private Dictionary<string, string> ParseFormData(string data)
        {
            Debug.WriteLine("ParseFormData called");
            var result = new Dictionary<string, string>();

            try
            {
                // Split by & to get key-value pairs
                string[] pairs = data.Split('&');
                Debug.WriteLine($"Found {pairs.Length} pairs");

                foreach (string pair in pairs)
                {
                    Debug.WriteLine($"Processing pair: '{pair}'");

                    // Split by = to separate key and value
                    string[] keyValue = pair.Split(new[] { '=' }, 2);
                    if (keyValue.Length == 2)
                    {
                        string key = Uri.UnescapeDataString(keyValue[0]);
                        string value = Uri.UnescapeDataString(keyValue[1]);
                        Debug.WriteLine($"  Decoded - Key: '{key}', Value: '{value}'");
                        result[key] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ParseFormData: {ex.Message}");
                throw;
            }

            return result;
        }

        /// <summary>
        /// This is the event handler that implement Step2Button action.
        /// Open the window of AddMediaLayoutDlg.
        /// </summary>
        private void Step2Button_Click(object sender, EventArgs e)
        {
            int index = PrinterInfoComboBox.SelectedIndex;
            if (_printerInfoList[index].devName.StartsWith("EPSON CW-C65") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C60") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C40"))
            {
                AddMediaLayoutDlg4 addMediaLayoutDlg4 = new AddMediaLayoutDlg4(_printerInfoList[index].devName, _printerInfoList[index].portName);
                addMediaLayoutDlg4.ShowDialog(this);
            }
            else if ((_printerInfoList[index].devName == "EPSON TM-C3500 Ver2") ||
                     (_printerInfoList[index].devName == "EPSON TM-C3510 Ver2") ||
                     (_printerInfoList[index].devName == "EPSON TM-C3520 Ver2"))
            {
                AddMediaLayoutDlg3 addMediaLayoutDlg3 = new AddMediaLayoutDlg3(_printerInfoList[index].devName, _printerInfoList[index].portName);
                addMediaLayoutDlg3.ShowDialog(this);
            }
            else
            {
                AddMediaLayoutDlg addMediaLayoutDlg = new AddMediaLayoutDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                addMediaLayoutDlg.ShowDialog(this);
            }
        }

        /// <summary>
        /// This is the event handler that implement Step3Button action.
        /// Open the window of SetRollPaperDlg.
        /// </summary>
        private void Step3Button_Click(object sender, EventArgs e)
        {
            int index = PrinterInfoComboBox.SelectedIndex;
            if (_printerInfoList[index].devName.StartsWith("EPSON CW-C65") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C60") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C40") ||
                (_printerInfoList[index].devName == "EPSON TM-C3500 Ver2") ||
                (_printerInfoList[index].devName == "EPSON TM-C3510 Ver2") ||
                (_printerInfoList[index].devName == "EPSON TM-C3520 Ver2"))
            {
                //The Step3Button action is not supported.
            }
            else
            {
                SetRollPaperDlg setRollPaperDlg = new SetRollPaperDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                setRollPaperDlg.ShowDialog(this);
            }
        }

        /// <summary>
        /// This is the event handler that implement Step4Button action.
        /// Open the window of PrintSettingsDlg.
        /// </summary>
        private void Step4Button_Click(object sender, EventArgs e)
        {
            int index = PrinterInfoComboBox.SelectedIndex;
            if (_printerInfoList[index].devName.StartsWith("EPSON CW-C65") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C60") ||
                _printerInfoList[index].devName.StartsWith("EPSON CW-C40"))
            {
                PrintSettingsDlg4 printSettingsDlg = new PrintSettingsDlg4(_printerInfoList[index].devName, _printerInfoList[index].portName);
                printSettingsDlg.ShowDialog(this);
            }
            else if ((_printerInfoList[index].devName == "EPSON TM-C3500 Ver2") ||
                     (_printerInfoList[index].devName == "EPSON TM-C3510 Ver2") ||
                     (_printerInfoList[index].devName == "EPSON TM-C3520 Ver2"))
            {
                PrintSettingsDlg3 printSettingsDlg = new PrintSettingsDlg3(_printerInfoList[index].devName, _printerInfoList[index].portName);
                printSettingsDlg.ShowDialog(this);
            }
            else
            {
                PrintSettingsDlg printSettingsDlg = new PrintSettingsDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                printSettingsDlg.ShowDialog(this);
            }
        }

        /// <summary>
        /// This is the event handler that implement Step5Button action.
        /// Open the window of PrintDlg.
        /// </summary>
        //private void Step5Button_Click(object sender, EventArgs e)
        //{
        //    int index = PrinterInfoComboBox.SelectedIndex;
        //    string source = _printSourceComboBox.SelectedItem?.ToString();

        //    Debug.WriteLine($"===== STEP 5 BUTTON CLICKED =====");
        //    Debug.WriteLine($"Source: {source}");
        //    Debug.WriteLine($"_latestHttpRequestData: '{_latestHttpRequestData}'");
        //    Debug.WriteLine($"Is null or empty: {string.IsNullOrEmpty(_latestHttpRequestData)}");

        //    if (source == "Sample Image")
        //    {
        //        PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
        //        printDlg.SetImage(Properties.Resources.SampleImage);
        //        printDlg.ShowDialog(this);
        //    }
        //    else if (source == "HTTP Request")
        //    {
        //        if (!string.IsNullOrEmpty(_latestHttpRequestData))
        //        {
        //            PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
        //            printDlg.SetLabelData(_latestHttpRequestData);
        //            printDlg.ShowDialog(this);
        //        }
        //        else
        //        {
        //            MessageBox.Show("No HTTP request data available.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    else if (source == "JSON Data")
        //    {
        //        if (!string.IsNullOrEmpty(_latestHttpRequestData))
        //        {
        //            try
        //            {
        //                var labelData = JsonConvert.DeserializeObject<LabelData>(_latestHttpRequestData);

        //                if (labelData == null)
        //                {
        //                    MessageBox.Show("Failed to parse JSON data 1.", "Print Error 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
        //                // Fix: Use SetLabelData(string) and pass the original JSON string
        //                printDlg.SetLabelData(_latestHttpRequestData);
        //                printDlg.ShowDialog(this);
        //            }
        //            catch (JsonException ex)
        //            {
        //                MessageBox.Show($"Invalid JSON format: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Json error: {ex.Message}");
        //                MessageBox.Show($"Error processing JSON data: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("No JSON data available.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    else
        //    {
        //        Debug.WriteLine($"Unknown source: {source}");
        //    }
        //}

        private void Step5Button_Click(object sender, EventArgs e)
        {
            int index = PrinterInfoComboBox.SelectedIndex;
            string source = _printSourceComboBox.SelectedItem?.ToString();

            Debug.WriteLine($"===== STEP 5 BUTTON CLICKED =====");
            Debug.WriteLine($"Source: {source}");
            Debug.WriteLine($"_latestHttpRequestData: '{_latestHttpRequestData}'");
            Debug.WriteLine($"Is null or empty: {string.IsNullOrEmpty(_latestHttpRequestData)}");

            // Validate printer selection
            if (index < 0 || index >= _printerInfoList.Count)
            {
                MessageBox.Show("Please select a valid printer.", "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (source == "Sample Image")
            {
                PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                printDlg.SetImage(Properties.Resources.SampleImage);
                printDlg.ShowDialog(this);
            }
            else if (source == "HTTP Request")
            {
                if (!string.IsNullOrEmpty(_latestHttpRequestData))
                {
                    PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                    printDlg.SetLabelData(_latestHttpRequestData);
                    printDlg.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("No HTTP request data available.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (source == "JSON Data")
            {
                if (!string.IsNullOrEmpty(_latestHttpRequestData))
                {
                    try
                    {
                        string trimmedJson = _latestHttpRequestData.Trim();
                        int labelCount = 0;

                        // Check if it's a JSON array
                        if (trimmedJson.StartsWith("["))
                        {
                            var labelArray = JsonConvert.DeserializeObject<List<LabelData>>(trimmedJson);

                            if (labelArray == null || labelArray.Count == 0)
                            {
                                MessageBox.Show("JSON array is empty or invalid.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Debug.WriteLine("Failed to parse JSON array or array is empty");
                                return;
                            }

                            labelCount = labelArray.Count;
                            Debug.WriteLine($"Parsed JSON array with {labelCount} label(s)");

                            // Confirm printing multiple labels
                            if (labelCount > 1)
                            {
                                DialogResult result = MessageBox.Show(
                                    $"You are about to print {labelCount} labels.\n\nDo you want to continue?",
                                    "Confirm Print",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                                if (result != DialogResult.Yes)
                                {
                                    Debug.WriteLine("User cancelled printing multiple labels");
                                    return;
                                }
                            }
                        }
                        // Check if it's a single JSON object
                        else if (trimmedJson.StartsWith("{"))
                        {
                            var singleLabel = JsonConvert.DeserializeObject<LabelData>(trimmedJson);

                            if (singleLabel == null)
                            {
                                MessageBox.Show("Failed to parse JSON data.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Debug.WriteLine("Failed to parse single JSON object");
                                return;
                            }

                            labelCount = 1;
                            Debug.WriteLine("Parsed single JSON object");
                        }
                        else
                        {
                            MessageBox.Show("Invalid JSON format. Must be an object {...} or array [...]",
                                          "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Debug.WriteLine("JSON doesn't start with { or [");
                            return;
                        }

                        // Create and show print dialog
                        PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);

                        // Pass the original JSON string - PrintDlg will handle parsing
                        printDlg.SetLabelData(_latestHttpRequestData);

                        Debug.WriteLine($"Opening PrintDlg with {labelCount} label(s)");
                        printDlg.ShowDialog(this);
                        
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show($"Invalid JSON format:\n\n{ex.Message}", "JSON Parse Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine($"JSON parsing error: {ex.Message}\n{ex.StackTrace}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing JSON data:\n\n{ex.Message}", "Print Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine($"Unexpected error: {ex.Message}\n{ex.StackTrace}");
                    }
                }
                else
                {
                    MessageBox.Show("No JSON data available. Please fetch or load JSON data first.",
                                  "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Debug.WriteLine("_latestHttpRequestData is null or empty");
                }
            }
            else
            {
                MessageBox.Show($"Unknown print source: {source}", "Print Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.WriteLine($"Unknown source: {source}");
            }
        }

        /// <summary>
        /// This is the event handler that implement ExitButton action.
        /// Close the window of SampleAppDlg.
        /// </summary>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void RequestsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Allow user to select previous requests
            //if (RequestsListBox.SelectedItem != null)
            //{
            //    _latestHttpRequestData = RequestsListBox.SelectedItem.ToString();
            //    PreviewLabel(_latestHttpRequestData);
            //}
        }

        /// <summary>
        /// Fetches JSON data from a URL and updates the preview
        /// </summary>
        //private async void FetchJsonButton_Click(object sender, EventArgs e)
        //{
        //    string url = _urlTextBox.Text.Trim();

        //    // Validate placeholder text
        //    if (string.IsNullOrEmpty(url) || url == "Enter JSON URL")
        //    {
        //        MessageBox.Show("Please enter a valid URL.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Validate URL format
        //    if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) ||
        //        (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        //    {
        //        MessageBox.Show("Please enter a valid HTTP or HTTPS URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Disable button during fetch
        //    _fetchJsonButton.Enabled = false;
        //    _fetchJsonButton.Text = "Fetching...";

        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            // Set timeout
        //            client.Timeout = TimeSpan.FromSeconds(HTTP_TIMEOUT_SECONDS);

        //            // Fetch JSON
        //            var json = await client.GetStringAsync(url);

        //            // Validate JSON format
        //            try
        //            {
        //                JsonConvert.DeserializeObject<LabelData>(json);
        //            }
        //            catch (JsonException)
        //            {
        //                MessageBox.Show("The fetched data is not in valid JSON format for labels.", "Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            // Store and display
        //            _latestHttpRequestData = json;
        //            _printSourceComboBox.SelectedItem = "JSON Data"; // Fixed: was "HTTP Request"
        //            PreviewLabel(json);
        //            RequestsListBox.Items.Add($"[JSON] {url}");

        //            MessageBox.Show("JSON data fetched and previewed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        MessageBox.Show($"Network error: {ex.Message}", "Fetch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (TaskCanceledException)
        //    {
        //        MessageBox.Show($"Request timed out after {HTTP_TIMEOUT_SECONDS} seconds.", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to fetch JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        // Re-enable button
        //        _fetchJsonButton.Enabled = true;
        //        _fetchJsonButton.Text = "Fetch JSON";
        //    }
        //}

        private async void FetchJsonButton_Click(object sender, EventArgs e)
        {
            string url = _urlTextBox.Text.Trim();

            // Validate placeholder text
            if (string.IsNullOrEmpty(url) || url == "Enter JSON URL")
            {
                MessageBox.Show("Please enter a valid URL.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate URL format
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                MessageBox.Show("Please enter a valid HTTP or HTTPS URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Disable button during fetch
            _fetchJsonButton.Enabled = false;
            _fetchJsonButton.Text = "Fetching...";

            try
            {
                using (var client = new HttpClient())
                {
                    // Set timeout
                    client.Timeout = TimeSpan.FromSeconds(HTTP_TIMEOUT_SECONDS);

                    // Fetch JSON
                    var json = await client.GetStringAsync(url);

                    // Validate and parse JSON format
                    int labelCount = 0;
                    bool isValidJson = false;

                    try
                    {
                        string trimmedJson = json.Trim();

                        // Check if it's a JSON array
                        if (trimmedJson.StartsWith("["))
                        {
                            var labelArray = JsonConvert.DeserializeObject<List<LabelData>>(json);

                            if (labelArray != null && labelArray.Count > 0)
                            {
                                isValidJson = true;
                                labelCount = labelArray.Count;
                                System.Diagnostics.Debug.WriteLine($"Parsed JSON array with {labelCount} label(s)");
                            }
                            else
                            {
                                MessageBox.Show("The JSON array is empty or invalid.", "Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        // Check if it's a single JSON object
                        else if (trimmedJson.StartsWith("{"))
                        {
                            var singleLabel = JsonConvert.DeserializeObject<LabelData>(json);

                            if (singleLabel != null)
                            {
                                isValidJson = true;
                                labelCount = 1;
                                System.Diagnostics.Debug.WriteLine("Parsed single JSON object");
                            }
                            else
                            {
                                MessageBox.Show("The JSON object is invalid.", "Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("The fetched data is not a valid JSON object or array.", "Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show($"The fetched data is not in valid JSON format for labels.\n\nError: {ex.Message}",
                                      "Invalid JSON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Diagnostics.Debug.WriteLine($"JSON parsing error: {ex.Message}");
                        return;
                    }

                    if (!isValidJson)
                    {
                        MessageBox.Show("Unable to parse the JSON data.", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Store and display
                    _latestHttpRequestData = json;
                    _printSourceComboBox.SelectedItem = "JSON Data";

                    // Preview the label(s)
                    PreviewLabel(json);

                    // Add to requests list with label count
                    string itemText = labelCount > 1
                        ? $"[JSON] {url} ({labelCount} labels)"
                        : $"[JSON] {url}";
                    RequestsListBox.Items.Add(itemText);

                    // Show success message
                    string successMessage = labelCount > 1
                        ? $"JSON data fetched successfully.\n\n{labelCount} labels ready to print."
                        : "JSON data fetched and previewed successfully.";

                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Network error: {ex.Message}", "Fetch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"HTTP request error: {ex.Message}");
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show($"Request timed out after {HTTP_TIMEOUT_SECONDS} seconds.", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to fetch JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Unexpected error: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // Re-enable button
                _fetchJsonButton.Enabled = true;
                _fetchJsonButton.Text = "Fetch JSON";
            }
        }

        /// <summary>
        /// Preview label data in the UI
        /// </summary>
        private void PreviewLabel(string json)
        {
            try
            {
                string trimmedJson = json.Trim();

                // Handle array of labels - preview the first one
                if (trimmedJson.StartsWith("["))
                {
                    var labelArray = JsonConvert.DeserializeObject<List<LabelData>>(json);

                    if (labelArray != null && labelArray.Count > 0)
                    {
                        var firstLabel = labelArray[0];
                        DisplayLabelPreview(firstLabel, labelArray.Count);
                    }
                }
                // Handle single label
                else if (trimmedJson.StartsWith("{"))
                {
                    var singleLabel = JsonConvert.DeserializeObject<LabelData>(json);

                    if (singleLabel != null)
                    {
                        DisplayLabelPreview(singleLabel, 1);
                    }
                }
                // Handle URL-encoded query string (e.g., title=STVN2025&content=Product...)
                else if (trimmedJson.Contains("="))
                {
                    var label = ParseLabelFromQueryString(trimmedJson);
                    if (label != null)
                        DisplayLabelPreview(label, 1);
                }
                else
                {

                    var singleLabel = JsonConvert.DeserializeObject<LabelData>(json);
                    System.Diagnostics.Debug.WriteLine($"Preview data error : {json}");
                    DisplayLabelPreview(singleLabel, 1);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Preview error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Preview data error : {json}");

            }
        }

        /// <summary>
        /// Display label preview in the UI
        /// </summary>
        private void DisplayLabelPreview(LabelData label, int totalCount)
        {
            StringBuilder previewText = new StringBuilder();

            if (totalCount > 1)
            {
                previewText.AppendLine($"Preview (1 of {totalCount} labels):");
                previewText.AppendLine(new string('-', 40));
            }

            previewText.AppendLine($"Title: {label.title}");
            //previewText.AppendLine();

            if (label.fields != null && label.fields.Count > 0)
            {
                previewText.AppendLine("Fields:");
                foreach (var field in label.fields)
                {
                    previewText.AppendLine($"  {field.name}: {field.value}");
                }
            }

            if (!string.IsNullOrEmpty(label.qrcode))
            {
                previewText.AppendLine($"QR Code: {label.qrcode}");
            }

            string finalPreview = previewText.ToString();

            // ✅ Clear any old content in the panel
            LabelPreviewPanel.Controls.Clear();

            // ✅ Create a label to hold the preview text
            Label previewLabel = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 8),
                Text = finalPreview,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(2),
                AutoEllipsis = true
            };

            // ✅ Optional: make it scrollable if long text
            LabelPreviewPanel.AutoScroll = true;

            // ✅ Add the label to the panel
            LabelPreviewPanel.Controls.Add(previewLabel);

            // Debug log
            System.Diagnostics.Debug.WriteLine(finalPreview);
        }


        /// <summary>
        /// This is the event handler that implement ClearRequestsButton action.
        /// Clear the HTTP requests list.
        /// </summary>
        private void ClearRequestsButton_Click(object sender, EventArgs e)
        {
            RequestsListBox.Items.Clear();
        }

        private void PrintSourceComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void AutoPrintCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private LabelData ParseLabelFromQueryString(string query)
        {
            try
            {
                var label = new LabelData();
                var fields = new List<Field>();
                var pairs = query.Split('&');

                foreach (var pair in pairs)
                {
                    var kv = pair.Split('=');
                    if (kv.Length != 2) continue;

                    string key = Uri.UnescapeDataString(kv[0]);
                    string value = Uri.UnescapeDataString(kv[1]);

                    switch (key.ToLower())
                    {
                        case "title":
                            label.title = value;
                            break;
                        case "qrcode":
                            label.qrcode = value;
                            break;
                        default:
                            fields.Add(new Field { name = key, value = value });
                            break;
                    }
                }

                label.fields = fields;
                return label;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Query parse error: {ex.Message}");
                return null;
            }
        }



    }



    /// <summary>
    /// Data model for JSON label data
    /// </summary>
    public class LabelData
    {
        public string title { get; set; }
        public List<Field> fields { get; set; }
        public string qrcode { get; set; }
    };

    /// <summary>
    /// Data model for label fields
    /// </summary>
    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    };
}