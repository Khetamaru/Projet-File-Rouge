using Projet_File_Rouge.Object;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Projet_File_Rouge.Tools
{
    /// <summary>
    /// Launch requests to the server
    /// </summary>
    class RequestLauncher
    {

        public string http { get; set; }
        public string port { get; set; }
        public string endPoint { get; set; }
        public HttpMethod httpMethod { get; set; }

        public RequestLauncher()
        {
            Settings Settings = new Settings();
            http = !Settings.testMode ? Settings.httpUrl : "http://localhost:";
            port = Settings.httpPort;
            endPoint = string.Empty;
            httpMethod = HttpMethod.Get;
        }

        /// <summary>
        /// Launch with a JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private (string, HttpStatusCode) MakeRequestAsync(string json)
        {
            //Do not instantiate httpclient like that, use dependency injection instead
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(http + port + endPoint);
            HttpResponseMessage response = null;

            //Set the headers of the client
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, http + port + endPoint);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            //A memory stream which is a temporary buffer that holds the payload of the request
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    //Write to the memory stream
                    memoryStream.Write(Encoding.UTF8.GetBytes(json), 0, Encoding.UTF8.GetBytes(json).Length);

                    //A stream content that represent the actual request stream
                    using (var stream = new StreamContent(memoryStream))
                    {
                        //Send the request
                        response = httpClient.SendAsync(httpRequest).Result;

                        //Ensure we got success response from the server
                        response.EnsureSuccessStatusCode();
                        //you can access the response like that
                        //response.Content
                    }
                }
                return (response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
            catch (Exception)
            {
                return ("", HttpStatusCode.GatewayTimeout);
            }
        }

        /// <summary>
        /// Launch without JSON
        /// </summary>
        /// <returns></returns>
        private (string, HttpStatusCode) MakeRequest()
        {
            //Do not instantiate httpclient like that, use dependency injection instead
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(http + port + endPoint);
            HttpResponseMessage response = null;

            //Set the headers of the client
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, http + port + endPoint);
            httpRequest.Content = new StringContent(String.Empty, Encoding.UTF8, "application/json");

            //A memory stream which is a temporary buffer that holds the payload of the request
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    //A stream content that represent the actual request stream
                    using (var stream = new StreamContent(memoryStream))
                    {
                        //Send the request
                        response = httpClient.SendAsync(httpRequest).Result;

                        //Ensure we got success response from the server
                        response.EnsureSuccessStatusCode();
                        //you can access the response like that
                        //response.Content
                    }
                }
                return (response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
            catch(Exception)
            {
                return ("", HttpStatusCode.GatewayTimeout);
            }
        }

        public (string, HttpStatusCode) GetRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpMethod.Get;

            return MakeRequest();
        }

        public (string, HttpStatusCode) PostRequestAsync(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpMethod.Post;

            return MakeRequestAsync(json);
        }

        public (string, HttpStatusCode) PutRequestAsync(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpMethod.Put;

            return MakeRequestAsync(json);
        }

        public (string, HttpStatusCode) DeleteRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpMethod.Delete;

            return MakeRequest();
        }

        private void CatchError(Exception e, string json)
        {
            PopUpCenter.MessagePopup("Une erreur a eu lieu pendant la communication entre le programme et le serveur.\n\n" +
                                     http + port + endPoint + "\n\n" +
                                     e.ToString() + "\n\n" +
                                     json);
        }

        private void CatchError(Exception e)
        {
            PopUpCenter.MessagePopup("Une erreur a eu lieu pendant la communication entre le programme et le serveur.\n\n" +
                                     http + port + endPoint + "\n\n" +
                                     e.ToString() + "\n\n");
        }
    }
}
