using DM.GAPRecruitment.WebService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.WebService.Models.Responses
{
    public class GetAllStoresResponse : IApiResponse
    {
        public List<StoreDTO> stores { get; set; }
        public bool success { get; set; }
        public int total_elements { get; set; }



    }
}