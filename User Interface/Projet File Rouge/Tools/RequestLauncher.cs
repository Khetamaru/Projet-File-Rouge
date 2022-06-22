using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Projet_File_Rouge.Tools
{
    class RequestLauncher
    {
        public enum HttpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public string http { get; set; }
        public string port { get; set; }
        public string endPoint { get; set; }
        public HttpVerb httpMethod { get; set; }

        public RequestLauncher()
        {
            http = ConfigurationManager.AppSettings["http"];
            port = ConfigurationManager.AppSettings["port"];
            endPoint = string.Empty;
            httpMethod = HttpVerb.GET;
        }

        private (string, HttpStatusCode) MakeRequest(string json)
        {
            string strResponseValue = string.Empty;
            HttpStatusCode statusCode = 0;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http + port + endPoint);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = httpMethod.ToString();

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            try
            {
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                // grab the response and print it out to the console along with the status code
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    strResponseValue = rdr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                // CatchError(e, json);
            }

            return (strResponseValue, statusCode);
        }

        private (string, HttpStatusCode) MakeRequest()
        {
            string result = string.Empty;
            HttpStatusCode statusCode = 0;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http + port + endPoint);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = httpMethod.ToString();

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";

            // now send it
            try
            {
                // grab the response and print it out to the console along with the status code
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                // CatchError(e);
            }

            return (result, statusCode);
        }

        public (string, HttpStatusCode) GetRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.GET;

            return MakeRequest();
        }

        public (string, HttpStatusCode) PostRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.POST;

            return MakeRequest(json);
        }

        public (string, HttpStatusCode) PutRequest(string requestString, string json)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.PUT;

            return MakeRequest(json);
        }

        public (string, HttpStatusCode) DeleteRequest(string requestString)
        {
            endPoint = requestString;
            httpMethod = HttpVerb.DELETE;

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
