namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if PORTABLE
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Runtime.ExceptionServices;
#endif
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>The main client class for working with AvaTax.</summary>
    /// <remarks>This file contains all the basic behavior.  Individual APIs are in the other partial class.</remarks>
    public partial class AvaTaxClient
    {
        private readonly Dictionary<string, string> clientHeaders = new();

        private readonly Uri envUri;

#if PORTABLE
        private static HttpClient _client = new();
#endif

        /// <summary>Tracks the amount of time spent on the most recent API call.</summary>
        /// <value>The last call time.</value>
        public CallDuration? LastCallTime { get; set; }

#if NET45
        /// <summary> Returns the version of the SDK that was compiled </summary>
        /// <value>The type of the sdk.</value>
        public static string SDK_TYPE { get { return "NET45"; } }
#elif NETSTANDARD1_6
        /// <summary> Returns the version of the SDK that was compiled </summary>
        /// <value>The type of the sdk.</value>
        public static string SDK_TYPE { get { return "NETSTANDARD1_6"; } }
#elif NET20
        /// <summary> Returns the version of the SDK that was compiled </summary>
        /// <value>The type of the sdk.</value>
        public static string SDK_TYPE { get { return "NET20"; } }
#elif NET5_0
        /// <summary> Returns the version of the SDK that was compiled </summary>
        /// <value>The type of the sdk.</value>
        public static string SDK_TYPE { get { return "NET5"; } }
#endif

        #region Constructor
        /// <summary>Generate a client that connects to one of the standard AvaTax servers.</summary>
        /// <param name="appName">    Name of the application.</param>
        /// <param name="appVersion"> The application version.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <param name="environment">The environment.</param>
        public AvaTaxClient(string appName, string appVersion, string machineName, AvaTaxEnvironment environment)
        {
            // Redo the client identifier
            WithClientIdentifier(appName, appVersion, machineName);
            // Setup the URI
            switch (environment)
            {
                case AvaTaxEnvironment.Sandbox:
                {
                    envUri = new(Constants.AVATAX_SANDBOX_URL);
                    break;
                }
                case AvaTaxEnvironment.Production:
                {
                    envUri = new(Constants.AVATAX_PRODUCTION_URL);
                    break;
                }
                default:
                {
                    throw new("Unrecognized Environment");
                }
            }
        }

        /// <summary>Generate a client that connects to a custom server.</summary>
        /// <param name="appName">          Name of the application.</param>
        /// <param name="appVersion">       The application version.</param>
        /// <param name="machineName">      Name of the machine.</param>
        /// <param name="customEnvironment">The custom environment.</param>
        public AvaTaxClient(string appName, string appVersion, string machineName, Uri customEnvironment)
        {
            // Redo the client identifier
            WithClientIdentifier(appName, appVersion, machineName);
            envUri = customEnvironment;
        }
        #endregion

        #region Security
        /// <summary>Sets the default security header string.</summary>
        /// <param name="headerString">The header string.</param>
        /// <returns>An AvaTaxClient.</returns>
        public AvaTaxClient WithSecurity(string headerString)
        {
            return WithCustomHeader("Authorization", headerString);
        }

        /// <summary>Set security using username/password.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>An AvaTaxClient.</returns>
        public AvaTaxClient WithSecurity(string username, string password)
        {
            return WithSecurity(
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))}");
        }

        /// <summary>Set security using AccountId / License Key.</summary>
        /// <param name="accountId"> Identifier for the account.</param>
        /// <param name="licenseKey">The license key.</param>
        /// <returns>An AvaTaxClient.</returns>
        public AvaTaxClient WithSecurity(int accountId, string licenseKey)
        {
            return WithSecurity(
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{accountId}:{licenseKey}"))}");
        }

        /// <summary>Set security using a bearer token.</summary>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>An AvaTaxClient.</returns>
        public AvaTaxClient WithBearerToken(string bearerToken)
        {
            WithSecurity("Bearer " + bearerToken);
            return this;
        }
        #endregion

        #region Custom headers
        /// <summary>Add custom header to this client.</summary>
        /// <param name="name"> Name of header.</param>
        /// <param name="value">Value of header.</param>
        /// <returns>An AvaTaxClient.</returns>
        public AvaTaxClient WithCustomHeader(string name, string value)
        {
            clientHeaders[name] = value;
            return this;
        }
        #endregion

        #region Client Identification
        /// <summary>Configure client identification.</summary>
        /// <param name="appName">    Name of the application.</param>
        /// <param name="appVersion"> The application version.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <returns>An AvaTaxClient.</returns>
        public AvaTaxClient WithClientIdentifier(string appName, string appVersion, string machineName)
        {
            clientHeaders.Add(
                Constants.AVALARA_CLIENT_HEADER,
                $"{appName}; {appVersion}; CSharpRestClient; {API_VERSION}; {machineName}");
            return this;
        }
        #endregion

        #region REST Call Interface
