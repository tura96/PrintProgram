using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Shapes;
using Path = System.IO.Path;
using Rectangle = System.Drawing.Rectangle;

namespace SampleProgram
{
    class Print
    {
        #region Fields

        private PrintDocument _pdPrint;
        private int _pageNumber = 0;
        private int _totalPrintPage = 0;
        private bool _exceptionFlag = false;
        // Add a field for label data
        private LabelData _labelData;
        private int _topOffset = 20; // Variable offset from top in millimeters

        #endregion

        #region Event handler

        private void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    // Debug: Print label title and fields
                    Console.WriteLine($"[DEBUG] Label Title: {_labelData?.title}");
                    Console.WriteLine($"[DEBUG] Top Offset: {_topOffset}mm");
                    if (_labelData?.fields != null)
                    {
                        foreach (var field in _labelData.fields)
                        {
                            Console.WriteLine($"[DEBUG] Field: {field.name} = {field.value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[DEBUG] No label fields.");
                    }

                    PD_PrintPage_DrawImage(e);
                    PD_PrintPage_DrawLogo(e);
                    //PD_PrintPage_DrawImageFromUrl(e);
                    //PD_PrintPage_DrawBarcode(e);
                    PD_PrintPage_DrawLabelFields(e); // <-- Draw label data fields
                    PD_PrintPage_DrawQRcode(e); // <-- Updated to draw QR code
                    PD_PrintPage_DrawPageCount(e);
                    PD_PrintPage_DrawRectangle(e);
                    PD_PrintPage_Close(e);

                    // Debug: Print page number
                    Console.WriteLine($"[DEBUG] Printed page {_pageNumber} of {_totalPrintPage}");
                }
            }
            catch (Exception ex)
            {
                // Error handling.
                Console.WriteLine($"[ERROR] Exception in PD_PrintPage: {ex.Message}");
                e.HasMorePages = false;
                e.Cancel = true;
                _exceptionFlag = true;
            }
        }

        #endregion

        #region Methods

        // Update constructor to accept label data and top offset
        public Print(string devName, string portName, int totalPrintPage, LabelData labelData, int topOffset = 20)
        {
            try
            {
                // Create the PrintDocumentObject.
                _pdPrint = new PrintDocument();

                // Add the PrintPageEventHandler.
                _pdPrint.PrintPage += new PrintPageEventHandler(PD_PrintPage);

                // Set the using Printer.
                _pdPrint.PrinterSettings.PrinterName = devName;
                _pdPrint.DocumentName = "STVN Test Print";

                _totalPrintPage = totalPrintPage;
                _labelData = labelData; // Initialize label data
                _topOffset = topOffset; // Initialize top offset

                Console.WriteLine($"[DEBUG] Print initialized with top offset: {_topOffset}mm");

                return;
            }
            catch
            {
                // Error handling.
                throw;
            }
        }

        // Method to update top offset after construction
        public void SetTopOffset(int topOffset)
        {
            _topOffset = topOffset;
            Console.WriteLine($"[DEBUG] Top offset updated to: {_topOffset}mm");
        }

        // Method to get current top offset
        public int GetTopOffset()
        {
            return _topOffset;
        }

        public void DoPrinting()
        {
            try
            {
                // Start the print.
                _pdPrint.Print();
                if (_exceptionFlag == true)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        private void PD_PrintPage_DrawLogo(PrintPageEventArgs e)
        {
            try
            {
                // Set page unit.
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;
                // Set draw area with top offset
                Rectangle r = new Rectangle(0, _topOffset, 100, 20);
                // Use label data or default value
                string strLogo = _labelData?.title ?? "Sneaker";
                using (Font f = new Font("Arial", 45, FontStyle.Bold))
                using (Font f2 = new Font("Arial", 15, FontStyle.Bold))
                using (Pen p = new Pen(Color.White))
                using (Brush b = new SolidBrush(Color.White))
                {
                    // Fill background color.
                    e.Graphics.FillRectangle(Brushes.Blue, r);

                    // Draw string.
                    e.Graphics.DrawString(strLogo, f, Brushes.White, PD_GetCenterPosition(e.Graphics, strLogo, f, r));
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        // Add this method to draw label fields
        private void PD_PrintPage_DrawLabelFields(PrintPageEventArgs e)
        {
            Debug.WriteLine($"[DEBUG] Starting PD_PrintPage_DrawLabelFields");
            Debug.WriteLine($"[DEBUG] LabelData: {_labelData?.fields}");
            Debug.WriteLine($"[DEBUG] LabelData is null: {_labelData == null}");
            Debug.WriteLine($"[DEBUG] Fields is null: {_labelData?.fields == null}");
            Debug.WriteLine($"[DEBUG] Fields count: {_labelData?.fields?.Count ?? 0}");
            Debug.WriteLine($"[DEBUG] Top Offset: {_topOffset}mm");

            if (_labelData?.fields == null || _labelData.fields.Count == 0)
            {
                Debug.WriteLine($"[DEBUG] No fields to display - exiting early");
                return;
            }

            try
            {
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;
                Debug.WriteLine($"[DEBUG] Graphics PageUnit set to: {e.Graphics.PageUnit}");

                int startY = 73 + _topOffset; // Start below the logo with offset
                int lineHeight = 10;

                Debug.WriteLine($"[DEBUG] Starting Y position: {startY}");
                Debug.WriteLine($"[DEBUG] Line height: {lineHeight}");

                using (Font f = new Font("Arial", 12))
                {
                    Debug.WriteLine($"[DEBUG] Font created: {f.Name}, Size: {f.Size}, Style: {f.Style}");

                    foreach (var field in _labelData.fields)
                    {
                        string line = $"{field.name}: {field.value}";
                        Debug.WriteLine($"[DEBUG] Drawing line: '{line}' at position (5, {startY})");

                        // Test if the field data is valid
                        Debug.WriteLine($"[DEBUG] Field name: '{field.name}', value: '{field.value}'");
                        Debug.WriteLine($"[DEBUG] Name is null/empty: {string.IsNullOrEmpty(field.name)}");
                        Debug.WriteLine($"[DEBUG] Value is null/empty: {string.IsNullOrEmpty(field.value)}");

                        e.Graphics.DrawString(line, f, Brushes.Black, 0, startY);

                        // Test drawing a simple rectangle to verify graphics is working
                        //e.Graphics.DrawRectangle(Pens.Red, 5, startY, 50, 5);

                        startY += lineHeight;
                        Debug.WriteLine($"[DEBUG] Next Y position: {startY}");
                    }
                }

                Debug.WriteLine($"[DEBUG] Finished drawing all fields");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in PD_PrintPage_DrawLabelFields: {ex.Message}");
                Debug.WriteLine($"[ERROR] Stack trace: {ex.StackTrace}");
            }
        }

        private void PD_PrintPage_DrawImage(PrintPageEventArgs e)
        {
            try
            {
                // Set page unit.
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // The image is saved in the same location as the SampleProgram.exe.
                String currentDirectryPath = Directory.GetCurrentDirectory();
                String imageFileName = "logoheader.tif";
                String imageFilePath = Path.Combine(currentDirectryPath, imageFileName);

                // DEBUG: Log the path being used
                Debug.WriteLine($"[DEBUG] Current Directory: {currentDirectryPath}");
                Debug.WriteLine($"[DEBUG] Image File Path: {imageFilePath}");
                Debug.WriteLine($"[DEBUG] Top Offset: {_topOffset}mm");

                // DEBUG: Check if file exists
                if (!File.Exists(imageFilePath))
                {
                    Debug.WriteLine($"[ERROR] Image file not found at: {imageFilePath}");
                    MessageBox.Show($"Image file not found!\n\nPath: {imageFilePath}\n\nCurrent Directory: {currentDirectryPath}",
                                  "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageString.GetSystemError((int)MessageString.STATE_DRAWIMAGE_ERROR);
                    return;
                }

                Debug.WriteLine("[DEBUG] File exists, attempting to load image...");

                using (FileStream fs = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
                {
                    Debug.WriteLine($"[DEBUG] FileStream opened successfully. Length: {fs.Length} bytes");

                    using (Image img = Image.FromStream(fs))
                    {
                        Debug.WriteLine($"[DEBUG] Image loaded. Size: {img.Width}x{img.Height}, Format: {img.RawFormat}");

                        // Draw image with top offset
                        e.Graphics.DrawImage(img, 10, 10 + _topOffset, 70, 64);

                        Debug.WriteLine("[DEBUG] Image drawn successfully");
                    }
                }
            }
            catch (FileNotFoundException fnfEx)
            {
                Debug.WriteLine($"[ERROR] FileNotFoundException: {fnfEx.Message}");
                MessageBox.Show($"Image file not found!\n\n{fnfEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageString.GetSystemError((int)MessageString.STATE_DRAWIMAGE_ERROR);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Debug.WriteLine($"[ERROR] UnauthorizedAccessException: {uaEx.Message}");
                MessageBox.Show($"Access denied to image file!\n\n{uaEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageString.GetSystemError((int)MessageString.STATE_DRAWIMAGE_ERROR);
            }
            catch (OutOfMemoryException oomEx)
            {
                Debug.WriteLine($"[ERROR] OutOfMemoryException (Invalid image format): {oomEx.Message}");
                MessageBox.Show($"Invalid image format or corrupted file!\n\n{oomEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageString.GetSystemError((int)MessageString.STATE_DRAWIMAGE_ERROR);
            }
            catch (Exception ex)
            {
                // General error handling with detailed information
                Debug.WriteLine($"[ERROR] Exception Type: {ex.GetType().Name}");
                Debug.WriteLine($"[ERROR] Message: {ex.Message}");
                Debug.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                MessageBox.Show($"Error loading/drawing image!\n\nType: {ex.GetType().Name}\nMessage: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageString.GetSystemError((int)MessageString.STATE_DRAWIMAGE_ERROR);
            }
        }

        private void PD_PrintPage_DrawImageFromUrl(PrintPageEventArgs e)
        {
            try
            {
                // Check if image URL field exists and has value
                if (_labelData?.fields == null)
                {
                    Debug.WriteLine("[DEBUG] No label data fields available");
                    return;
                }

                // Find the image URL field - adjust field name as needed
                var imageField = _labelData.fields.FirstOrDefault(f =>
                    f.name?.ToLower() == "image" ||
                    f.name?.ToLower() == "imageurl" ||
                    f.name?.ToLower() == "logo");

                if (imageField == null || string.IsNullOrEmpty(imageField.value))
                {
                    Debug.WriteLine("[DEBUG] No image URL field found or URL is empty");
                    return;
                }

                string imageUrl = imageField.value;
                Debug.WriteLine($"[DEBUG] Image URL: {imageUrl}");
                Debug.WriteLine($"[DEBUG] Top Offset: {_topOffset}mm");

                // Set page unit
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // Set draw area - adjust coordinates as needed with top offset
                int x = 0;
                int y = 20 + _topOffset;
                int width = 80;
                int height = 68;

                Debug.WriteLine($"[DEBUG] Drawing image at position: ({x}, {y}), Size: {width}x{height}mm");

                // Download and draw image
                DrawImageFromUrl(e.Graphics, imageUrl, x, y, width, height);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in PD_PrintPage_DrawImageFromUrl: {ex.Message}");
                Debug.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
            }
        }

        private void DrawImageFromUrl(Graphics graphics, string imageUrl, int x, int y, int width, int height)
        {
            try
            {
                Debug.WriteLine($"[DEBUG] Starting image download from: {imageUrl}");

                using (WebClient webClient = new WebClient())
                {
                    // Download image data
                    byte[] imageData = webClient.DownloadData(imageUrl);
                    Debug.WriteLine($"[DEBUG] Image downloaded successfully. Size: {imageData.Length} bytes");

                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        using (Image img = Image.FromStream(ms))
                        {
                            Debug.WriteLine($"[DEBUG] Image loaded. Original Size: {img.Width}x{img.Height}, Format: {img.RawFormat}");

                            // Draw image
                            graphics.DrawImage(img, x, y, width, height);
                            Debug.WriteLine($"[DEBUG] Image drawn successfully at ({x}, {y})");
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                Debug.WriteLine($"[ERROR] WebException: {webEx.Message}");
                Debug.WriteLine($"[ERROR] Status: {webEx.Status}");
                if (webEx.Response is HttpWebResponse httpResponse)
                {
                    Debug.WriteLine($"[ERROR] HTTP Status Code: {httpResponse.StatusCode}");
                }
            }
            catch (NotSupportedException nsEx)
            {
                Debug.WriteLine($"[ERROR] NotSupportedException: {nsEx.Message}");
                Debug.WriteLine("[ERROR] The URI may not be a valid HTTP/HTTPS URL");
            }
            catch (ArgumentException argEx)
            {
                Debug.WriteLine($"[ERROR] ArgumentException (invalid URL): {argEx.Message}");
            }
            catch (OutOfMemoryException oomEx)
            {
                Debug.WriteLine($"[ERROR] OutOfMemoryException (invalid image format): {oomEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in DrawImageFromUrl: {ex.Message}");
                Debug.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
            }
        }

        private void PD_PrintPage_DrawBarcode(PrintPageEventArgs e)
        {
            try
            {
                // Set page unit.
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // Set draw area with top offset.
                Rectangle r = new Rectangle(0, 98 + _topOffset, 100, 20);

                // Select the barcode font that set in the printer driver.
                using (Font f = new Font("Courier New", 57))
                {
                    // Draw string.Fixed value of 20mm from the left edge.
                    e.Graphics.DrawString("*123456*", f, Brushes.Black, 20, 100 + _topOffset);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        // Add this helper method to generate QR code bitmap
        private Bitmap GenerateQRCodedev(string data, int pixelsPerModule = 20)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                return qrCode.GetGraphic(pixelsPerModule);
            }
        }

        // Update the DrawBarcode method to use QR code
        private void PD_PrintPage_DrawQRcode(PrintPageEventArgs e)
        {
            try
            {
                // Set page unit.
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // Get data for QR code (use label data or default)
                string qrData = "123456"; // Default
                Console.WriteLine("[DEBUG] Preparing to generate QR code: ", _labelData.fields);
                Console.WriteLine($"[DEBUG] Top Offset: {_topOffset}mm");

                if (_labelData?.fields != null)
                {
                    // Try to find a specific field for QR code, or use title
                    var qrField = _labelData.fields.FirstOrDefault(f => f.name.ToLower() == "qrcode" || f.name.ToLower() == "barcode");
                    if (qrField != null)
                    {
                        qrData = qrField.value;
                    }
                    else if (!string.IsNullOrEmpty(_labelData.title))
                    {
                        qrData = _labelData.title;
                    }
                }

                Console.WriteLine($"[DEBUG] Generating QR Code for: {qrData}");

                // Generate QR code
                using (Bitmap qrCodeImage = GenerateQRCodedev(qrData, 10))
                {
                    // Draw QR code - centered in the barcode area with top offset
                    int qrSize = 30; // 30mm square
                    int xPos = 55; // Center horizontally
                    int yPos = 70 + _topOffset; // Apply top offset
                    e.Graphics.DrawImage(qrCodeImage, xPos, yPos, qrSize, qrSize);
                }
            }
            catch (Exception ex)
            {
                // Error handling.
                Console.WriteLine($"[ERROR] QR Code generation failed: {ex.Message}");

                // Fallback to text if QR code fails
                using (Font f = new Font("Courier New", 20))
                {
                    e.Graphics.DrawString("QR Error", f, Brushes.Black, 20, 100 + _topOffset);
                }
            }
        }

        private Bitmap GenerateQRCode(string qrData, int v)
        {
            throw new NotImplementedException();
        }

        private void PD_PrintPage_DrawPageCount(PrintPageEventArgs e)
        {
            try
            {
                // Set page unit.
                e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                // Set draw area with top offset.
                Rectangle r = new Rectangle(0, 118 + _topOffset, 100, 12);

                // Set page count.
                _pageNumber++;

                using (Font f = new Font("Arial", 12, GraphicsUnit.Point))
                {
                    // Draw page count.
                    String strPageCount = String.Format("{0} / {1}", _pageNumber, _totalPrintPage);
                    e.Graphics.DrawString(strPageCount, f, Brushes.Black, PD_GetCenterPosition(e.Graphics, strPageCount, f, r));
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        private void PD_PrintPage_DrawRectangle(PrintPageEventArgs e)
        {
            try
            {
                using (Pen p = new Pen(Color.Black))
                {
                    // Set page unit.
                    e.Graphics.PageUnit = GraphicsUnit.Millimeter;

                    // Set pen width.
                    p.Width = 1;

                    // Draw rectangle with top offset.
                    e.Graphics.DrawRectangle(p, new Rectangle(0, _topOffset, 100, 20));
                    e.Graphics.DrawRectangle(p, new Rectangle(0, 20 + _topOffset, 100, 48));
                    //e.Graphics.DrawRectangle(p, new Rectangle(0, 98 + _topOffset, 100, 20));
                    //e.Graphics.DrawRectangle(p, new Rectangle(0, 118 + _topOffset, 100, 12));
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        private void PD_PrintPage_Close(PrintPageEventArgs e)
        {
            try
            {
                // Check final page.
                if (_pageNumber < _totalPrintPage)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        private Point PD_GetCenterPosition(Graphics g, String str, Font f, Rectangle r)
        {
            try
            {
                int xPos = 0, yPos = 0;
                using (StringFormat sf = new StringFormat())
                {
                    // Get String Drawing Size.
                    SizeF size = g.MeasureString(str, f, r.Width, sf);

                    // Center Position Calculated.
                    xPos = (int)(((r.Width - size.Width) / 2) + r.Left);
                    yPos = (int)(((r.Height - size.Height) / 2) + r.Top);
                    return new Point(xPos, yPos);
                }
            }
            catch (Exception)
            {
                // Error handling.
                throw;
            }
        }

        #endregion
    }
}