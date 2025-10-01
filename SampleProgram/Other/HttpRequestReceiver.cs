using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SampleProgram
{
    public class HttpRequestReceivedEventArgs : EventArgs
    {
        public string RequestData { get; set; }
    }

    public class HttpRequestReceiver
    {
        private HttpListener _listener;
        private Thread _listenerThread;
        private volatile bool _shouldStop;

        public event EventHandler<HttpRequestReceivedEventArgs> RequestReceived;

        public void Start(string prefix)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
            _listener.Start();
            _shouldStop = false;
            _listenerThread = new Thread(ListenLoop);
            _listenerThread.IsBackground = true;
            _listenerThread.Start();
        }

        public void Stop()
        {
            _shouldStop = true;
            _listener?.Close();
            _listenerThread?.Join();
        }

        private void ListenLoop()
        {
            while (!_shouldStop && _listener.IsListening)
            {
                try
                {
                    var context = _listener.GetContext();
                    string data = string.Empty;

                    System.Diagnostics.Debug.WriteLine($"Request Method: {context.Request.HttpMethod}");
                    System.Diagnostics.Debug.WriteLine($"Content Type: {context.Request.ContentType}");
                    System.Diagnostics.Debug.WriteLine($"Content Length: {context.Request.ContentLength64}");

                    // Check if it's a GET request with query string
                    if (context.Request.HttpMethod == "GET" && !string.IsNullOrEmpty(context.Request.Url.Query))
                    {
                        // Remove the leading '?' from query string
                        data = context.Request.Url.Query.TrimStart('?');
                        System.Diagnostics.Debug.WriteLine($"GET Query String: {data}");
                    }
                    // Check if it's a POST request with form data
                    else if (context.Request.HttpMethod == "POST" && context.Request.HasEntityBody)
                    {
                        using (var reader = new System.IO.StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                        {
                            data = reader.ReadToEnd();
                            System.Diagnostics.Debug.WriteLine($"POST Body: {data}");
                        }
                    }

                    if (!string.IsNullOrEmpty(data))
                    {
                        RequestReceived?.Invoke(this, new HttpRequestReceivedEventArgs { RequestData = data });
                    }

                    // Send response
                    context.Response.StatusCode = 200;
                    byte[] buffer = Encoding.UTF8.GetBytes("OK");
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.Close();
                }
                catch (HttpListenerException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"HttpListenerException: {ex.Message}");
                    if (!_shouldStop) // Only show message if not intentionally stopped
                    {
                        MessageBox.Show($"HttpListenerException: {ex.Message}", "Listener Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\n{ex.StackTrace}");
                    MessageBox.Show($"Exception: {ex.Message}", "Listener Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}