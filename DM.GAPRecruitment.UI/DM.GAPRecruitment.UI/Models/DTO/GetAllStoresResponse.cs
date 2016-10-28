
using System.Collections.Generic;


namespace DM.GAPRecruitment.UI.Models.DTO
{
    public class GetAllStoresResponse : IApiResponse
    {
        public List<Store> stores { get; set; }
        public bool success { get; set; }
        public int total_elements { get; set; }



    }
}