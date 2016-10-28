
using System.Collections.Generic;


namespace DM.GAPRecruitment.UI.Models.DTO
{
    public class GetAllArticleResponse :IApiResponse
    {

        public List<Article> articles { get;set;}
        public bool success { get; set; }
        public int total_elements { get; set; }

    }
}