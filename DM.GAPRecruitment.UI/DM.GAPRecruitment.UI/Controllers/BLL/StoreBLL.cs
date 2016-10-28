using DM.GAPRecruitment.UI.Controllers.BLL.Util;
using DM.GAPRecruitment.UI.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.UI.Controllers.BLL
{
    public class StoreBLL
    {
        public static List<Store> GetAllStores(ISystemFail error)
        {
            List<Store> stores = null;
            try
            {

                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceStoreControllerName);
                string response = Proxy.ProxyService.GetRequestURlConcatParameters(webServiceUrl, error);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    GetAllStoresResponse apiResponse = JsonConvert.DeserializeObject<GetAllStoresResponse>(response);
                    if (apiResponse.success)
                    {
                        stores = apiResponse.stores;
                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred obtaining the list of stores.");
                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred obtaining the list of stores. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred obtaining the list of stores.. Error:", ex.Message);
            }
            return stores;
        }
        
        public static Store GetStoreById(int storeId, ISystemFail error)
        {
            Store store = null;
            try
            {

                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceStoreControllerName);
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("id", storeId.ToString());
                
                string response = Proxy.ProxyService.GetRequestURlConcatParameters(webServiceUrl, error, parameters);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    GetStoreByIdResponse apiResponse = JsonConvert.DeserializeObject<GetStoreByIdResponse>(response);
                    if (apiResponse.success)
                    {
                        store = apiResponse.store;

                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred obtaining the specific store");

                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred obtaining the specific store. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred obtaining the specific store. Error:", ex.Message);
            }
            return store;
        }

        public static void CreateStore(Store store, ISystemFail error)
        {

            try
            {
                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys. WebServiceStoreControllerName);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                //Content Type Header
                headers.Add(AppKeys.ContentTypeHeaderName, AppKeys.ContentTypeHeaderValue);
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("dummy", "dummy");

                string response = Proxy.ProxyService.PostRequest(webServiceUrl, error, parameters, headers, store);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (apiResponse != null && apiResponse.success)
                    {
                        error.Message = "The store was succesfully created.";
                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred creating the store. Error:", apiResponse.Message);
                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred creating the store. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred creating the store. Error:", ex.Message);
            }


        }

        public static void UpdateStore(Store store, ISystemFail error)
        {
            try
            {
                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceStoreControllerName);

                string response = Proxy.ProxyService.PutRequest(webServiceUrl, error, store);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (apiResponse != null && apiResponse.success)
                    {
                        error.Message = "The store was succesfully updated";
                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred updating the store. Error:", apiResponse.Message);
                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred updating the store. Error:", error.Message);

                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred updating the store. Error:", ex.Message);
            }

        }

        public static void DeleteStore(int storeId, ISystemFail error)
        {

            try
            {
                string response = string.Empty;
                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceStoreControllerName);

                Dictionary<string, string> headers = new Dictionary<string, string>();
                //Content Type Header
                headers.Add(AppKeys.ContentTypeHeaderName, AppKeys.ContentTypeHeaderValue);
                Store article = GetStoreById(storeId, error);
                if (article != null && !error.Error)
                {
                    response = BLL.Proxy.ProxyService.DeleteRequest(webServiceUrl, error, null, article);
                }
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (apiResponse != null && apiResponse.success)
                    {
                        error.Error = false;
                        error.Message = "The store was succesfully eliminated.";
                    }
                    else
                    {

                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred eliminating the store. Error:", apiResponse.Message);
                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred eliminating the store. Error:", error.Message);
                }


            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred eliminating the store. Error:", ex.Message);
            }

        }


    }
}