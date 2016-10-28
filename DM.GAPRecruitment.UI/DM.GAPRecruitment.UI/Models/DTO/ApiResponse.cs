using System;

namespace DM.GAPRecruitment.UI.Models.DTO
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