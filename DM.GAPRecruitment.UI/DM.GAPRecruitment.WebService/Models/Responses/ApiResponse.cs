using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.WebService.Models.Responses
{
    public interface IApiResponse {


        bool success { get; set; }

    }


    public class ApiResponse :IApiResponse
    {
        public string Message { get; set; }

        public dynamic Data { get; set; }

        public bool success { get; set; }

        public Exception Error { get; set; }


    }
}