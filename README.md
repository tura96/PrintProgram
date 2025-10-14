
# Label Printer Sample Program API Documentation

## Overview
The Label Printer Sample Program provides an HTTP-based API for receiving print data, which is processed by the `HttpRequestReceiver` class. The API allows external systems to send data (via GET or POST requests) to trigger label printing. The API supports Cross-Origin Resource Sharing (CORS) to enable requests from web clients or other domains.

- **Base URL**: Configurable (e.g., `http://localhost:8080/`), set via the `prefix` parameter in `HttpRequestReceiver.Start(prefix)`.
- **Supported Methods**: GET, POST, OPTIONS (due to CORS support).
- **CORS**: Enabled with `Access-Control-Allow-Origin: *`, `Access-Control-Allow-Methods: GET, POST, OPTIONS`, and `Access-Control-Allow-Headers: Content-Type`.

## Endpoints

### 1. Send Print Data
This endpoint accepts print data via GET or POST requests, which is then processed by the application for label printing.

#### Request
- **URL**: `{base_url}/` (e.g., `http://localhost:8080/`)
- **Methods**: GET, POST
- **Headers**:
  - `Content-Type`: Optional for GET; required for POST (e.g., `application/x-www-form-urlencoded`, `application/json`).
- **Query Parameters (for GET)**:
  - Any key-value pairs in the query string (e.g., `?data=print_content` or `?key1=value1&key2=value2`).
  - The entire query string (excluding the leading `?`) is passed to the application as raw data.
- **Body (for POST)**:
  - Raw data in the request body (e.g., JSON, form data, or plain text).
  - Example (JSON):
    ```json
    {
      "label": "Sample Label",
      "content": "Print this text"
    }
    ```
  - Example (Form Data):
    ```
    label=Sample+Label&content=Print+this+text
    ```

#### Response
- **Status Code**: `200 OK`
- **Body**: Plain text response `"OK"`
- **Headers**:
  - `Access-Control-Allow-Origin: *`
  - `Access-Control-Allow-Methods: GET, POST, OPTIONS`
  - `Access-Control-Allow-Headers: Content-Type`
- **Content-Type**: `text/plain`

#### Example Requests
1. **GET Request**:
   ```
   GET /?label=Sample+Label&content=Print+this+text HTTP/1.1
   Host: localhost:8080
   ```
   - Query string (`label=Sample+Label&content=Print+this+text`) is passed as raw data.

2. **POST Request (JSON)**:
   ```
   POST / HTTP/1.1
   Host: localhost:8080
   Content-Type: application/json
   Content-Length: 54

   {
     "label": "Sample Label",
     "content": "Print this text"
   }
   ```
   - The JSON body is passed as raw data.

3. **POST Request (Form Data)**:
   ```
   POST / HTTP/1.1
   Host: localhost:8080
   Content-Type: application/x-www-form-urlencoded
   Content-Length: 37

   label=Sample+Label&content=Print+this+text
   ```
   - The form data is passed as raw data.

#### Example Response
```
HTTP/1.1 200 OK
Content-Type: text/plain
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET, POST, OPTIONS
Access-Control-Allow-Headers: Content-Type
Content-Length: 2

OK
```

#### Notes
- The application processes the request data (query string for GET, body for POST) and raises the `RequestReceived` event with the raw data as a string.
- The application does not validate or parse the data; it is up to the event handler (e.g., in the UI) to interpret the data (e.g., as JSON, form data, or plain text).
- If the `AutoPrintCheckBox` is enabled in the UI, received requests may trigger automatic printing.

### 2. CORS Preflight Request
The API supports CORS preflight requests to allow cross-origin clients.

#### Request
- **URL**: `{base_url}/` (e.g., `http://localhost:8080/`)
- **Method**: OPTIONS
- **Headers**:
  - `Origin`: The requesting origin (e.g., `http://example.com`).
  - `Access-Control-Request-Method`: The method to be used (e.g., `POST`).
  - `Access-Control-Request-Headers`: Headers to be used (e.g., `Content-Type`).

#### Response
- **Status Code**: `200 OK`
- **Headers**:
  - `Access-Control-Allow-Origin: *`
  - `Access-Control-Allow-Methods: GET, POST, OPTIONS`
  - `Access-Control-Allow-Headers: Content-Type`
- **Body**: Empty

#### Example Request
```
OPTIONS / HTTP/1.1
Host: localhost:8080
Origin: http://example.com
Access-Control-Request-Method: POST
Access-Control-Request-Headers: Content-Type
```

#### Example Response
```
HTTP/1.1 200 OK
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET, POST, OPTIONS
Access-Control-Allow-Headers: Content-Type
Content-Length: 0
```

## Error Handling
- **HttpListenerException**: Occurs if the listener encounters network issues or is closed. The error is logged to the debug console, and a MessageBox is shown to the user (unless the listener was intentionally stopped).
- **General Exceptions**: Any other exceptions (e.g., invalid request data) are logged to the debug console with a stack trace and displayed in a MessageBox.
- **No Data**: If no query string (for GET) or body (for POST) is provided, no `RequestReceived` event is raised, but the response is still `200 OK`.

## Example Client Code
### JavaScript (Fetch API)
```javascript
// POST JSON request
fetch('http://localhost:8080/', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    label: 'Sample Label',
    content: 'Print this text'
  })
})
  .then(response => response.text())
  .then(data => console.log(data)) // Outputs: OK
  .catch(error => console.error('Error:', error));

// GET request
fetch('http://localhost:8080/?label=Sample+Label&content=Print+this+text')
  .then(response => response.text())
  .then(data => console.log(data)) // Outputs: OK
  .catch(error => console.error('Error:', error));
```
