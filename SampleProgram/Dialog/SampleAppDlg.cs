using Newtonsoft.Json; // Add NuGet package for JSON parsing
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SampleProgram
{
    /// <summary>
    /// This is the main dialog class of application.
    /// </summary>
    public partial class  SampleAppDlg : Form
    {
        #region Fields

        private List<PRINTER_INFO> _printerInfoList;
        private HttpRequestReceiver _httpReceiver;
        private string _latestHttpRequestData;
        private ComboBox _printSourceComboBox;

        #endregion

        #region Constructors/Destructors

        public SampleAppDlg()
        {
            InitializeComponent();
            _printSourceComboBox = new ComboBox
            {
                Location = new Point(22, 410),
                Size = new Size(200, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _printSourceComboBox.Items.Add("Sample Image");
            _printSourceComboBox.Items.Add("HTTP Request");
            _printSourceComboBox.SelectedIndex = 0;
            this.Controls.Add(_printSourceComboBox);
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

        private void HttpReceiver_RequestReceived(object sender, HttpRequestReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"===== HTTP REQUEST RECEIVED =====");
            System.Diagnostics.Debug.WriteLine($"Request data: {e.RequestData}");

            _latestHttpRequestData = e.RequestData;

            System.Diagnostics.Debug.WriteLine($"Stored in _latestHttpRequestData: {_latestHttpRequestData}");

            this.Invoke((Action)(() =>
            {
                RequestsListBox.Items.Add(e.RequestData);

                // Auto-switch to HTTP Request mode when data is received
                _printSourceComboBox.SelectedItem = "HTTP Request";

                if (_printSourceComboBox.SelectedItem?.ToString() == "HTTP Request")
                {
                    System.Diagnostics.Debug.WriteLine("Calling PreviewLabel...");
                    PreviewLabel(e.RequestData);
                }
            }));
        }

        private void PreviewLabel(string data)
        {
            LabelPreviewPanel.Paint -= LabelPreviewPanel_Paint;
            LabelPreviewPanel.Paint += LabelPreviewPanel_Paint;
            LabelPreviewPanel.Tag = data;
            LabelPreviewPanel.Invalidate();
        }

        private void LabelPreviewPanel_Paint(object sender, PaintEventArgs pe)
        {
            pe.Graphics.Clear(Color.White);
            string source = _printSourceComboBox.SelectedItem?.ToString();
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
                        // Debug: Log the raw data
                        Debug.WriteLine($"Raw data: {data}");

                        // Parse URL-encoded form data
                        var parsedData = ParseFormData(data);

                        // Debug: Log parsed data
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
                        // Debug: Log the exception
                        Debug.WriteLine($"Exception occurred: {ex.GetType().Name}");
                        Debug.WriteLine($"Message: {ex.Message}");
                        Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                        // Fallback: just print raw data
                        pe.Graphics.DrawString(data, new Font("Arial", 12), Brushes.Black, new PointF(10, 10));
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
        private void Step5Button_Click(object sender, EventArgs e)
        {
            int index = PrinterInfoComboBox.SelectedIndex;
            string source = _printSourceComboBox.SelectedItem?.ToString();

            System.Diagnostics.Debug.WriteLine($"===== STEP 5 BUTTON CLICKED =====");
            System.Diagnostics.Debug.WriteLine($"Source: {source}");
            System.Diagnostics.Debug.WriteLine($"_latestHttpRequestData: '{_latestHttpRequestData}'");
            System.Diagnostics.Debug.WriteLine($"Is null or empty: {string.IsNullOrEmpty(_latestHttpRequestData)}");

            if (source == "Sample Image")
            {
                // Print sample image logic
                PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                printDlg.SetImage(Properties.Resources.SampleImage);
                printDlg.ShowDialog(this);
            }
            else if (source == "HTTP Request")
            {
                if (!string.IsNullOrEmpty(_latestHttpRequestData))
                {
                    System.Diagnostics.Debug.WriteLine($"Passing data to PrintDlg: {_latestHttpRequestData}");
                    // Print label from HTTP request data
                    PrintDlg printDlg = new PrintDlg(_printerInfoList[index].devName, _printerInfoList[index].portName);
                    printDlg.SetLabelData(_latestHttpRequestData);
                    printDlg.ShowDialog(this);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: _latestHttpRequestData is null or empty!");
                    MessageBox.Show("No HTTP request data available.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Unknown source: {source}");
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

        }
    }

    // Replace the dynamic usage with a strongly-typed class for deserialization
    public class LabelData
    {
        public string title { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
