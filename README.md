# Label Printer Sample Program API Documentation

## Overview

The **Label Printer Sample Program** provides an HTTP-based API for receiving structured print data through the `HttpRequestReceiver` component.
External systems can send JSON or form data to this API to trigger label printing with support for text, image, and QR code fields.

* **Base URL**: Configurable via the `prefix` parameter in `HttpRequestReceiver.Start(prefix)` (e.g., `http://localhost:8080/`).
* **Supported Methods**: `GET`, `POST`, `OPTIONS`
* **CORS**: Fully enabled to allow integration with web-based applications.

  ```
  Access-Control-Allow-Origin: *
  Access-Control-Allow-Methods: GET, POST, OPTIONS
  Access-Control-Allow-Headers: Content-Type
  ```

---

## Endpoints

### 1. Send Print Data

This endpoint receives label data via HTTP `POST` or `GET` and passes it to the label printing logic.

#### Request

* **URL**:
  `{base_url}/`
  Example: `http://localhost:8080/`

* **Methods**:
  `POST`, `GET`

* **Headers**:

  * `Content-Type`:

    * For JSON: `application/json`
    * For form data: `application/x-www-form-urlencoded`

#### Supported Data Format

The API accepts structured JSON with the following schema:

| Field    | Type     | Description                                                                                        |
| -------- | -------- | -------------------------------------------------------------------------------------------------- |
| `title`  | `string` | The title or name of the label template.                                                           |
| `fields` | `array`  | A collection of key-value pairs representing label fields. Each field includes `name` and `value`. |

##### Example JSON Body

```json
{
  "title": "adsdasads",
  "fields": [
    {
      "name": "Name",
      "value": "xzczxc"
    },
    {
      "name": "Position",
      "value": "xzcxz"
    },
    {
      "name": "Company",
      "value": "cxzc"
    }
  ]
}
```

##### Special Field Handling

The system supports special field names for dynamic rendering:

| Field Name                        | Functionality                                                                                    |
| --------------------------------- | ------------------------------------------------------------------------------------------------ |
| `"image"`, `"imageurl"`, `"logo"` | Interpreted as an **image URL**. The application will load and print the image at the given URL. |
| `"qrcode"`                        | The value is converted into a **QR code**, which is printed on the label.                        |

> Matching for these special fields is **case-insensitive** (e.g., `"Image"`, `"Logo"`, `"QRCode"` are all valid).

#### GET Example

You can alternatively send simple key-value pairs using query parameters.
Example:

```
GET /?title=adsdasads&Name=xzczxc&Position=xzcxz&Company=cxzc HTTP/1.1
Host: localhost:8080
```

---

#### Example POST Request (JSON)

```
POST / HTTP/1.1
Host: localhost:8080
Content-Type: application/json
Content-Length: 158

{
  "title": "adsdasads",
  "fields": [
    { "name": "Name", "value": "xzczxc" },
    { "name": "Position", "value": "xzcxz" },
    { "name": "Company", "value": "cxzc" }
  ]
}
```

#### Example POST Request (Form Data)

```
POST / HTTP/1.1
Host: localhost:8080
Content-Type: application/x-www-form-urlencoded
Content-Length: 85

title=adsdasads&Name=xzczxc&Position=xzcxz&Company=cxzc
```

---

#### Response

* **Status Code**: `200 OK`
* **Content-Type**: `text/plain`
* **Body**:
  `"OK"`
* **Headers**:

  ```
  Access-Control-Allow-Origin: *
  Access-Control-Allow-Methods: GET, POST, OPTIONS
  Access-Control-Allow-Headers: Content-Type
  ```

##### Example Response

```
HTTP/1.1 200 OK
Content-Type: text/plain
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET, POST, OPTIONS
Access-Control-Allow-Headers: Content-Type
Content-Length: 2

OK
```

---

### 2. CORS Preflight Request

The API supports preflight (OPTIONS) requests for CORS-enabled web applications.

#### Request Example

```
OPTIONS / HTTP/1.1
Host: localhost:8080
Origin: http://example.com
Access-Control-Request-Method: POST
Access-Control-Request-Headers: Content-Type
```

#### Response Example

```
HTTP/1.1 200 OK
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET, POST, OPTIONS
Access-Control-Allow-Headers: Content-Type
Content-Length: 0
```

---

## Data Processing Behavior

* The application extracts the raw body (for POST) or query string (for GET) and raises the `RequestReceived` event.
* The JSON structure is deserialized into a `LabelData` object containing:

  * `title`
  * `fields[]` (list of `{ name, value }` pairs)
* During rendering:

  * Text fields are displayed as text.
  * Fields with names `"image"`, `"imageurl"`, or `"logo"` are treated as **image URLs**.
  * A field named `"qrcode"` triggers **QR code generation** using its value.
* If `"AutoPrintCheckBox"` is enabled, printing is triggered automatically upon data receipt.

---

## Error Handling

| Type                    | Description                                                                                                             |
| ----------------------- | ----------------------------------------------------------------------------------------------------------------------- |
| `HttpListenerException` | Occurs when the listener stops unexpectedly or network issues arise. Logged and displayed unless stopped intentionally. |
| `Invalid Data Format`   | If the JSON body is malformed or missing required fields, the data is ignored but the response remains `200 OK`.        |
| `General Exception`     | All other errors are logged to the debug console with stack trace information.                                          |
| `Empty Request`         | Requests with no data still return `200 OK` but do not trigger `RequestReceived`.                                       |

---

## Example JavaScript Client

```javascript
// POST JSON request
fetch('http://localhost:8080/', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    title: 'adsdasads',
    fields: [
      { name: 'Name', value: 'xzczxc' },
      { name: 'Position', value: 'xzcxz' },
      { name: 'Company', value: 'cxzc' },
      { name: 'QRCode', value: 'https://example.com/profile?id=123' },
      { name: 'Logo', value: 'https://example.com/logo.png' }
    ]
  })
})
  .then(response => response.text())
  .then(data => console.log(data)) // Output: OK
  .catch(error => console.error('Error:', error));
```

---

Would you like me to append an **“Extended Example”** showing how the application would visually interpret (render) this data — i.e., label layout showing text, QR code, and logo placement? This could help clarify how the `"fields"` translate to printed output.
