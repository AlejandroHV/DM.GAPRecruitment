

namespace DM.GAPRecruitment.UI.Models.DTO
{
    public class GetStoreByIdResponse :IApiResponse
    {

        public Store store { get; set; }

        public bool success { get; set; }
    }
}