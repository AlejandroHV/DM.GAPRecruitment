using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.UI.Controllers.BLL.Util
{
    public class AppKeys
    {

        public static string WebServiceURL {
            get {
                return ConfigurationManager.AppSettings["WebServiceUrl"];
                }
        }

        public static string WebServiceArticleControllerName {
            get
            {
                return "Article";
            }

        }

        public static string WebServiceStoreControllerName
        {
            get
            {
                return "Store";
            }

        }

        public static string WebServiceStoreParameterName
        {
            get
            {
                return "storeId";
            }

        }

        public static string WebServiceArticleParameterName
        {
            get
            {
                return "articleId";
            }

        }

        public static string ContentTypeHeaderName {
            get {

                return "Content-Type";
            }

        }
        public static string ContentTypeHeaderValue
        {
            get
            {

                return "Application/Json";
            }

        }

    }
}