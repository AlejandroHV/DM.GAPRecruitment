
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DM.GAPRecruitment.UI.Controllers.BLL.Util;

namespace DM.GAPRecruitment.UI.Controllers.BLL.Proxy
{
    

    public class ProxyService 
    {

        public static string GetRequestURlConcatParameters(string serviceURL, ISystemFail error, Dictionary<string, string> parametersDictionary = null, Dictionary<string, string> requestAditionalHeaders = null)
        {
            string response = null;
            string parameters = string.Empty;
            if (parametersDictionary != null && parametersDictionary.Count > 0)
            {
                parameters = "?";
                int counter = 0;
                foreach (var item in parametersDictionary)
                {

                    if (counter == (parametersDictionary.Count - 1))
                        parameters += string.Format("{0}={1}", item.Key, item.Value);
                    else
                        parameters += string.Format("{0}={1}&", item.Key, item.Value);

                    counter++;

                }
            }
            string completeURL = string.Format("{0}{1}", serviceURL, parameters);
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    
                    if (requestAditionalHeaders != null && requestAditionalHeaders.Count > 0)
                    {
                        foreach (var item in requestAditionalHeaders)
                        {
                            client.Headers.Add(item.Key, item.Value);
                        }
                    }
                    Stream stream = client.OpenRead(completeURL);
                    StreamReader reader = new StreamReader(stream);
                    response = reader.ReadToEnd();
                }
            }
            catch (WebException webEx)
            {
                var listErrors = webEx.Message;
                var message = string.Join("; ", listErrors);
                error.Error = true;
                error.Message = message;
                error.Exception = webEx;
                if (webEx.Response is HttpWebResponse)
                {
                    switch (((HttpWebResponse)webEx.Response).StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            response = null;
                            error.Message+= string.Concat( "Error en realizar la petición", "URL not found");
                            break;
                        case HttpStatusCode.Forbidden:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Access to the URL not allowed");
                            break;
                        default:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Exception ocurred while executing the Get request");
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Message = ex.Message;
                error.Exception = ex;
                response = null;
            }
            return response;
        }

        public byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static string PostRequest(string requestURL, ISystemFail error, Dictionary<string, string> parametersDictionary = null, Dictionary<string, string> requestAditionalHeaders = null, object requestBody = null)
        {
            string response = null;
            string parameters = string.Empty;
            if (parametersDictionary != null && parametersDictionary.Count > 0)
            {
                parameters = "?";
                int counter = 0;
                foreach (var item in parametersDictionary)
                {
                    if (counter == (parametersDictionary.Count - 1))
                        parameters += string.Format("{0}={1}", item.Key, item.Value);
                    else
                        parameters += string.Format("{0}={1}&", item.Key, item.Value);

                    counter++;
                }
            }
            string completeURL = string.Concat(requestURL, parameters);
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    // client.Headers["Content-Type"] = "application/json";
                    client.Headers.Add("Accept-Language", " en-US");
                    //client.Headers.Add("Accept", " text/html, application/xhtml+xml, */*");
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
                    if (requestAditionalHeaders != null && requestAditionalHeaders.Count > 0)
                    {
                        foreach (var item in requestAditionalHeaders)
                        {
                            client.Headers.Add(item.Key, item.Value);
                        }
                    }
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] byteArrayResponse = new byte[0];
                    if (requestBody != null)
                    {
                        byte[] requestBodyBites = encoding.GetBytes(JsonConvert.SerializeObject(requestBody));
                        byteArrayResponse = client.UploadData(completeURL, "POST", requestBodyBites);
                    }
                    else
                    {
                        NameValueCollection val = new NameValueCollection();

                        foreach (var pair in parametersDictionary)
                            val.Add(pair.Key, pair.Value.ToString());

                        byteArrayResponse = client.UploadValues(requestURL, "POST", val);
                    }
                    response = System.Text.Encoding.UTF8.GetString(byteArrayResponse);
                }
            }
            catch (WebException webEx)
            {
                var listErrors = webEx.Message;
                var message = string.Join("; ", listErrors);
                error.Error = true;
                error.Message = message;
                error.Exception = webEx;
                if (webEx.Response is HttpWebResponse)
                {
                    switch (((HttpWebResponse)webEx.Response).StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "URL not found");
                            break;
                        case HttpStatusCode.Forbidden:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Access to the URL not allowed");
                            break;
                        default:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Exception ocurred while executing the Post request to the service");
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Message = ex.Message;
                error.Exception = ex;
                response = null;
            }

            return response;
        }

        public static string PostFile(string requestURL, ISystemFail error, string filePath, Dictionary<string, string> parametersDictionary = null, Dictionary<string, string> requestAditionalHeaders = null)
        {

            string response = null;
            string parameters = string.Empty;
            if (parametersDictionary != null && parametersDictionary.Count > 0)
            {
                parameters = "?";
                int counter = 0;
                foreach (var item in parametersDictionary)
                {
                    if (counter == (parametersDictionary.Count - 1))
                        parameters += string.Format("{0}={1}", item.Key, item.Value);
                    else
                        parameters += string.Format("{0}={1}&", item.Key, item.Value);

                    counter++;
                }
            }
            string completeURL = string.Concat(requestURL, parameters);
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Headers.Add("Accept-Language", " en-US");
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
                    if (requestAditionalHeaders != null && requestAditionalHeaders.Count > 0)
                    {
                        foreach (var item in requestAditionalHeaders)
                        {
                            client.Headers.Add(item.Key, item.Value);
                        }
                    }
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] byteArrayResponse = new byte[0];
                    byteArrayResponse = client.UploadFile(completeURL, "POST", filePath);

                    response = System.Text.Encoding.UTF8.GetString(byteArrayResponse);
                }
            }
            catch (WebException webEx)
            {
                var listErrors = webEx.Message;
                var message = string.Join("; ", listErrors);
                error.Error = true;
                error.Message = message;
                error.Exception = webEx;
                if (webEx.Response is HttpWebResponse)
                {
                    switch (((HttpWebResponse)webEx.Response).StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "URL not found");
                            break;
                        case HttpStatusCode.Forbidden:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Access to the URL not allowed");
                            break;
                        default:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Exception ocurred while executing the Post request to the service");
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Message = ex.Message;
                error.Exception = ex;
                response = null;
            }

            return response;
        }

        public static string PutRequest(string requestURL, ISystemFail error, object requestBody, Dictionary<string, string> parametersDictionary = null)
        {
            string response = null;
            string parameters = string.Empty;
            if (parametersDictionary != null && parametersDictionary.Count > 0)
            {
                parameters = "?";
                int counter = 0;
                foreach (var item in parametersDictionary)
                {
                    if (counter == (parametersDictionary.Count - 1))
                        parameters += string.Format("{0} = {1}", item.Key, item.Value);
                    else
                        parameters += string.Format("{0} = {1},", item.Key, item.Value);
                    counter++;
                }
            }


            string completeURL = string.Concat(requestURL, parameters);

            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Headers["Content-Type"] = "application/json";
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] requestBodyBites = encoding.GetBytes(JsonConvert.SerializeObject(requestBody));
                    byte[] byteArrayResponse = client.UploadData(completeURL, "PUT", requestBodyBites);
                    response = System.Text.Encoding.UTF8.GetString(byteArrayResponse);
                }
            }
            catch (WebException webEx)
            {
                var listErrors = webEx.Message;
                var message = string.Join("; ", listErrors);
                error.Error = true;
                error.Message = message;
                error.Exception = webEx;

                if (webEx.Response is HttpWebResponse)
                {
                    switch (((HttpWebResponse)webEx.Response).StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "URL not found");
                            break;
                        case HttpStatusCode.Forbidden:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Access to the URL not allowed");
                            break;
                        default:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Exception ocurred while executing the Post request to the service");
                            break;

                    }
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Message = ex.Message;
                error.Exception = ex;
                response = null;
            }

            return response;
        }

        public static string DeleteRequest(string requestURL, ISystemFail error, Dictionary<string, string> parametersDictionary = null, object requestBody = null)
        {
            string response = null;
            string parameters = string.Empty;
            if (parametersDictionary != null && parametersDictionary.Count > 0)
            {
                parameters = "?";
                int counter = 0;
                foreach (var item in parametersDictionary)
                {
                    if (counter == (parametersDictionary.Count - 1))
                        parameters += string.Format("{0} = {1}", item.Key, item.Value);
                    else
                        parameters += string.Format("{0} = {1},", item.Key, item.Value);
                    counter++;
                }
            }


            string completeURL = string.Concat(requestURL, parameters);

            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Headers["Content-Type"] = "application/json";
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] requestBodyBites = encoding.GetBytes(JsonConvert.SerializeObject(requestBody));
                    byte[] byteArrayResponse = client.UploadData(completeURL, "DELETE", requestBodyBites);
                    response = System.Text.Encoding.UTF8.GetString(byteArrayResponse);
                }
            }
            catch (WebException webEx)
            {
                var listErrors = webEx.Message;
                var message = string.Join("; ", listErrors);
                error.Error = true;
                error.Message = message;
                error.Exception = webEx;

                if (webEx.Response is HttpWebResponse)
                {
                    switch (((HttpWebResponse)webEx.Response).StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "URL not found");
                            break;
                        case HttpStatusCode.Forbidden:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Access to the URL not allowed");
                            break;
                        default:
                            response = null;
                            error.Message += string.Concat("Error en realizar la petición", "Exception ocurred while executing the Post request to the service");
                            break;

                    }
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Message = ex.Message;
                error.Exception = ex;
                response = null;
            }

            return response;
        }
        
    }


}
