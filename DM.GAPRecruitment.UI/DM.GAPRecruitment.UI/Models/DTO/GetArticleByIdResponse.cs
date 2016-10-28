

namespace DM.GAPRecruitment.UI.Models.DTO
{
    public class GetArticleByIdResponse :IApiResponse
    {

        public Article article { get; set; }
        public bool success { get; set; }

    }
}