#if PORTABLE
        /// <summary>
        /// Implementation of client APIs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T> RestCallAsync<T>(string verb, AvaTaxPath relativePath, object content = null)
        {
            CallDuration cd = new CallDuration();
            var s = await RestCallStringAsync(verb, relativePath, content, cd).ConfigureAwait(false);
            var o = JsonConvert.DeserializeObject<T>(s);
            cd.FinishParse();
            this.LastCallTime = cd;
            return o;
        }

        /// <summary>
        /// Direct implementation of client APIs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public T RestCall<T>(string verb, AvaTaxPath relativePath, object content = null)
        {
            try
            {
                return RestCallAsync<T>(verb, relativePath, content).Result;
            }
            // Unroll single-exception aggregates for ease of use
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Count == 1)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }
                throw;
            }
        }

        /// <summary>
        /// Non-async method for downloading a file
        /// </summary>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public FileResult RestCallFile(string verb, AvaTaxPath relativePath, object payload = null)
        {
            try
            {
                return RestCallFileAsync(verb, relativePath, payload).Result;
                // Unroll single-exception aggregates for ease of use
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Count == 1)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }
                throw;
            }
        }
#endif
        #endregion

        #region Implementation
        private JsonSerializerSettings? serializer_settings;

        private JsonSerializerSettings SerializerSettings
        {
            get
            {
                if (serializer_settings == null)
                {
                    lock (lockObject)
                    {
                        serializer_settings = new()
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                        };
                        serializer_settings.Converters.Add(new StringEnumConverter());
                    }
                }
                return serializer_settings;
            }
        }

