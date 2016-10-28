using DM.GAPRecruitment.WebService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.WebService.Models.Responses
{
    public class GetStoreByIdResponse :IApiResponse
    {

        public StoreDTO store { get; set; }

        public bool success { get; set; }
    }
}