using DM.GAPRecruitment.UI.Controllers.BLL.Util;
using DM.GAPRecruitment.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using DM.GAPRecruitment.WebService.Models.DTO;

namespace DM.GAPRecruitment.UI.Controllers.BLL
{
    public class ArticleBLL
    {

        public static List<Article> GetAllArticles(ISystemFail error)
        {
            List<Article> articles = null;
            try
            {

                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceArticleControllerName);
                string response = Proxy.ProxyService.GetRequestURlConcatParameters(webServiceUrl,error);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    GetAllArticleResponse apiResponse = JsonConvert.DeserializeObject<GetAllArticleResponse>(response);
                    if (apiResponse.success)
                    {
                        articles = apiResponse.articles;
                        
                        
                    }
                    else {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred obtaining the list of articles.");
                    }
                }
                else {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred obtaining the list of articles. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred obtaining the list of articles. Error:", ex.Message);
            }
            return articles;
        }

        public static List<Article> GetStoreArticles(int storeId, ISystemFail error) {

            List<Article> articles = null;
            try
            {

                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceArticleControllerName);
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("id",storeId.ToString());
                parameters.Add("dummy","dummy");
                string response = Proxy.ProxyService.GetRequestURlConcatParameters(webServiceUrl, error,parameters);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    GetAllArticleResponse apiResponse = JsonConvert.DeserializeObject<GetAllArticleResponse>(response);
                    if (apiResponse.success)
                    {
                        articles = apiResponse.articles;


                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred obtaining the list of articles of the store.");
                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred obtaining the list of articles of the store. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred obtaining the list of articles of the store. Error:", ex.Message);
            }
            return articles;
        }


        public static Article GetArticleById(int articleId,ISystemFail error)
        {
            Article article = null;
            try
            {

                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceArticleControllerName);
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("articleId", articleId.ToString());
                

                string response = Proxy.ProxyService.GetRequestURlConcatParameters(webServiceUrl, error,parameters);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    GetArticleByIdResponse apiResponse = JsonConvert.DeserializeObject<GetArticleByIdResponse>(response);
                    if (apiResponse.success)
                    {
                        article = apiResponse.article;
                        
                    }
                    else {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred obtaining the specific article");

                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred obtaining the specific article. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred obtaining the specific article. Error:", ex.Message);
            }
            return article;
        }

        public static void CreateArticle(Article article, ISystemFail error) {

            try
            {
                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceArticleControllerName);
                Dictionary<string, string> headers = new Dictionary<string, string>();
                //Content Type Header
                headers.Add(AppKeys.ContentTypeHeaderName, AppKeys.ContentTypeHeaderValue);
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("dummy", "dummy");

                string response = Proxy.ProxyService.PostRequest(webServiceUrl, error, parameters, headers,article);
                if (!string.IsNullOrEmpty(response) && !error.Error)
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (apiResponse != null && apiResponse.success)
                    {
                        error.Message = "The article was succesfully created";
                    }
                    else {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred creating the article. Error:", apiResponse.Message);
                    }
                }
                else
                {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred creating the article. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred creating the article. Error:", ex.Message);
            }
           

        }

        public static void UpdateArticle(Article article, ISystemFail error)
        {
            try
            {
                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceArticleControllerName);

               string response = Proxy.ProxyService.PutRequest(webServiceUrl, error, article);
                if(!string.IsNullOrEmpty( response) && !error.Error)
                {
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (apiResponse != null && apiResponse.success)
                    {
                        error.Message = "The article was succesfully updated";
                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred updating the Article. Error:", apiResponse.Message);
                    }
                }
                else{
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred updating the Article. Error:", error.Message);

                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred updating the Article. Error:", ex.Message);
            }

        }

        public static void DeleteArticle(int articleId, ISystemFail error) {

            try
            {
                string response = string.Empty;
                string webServiceUrl = string.Concat(AppKeys.WebServiceURL, AppKeys.WebServiceArticleControllerName);
               
                Dictionary<string, string> headers = new Dictionary<string, string>();
                //Content Type Header
                headers.Add(AppKeys.ContentTypeHeaderName, AppKeys.ContentTypeHeaderValue);
                Article article = GetArticleById(articleId,error);

                if (article != null && !error.Error)
                {
                    response = BLL.Proxy.ProxyService.DeleteRequest(webServiceUrl, error, null, article);

                    
                    if (!string.IsNullOrEmpty(response) && !error.Error)
                    {
                        ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                        if (apiResponse != null && apiResponse.success)
                        {
                            error.Error = false;
                            error.Message = "The article was succesfully deleted";
                        }
                        else
                        {

                            error.Error = true;
                            error.Message = string.Concat("An error has ocurred deleting the article. Error", apiResponse.Message);
                        }
                    }
                    else
                    {
                        error.Error = true;
                        error.Message = string.Concat("An error has ocurred deleting the article. Error:", error.Message);
                    }
                }
                else {
                    error.Error = true;
                    error.Message = string.Concat("An error has ocurred deleting the article. Error:", error.Message);
                }

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = string.Concat("An error has ocurred deleting the article. Error:", ex.Message);
            }

        }

    }
}