#if PORTABLE
        /// <summary>
        /// Implementation of raw file-returning async API
        /// </summary>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task<FileResult> RestCallFileAsync(string verb, AvaTaxPath relativePath, object content = null)
        {
            CallDuration cd = new CallDuration();

            // Convert the JSON payload, if any
            string jsonPayload = null;
            string mimeType = null;

            if (content != null && content is FileResult) {
                content = ((FileResult)content).Data;
                mimeType = ((FileResult)content).ContentType;
            } else if (content != null) {
                jsonPayload = JsonConvert.SerializeObject(content, SerializerSettings);
                mimeType = "application/json";
            }

            // Call REST
            using (var result = await InternalRestCallAsync(cd, verb, relativePath, jsonPayload, mimeType).ConfigureAwait(false)) {

                // Read the result
                if (result.IsSuccessStatusCode) {
                    var fr = new FileResult()
                    {
                        ContentType = result.Content.Headers.GetValues("Content-Type").FirstOrDefault(),
                        Filename = GetDispositionFilename(result.Content.Headers.GetValues("Content-Disposition").FirstOrDefault()),
                        Data = await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false)
                    };

                    // Capture timings
                    cd.FinishParse();
                    this.LastCallTime = cd;

                    // Capture information about this API call and make it available for logging
                    var eventargs = new AvaTaxCallEventArgs() { HttpVerb = verb.ToUpper(), Code = result.StatusCode, RequestUri = new Uri(_envUri, relativePath.ToString()), RequestBody = jsonPayload, ResponseBody = fr.Data, Duration = cd };
                    OnCallCompleted(eventargs);
                    return fr;

                // Handle exceptions and convert them to AvaTax results
                } else {
                    var errorResponseString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var err = JsonConvert.DeserializeObject<ErrorResult>(errorResponseString);
                    cd.FinishParse();
                    this.LastCallTime = cd;

                    // Capture information about this API call error and make it available for logging
                    var eventargs = new AvaTaxCallEventArgs() { HttpVerb = verb.ToUpper(), Code = result.StatusCode, RequestUri = new Uri(_envUri, relativePath.ToString()), RequestBody = jsonPayload, ResponseString = errorResponseString, Duration = cd };
                    OnCallCompleted(eventargs);
                    throw new AvaTaxError(err, result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Implementation of raw request API
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="jsonPayload"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> InternalRestCallAsync(CallDuration cd, string verb, AvaTaxPath relativePath, object jsonPayload, string mimeType = null)
        {
            // Setup the request
            using (var request = new HttpRequestMessage()) {
                request.Method = new HttpMethod(verb);
                request.RequestUri = new Uri(_envUri, relativePath.ToString());

                // Add headers
                foreach (var key in _clientHeaders.Keys)
                {
                    request.Headers.Add(key, _clientHeaders[key]);
                }

                //Add payload if present.
                if (mimeType == "multipart/form-data") {
                    request.Content = jsonPayload as MultipartFormDataContent;
                } else if (jsonPayload != null) {
                    request.Content = new StringContent(jsonPayload as string, Encoding.UTF8, mimeType);
                }

                // Send
                cd.FinishSetup();

                return await _client.SendAsync(request).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Implementation of raw string-returning async API
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task<string> RestCallStringAsync(string verb, AvaTaxPath relativePath, object content = null, CallDuration cd = null)
        {
            if (cd == null) cd = new CallDuration();

            // Convert the JSON payload, if any
            object jsonPayload = null;
            string mimeType = null;

            if(content != null && content is FileResult) {
                var fr = (FileResult)content;
                mimeType = fr.ContentType;
                MultipartFormDataContent mpfdc = new MultipartFormDataContent("----dataBoundary");
                ByteArrayContent byteArrayContent = new ByteArrayContent(fr.Data);
                byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
                mpfdc.Add(byteArrayContent, fr.Filename, fr.Filename);
                jsonPayload = mpfdc;
            } else if (content != null) {
                jsonPayload = JsonConvert.SerializeObject(content, SerializerSettings);
                mimeType = "application/json";
            }

            // Call REST
            using (var result = await InternalRestCallAsync(cd, verb, relativePath, jsonPayload, mimeType).ConfigureAwait(false)) {

                // Read the result
                var responseString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                // Determine server duration
                var sd = DetectDuration(result, "serverduration");
                var dd = DetectDuration(result, "dataduration");
                var td = DetectDuration(result, "serviceduration");
                cd.FinishReceive(sd, dd, td);

                // Capture information about this API call and make it available for logging
                var eventargs = new AvaTaxCallEventArgs() { HttpVerb = verb.ToUpper(), Code = result.StatusCode, RequestUri = new Uri(_envUri, relativePath.ToString()), RequestBody = jsonPayload as string, ResponseString = responseString, Duration = cd };
                OnCallCompleted(eventargs);

                // Deserialize the result
                if (result.IsSuccessStatusCode) {
                    return responseString;
                } else {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    cd.FinishParse();
                    this.LastCallTime = cd;
                    throw new AvaTaxError(err, result.StatusCode);
                }
            }
        }
        private TimeSpan? DetectDuration(HttpResponseMessage result, string name)
        {
            IEnumerable<string> values = null;
            if (result.Headers.TryGetValues(name, out values)) {
                var firstHeader = values.FirstOrDefault();
                if (firstHeader != null) {
                    TimeSpan duration = default(TimeSpan);
                    if (TimeSpan.TryParse(firstHeader, out duration)) {
                        return duration;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Implementation of raw string-returning API
        /// </summary>
        /// <param name="verb"></param>
        /// <param name="relativePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private string RestCallString(string verb, AvaTaxPath relativePath, object content = null)
        {
            try
            {
                return RestCallStringAsync(verb, relativePath, content).Result;
                // Unroll single-exception aggregates for ease of use
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Count == 1)
                {
                    ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                }
                throw;
            }
        }
#else
        /// <summary>Direct implementation of client APIs to object values.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="verb">        The verb.</param>
        /// <param name="relativePath">Full pathname of the relative file.</param>
        /// <param name="content">     The content.</param>
        /// <returns>A T.</returns>
        private T RestCall<T>(string verb, AvaTaxPath relativePath, object? content = null)
        {
            var s = RestCallString(verb, relativePath, content);
            return JsonConvert.DeserializeObject<T>(s)!;
        }

        /// <summary>Direct implementation of client APIs to string values.</summary>
        /// <param name="verb">        The verb.</param>
        /// <param name="relativePath">Full pathname of the relative file.</param>
        /// <param name="content">     The content.</param>
        /// <returns>A FileResult.</returns>
        private FileResult RestCallFile(string verb, AvaTaxPath relativePath, object? content = null)
        {
            var path = CombinePath(envUri.ToString(), relativePath.ToString());
            string? jsonPayload = null;
            // Use HttpWebRequest so we can get a decent response
            var wr = (HttpWebRequest)WebRequest.Create(path);
            wr.Proxy = null;
            // Add headers
            foreach (var key in clientHeaders.Keys)
            {
                wr.Headers.Add(key, clientHeaders[key]);
            }
            // Convert the name-value pairs into a byte array
            wr.Method = verb;
            if (content != null)
            {
                wr.ContentType = Constants.JSON_MIME_TYPE;
                wr.ServicePoint.Expect100Continue = false;
                // Encode the payload
                jsonPayload = JsonConvert.SerializeObject(content, SerializerSettings);
                var encoding = new UTF8Encoding();
                var data = encoding.GetBytes(jsonPayload);
                wr.ContentLength = data.Length;
                // Call the server
                using var s = wr.GetRequestStream();
                s.Write(data, 0, data.Length);
            }
            // Transmit, and get back the response, save it to a temp file
            try
            {
                using var response = wr.GetResponse();
                using var inStream = response.GetResponseStream();
                const int BUFFER_SIZE = 1024;
                var chunks = new List<byte>();
                var totalBytes = 0;
                var bytesRead = 0;
                do
                {
                    var buffer = new byte[BUFFER_SIZE];
                    bytesRead = inStream.Read(buffer, 0, BUFFER_SIZE);
                    if (bytesRead == BUFFER_SIZE)
                    {
                        chunks.AddRange(buffer);
                    }
                    else
                    {
                        for (var i = 0; i < bytesRead; i++)
                        {
                            chunks.Add(buffer[i]);
                        }
                    }
                    totalBytes += bytesRead;
                }
                while (bytesRead > 0);
                if (totalBytes <= 0)
                {
                    throw new IOException("Response contained no data");
                }
                // Here's your file result
                var fr = new FileResult
                {
                    ContentType = response.Headers["Content-Type"],
                    Filename = GetDispositionFilename(response.Headers["Content-Disposition"]!),
                    Data = chunks.ToArray(),
                };
                // Track the API call
                var eventargs = new AvaTaxCallEventArgs
                {
                    Code = ((HttpWebResponse)response).StatusCode,
                    Duration = null,
                    HttpVerb = wr.Method,
                    RequestBody = jsonPayload,
                    ResponseBody = fr.Data,
                    RequestUri = new(path),
                };
                OnCallCompleted(eventargs);
                return fr;
            }
            // Catch a web exception
            catch (WebException webex) when (webex.Response is HttpWebResponse httpWebResponse)
            {
                using var stream = httpWebResponse.GetResponseStream();
                using var reader = new StreamReader(stream);
                var errString = reader.ReadToEnd();
                // Track the API call
                var eventargs = new AvaTaxCallEventArgs
                {
                    Code = httpWebResponse.StatusCode,
                    Duration = null,
                    HttpVerb = wr.Method,
                    RequestBody = jsonPayload,
                    ResponseString = errString,
                    RequestUri = new(path),
                };
                OnCallCompleted(eventargs);
                // Pass on the error
                var err = JsonConvert.DeserializeObject<ErrorResult>(errString);
                throw new AvaTaxError(err, httpWebResponse.StatusCode);
            }
        }

        /// <summary>Direct implementation of client APIs to string values.</summary>
        /// <param name="verb">        The verb.</param>
        /// <param name="relativePath">Full pathname of the relative file.</param>
        /// <param name="content">     The content.</param>
        /// <returns>A string.</returns>
        private string RestCallString(string verb, AvaTaxPath relativePath, object? content = null)
        {
            var path = CombinePath(envUri.ToString(), relativePath.ToString());
            string? jsonPayload = null;
            string? mimeType = null;
            // Use HttpWebRequest so we can get a decent response
            var wr = (HttpWebRequest)WebRequest.Create(path);
            wr.Proxy = null;
            // Add headers
            foreach (var key in clientHeaders.Keys)
            {
                wr.Headers.Add(key, clientHeaders[key]);
            }
            // Convert the name-value pairs into a byte array
            wr.Method = verb;
            // Upload file.
            if (content is FileResult fr)
            {
                wr.KeepAlive = true;
                ////mimeType = fr.ContentType;
                ////content = fr.Data;
                const string dataBoundary = "----dataBoundary";
                wr.ContentType = "multipart/form-data; boundary=" + dataBoundary;
                var boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + dataBoundary + "\r\n");
                using var rs = wr.GetRequestStream();
                rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                var header = $"Content-Disposition: form-data; name=\"{fr.Filename}\"; filename=\"{fr.Filename}\"\r\nContent-Type: {fr.ContentType}\r\n\r\n";
                var headerBytes = Encoding.UTF8.GetBytes(header);
                rs.Write(headerBytes, 0, headerBytes.Length);
                rs.Write(fr.Data!, 0, fr.Data!.Length);
                var trailer = Encoding.ASCII.GetBytes("\r\n--" + dataBoundary + "--\r\n");
                rs.Write(trailer, 0, trailer.Length);
            }
            else if (content != null)
            {
                wr.ContentType = mimeType ?? Constants.JSON_MIME_TYPE;
                wr.ServicePoint.Expect100Continue = false;
                // Encode the payload
                jsonPayload = JsonConvert.SerializeObject(content, SerializerSettings);
                var encoding = new UTF8Encoding();
                var data = encoding.GetBytes(jsonPayload);
                wr.ContentLength = data.Length;
                // Call the server
                using var s = wr.GetRequestStream();
                s.Write(data, 0, data.Length);
            }
            // Transmit, and get back the response, save it to a temp file
            try
            {
                using var response = wr.GetResponse();
                using var inStream = response.GetResponseStream();
                using var reader = new StreamReader(inStream);
                var responseString = reader.ReadToEnd();
                // Track the API call
                var eventargs = new AvaTaxCallEventArgs
                {
                    Code = ((HttpWebResponse)response).StatusCode,
                    Duration = null,
                    HttpVerb = wr.Method,
                    RequestBody = jsonPayload,
                    ResponseString = responseString,
                    RequestUri = new(path),
                };
                OnCallCompleted(eventargs);
                // Here's the results
                return responseString;
            }
            // Catch a web exception
            catch (WebException webex) when (webex.Response is HttpWebResponse httpWebResponse)
            {
                using var stream = httpWebResponse.GetResponseStream();
                using var reader = new StreamReader(stream);
                var errString = reader.ReadToEnd();
                // Track the API call
                var eventargs = new AvaTaxCallEventArgs
                {
                    Code = httpWebResponse.StatusCode,
                    Duration = null,
                    HttpVerb = wr.Method,
                    RequestBody = jsonPayload,
                    ResponseString = errString,
                    RequestUri = new(path),
                };
                OnCallCompleted(eventargs);
                // Here's the results
                var err = JsonConvert.DeserializeObject<ErrorResult>(errString);
                throw new AvaTaxError(err, httpWebResponse.StatusCode);
            }
        }

        private static string CombinePath(string url1, string url2)
        {
            var endslash = url1.EndsWith("/");
            var startslash = url2.StartsWith("/");
            if (endslash && startslash)
            {
                return url1 + url2[1..];
            }
            if (!endslash && !startslash)
            {
                return url1 + "/" + url2;
            }
            return url1 + url2;
        }
#endif

        /// <summary>Shortcut to parse a content disposition to determine attachment filename.</summary>
        /// <param name="contentDisposition">The content disposition.</param>
        /// <returns>The disposition filename.</returns>
        private static string GetDispositionFilename(string contentDisposition)
        {
            const string Filename = "filename=";
            var index = contentDisposition.LastIndexOf(Filename, StringComparison.OrdinalIgnoreCase);
            return index > -1 ? contentDisposition[(index + Filename.Length)..] : contentDisposition;
        }
        #endregion

        #region Logging
        /// <summary>Hook this event to capture information about API calls.</summary>
        public event EventHandler? CallCompleted;

        /// <summary>Call this function to trigger notification.</summary>
        /// <param name="e">Event information to send to registered event handlers.</param>
        protected virtual void OnCallCompleted(EventArgs e)
        {
            CallCompleted?.Invoke(this, e);
        }

        /// <summary>Enable logging to a file.</summary>
        /// <param name="logFileName">Filename of the log file.</param>
        public void LogToFile(string logFileName)
        {
            this.logFileName = logFileName;
            CallCompleted += LogFile_CallCompleted;
        }

        private readonly object lockObject = new();

        private string? logFileName = null;

        private void LogFile_CallCompleted(object? sender, EventArgs e)
        {
            var evt = e as AvaTaxCallEventArgs;
            // Write to disk
            var g = Guid.NewGuid().ToString();
            var sb = new StringBuilder();
            sb.Append("=== REQUEST ")
                .Append(g)
                .Append(": ")
                .Append(evt?.HttpVerb ?? string.Empty)
                .Append(' ')
                .Append(evt?.RequestUri ?? new Uri(string.Empty))
                .Append(" Timestamp: ")
                .Append(DateTime.UtcNow)
                .Append(" ===\r\n")
                .Append(evt?.RequestBody ?? string.Empty)
                .Append("\r\n")
                .Append("=== RESPONSE ")
                .Append(g)
                .Append(": ")
                .Append(evt?.Code);
            if (evt?.ResponseString != null)
            {
                sb.Append(" Type: JSON ===\r\n").Append(evt.ResponseString);
            }
            else
            {
                sb.Append(" Type: FILE ===\r\n").Append(evt?.ResponseBody?.Length).Append(" bytes");
            }
            sb.Append("\r\n=== END ").Append(g).Append(" ===\r\n");
            File.AppendAllText(logFileName!, sb.ToString(), Encoding.UTF8);
        }
        #endregion
    }
}
