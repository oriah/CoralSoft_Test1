using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;

namespace TestProject1.Web.Helpers
{
    public enum HttpMethods
    {
        [Description("GET")]
        Get = 0,
        [Description("PUT")]
        Put = 1,
        [Description("DELETE")]
        Delete = 2,
        [Description("POST")]
        Post = 3,
        [Description("HEAD")]
        Head = 4,
        [Description("TRACE")]
        Trace = 5,
        [Description("PATCH")]
        Patch = 6,
        [Description("CONNECT")]
        Connect = 7,
        [Description("OPTIONS")]
        Options = 8,
        [Description("CUSTOM")]
        Custom = 9,
        [Description("NONE")]
        None = 255,
    }
    public static class WebAPIClient
    {
        public static TResult WebApiGet<TResult>(string url, StringDictionary pParams = null)
        {
            return WebApiSend<TResult>("GET", url, pParams);
        }

        public static TResult WebApiPost<TResult>(string url, StringDictionary pParams = null)
        {
            return WebApiSend<TResult>("POST", url, pParams);
        }
        public static TResult WebApiPut<TResult>(string url, StringDictionary pParams = null)
        {
            return WebApiSend<TResult>("PUT", url, pParams);
        }
        public static TResult WebApiDelete<TResult>(string url, StringDictionary pParams = null)
        {
            return WebApiSend<TResult>("DELETE", url, pParams);
        }


        /// <summary>
        /// Sends the given web api request.
        /// in case of get: sends as query string params
        /// in case of post/put/delete: sends as a json body
        /// optionally receives json/regular response.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public static TResult WebApiSend<TResult>(string httpMethod, string url, StringDictionary pParams = null, bool isJsonResponse = true)
        {
            string urlParameters = pParams?.ToJson() ?? "";

            //var handler = new WinHttpHandler();
            //using (HttpClient client = new HttpClient(handler, true))
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage msg = new HttpRequestMessage();
                msg.Method = new HttpMethod(httpMethod);
                msg.RequestUri = new Uri(httpMethod != "GET" || pParams == null ? url : BuildUriWithParams(url, pParams));
                if (pParams != null && httpMethod != "GET")
                    msg.Content = new StringContent(urlParameters, Encoding.UTF8, "application/json");   // This is where your content gets added to the request body

                if (isJsonResponse)
                {
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                // List data response.
                HttpResponseMessage response = client.SendAsync(msg).Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    TResult dataObject;
                    if (isJsonResponse)
                    {
                        dataObject = response.Content.ReadAsAsync<TResult>().Result; //Make sure to add a reference to System.Net.Http.Formatting.dll
                    }
                    else
                    {
                        dataObject = (TResult)(object)response.Content.ReadAsStringAsync().Result; //Make sure to add a reference to System.Net.Http.Formatting.dll
                    }

                    return dataObject;
                }
                else
                {
                    string errMsg = $@"API error:::statusCode={(int)response.StatusCode}:::reason={response.ReasonPhrase}";
                    throw new Exception(errMsg);
                }

            }
        }


        /// <summary>
        /// Sends the given web api request.
        /// sends the parameters as a json body
        /// no-response overload.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="pParams"></param>
        public static void WebApiSend(string httpMethod, string url, StringDictionary pParams = null)
        {
            string urlParameters = pParams?.ToJson() ?? "";

            //var handler = new WinHttpHandler();
            //using (HttpClient client = new HttpClient(handler, true))
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage msg = new HttpRequestMessage();
                msg.Method = new HttpMethod(httpMethod);
                msg.RequestUri = new Uri(httpMethod != "GET" || pParams == null ? url : BuildUriWithParams(url, pParams));
                if (pParams != null && httpMethod != "GET")
                    msg.Content = new StringContent(urlParameters, Encoding.UTF8, "application/json");   // This is where your content gets added to the request body

                //// Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //  new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.SendAsync(msg).Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (!response.IsSuccessStatusCode)
                {
                    string errMsg = $@"API error:::statusCode={(int)response.StatusCode}:::reason={response.ReasonPhrase}";
                    throw new Exception(errMsg);
                }

            }
        }

        private static string BuildUriWithParams(string url, StringDictionary pParams)    //helper
        {
            var builder = new UriBuilder(url);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(string.Empty);
            pParams.ToList().ForEach(kp => query[kp.Key] = kp.Value);
            string queryString = query.ToString();
            builder.Query = queryString;
            return builder.ToString();

        }
    }
}
