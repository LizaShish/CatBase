using CatBase.DTOs;
using CatBase.Interface;

namespace CatBase.Servises
{
    public class HomeService: IHomeService
    {
        public HomeInfoDTO GetHomeInfo()
        {
            var homeInfo = new HomeInfoDTO
            {
                ProjectName = "CatBase Project",
                Description = "CatBase is a simple application to store information about your cats.",
                Features = new List<string>
                {
                    "Add new cats",
                    "Search by breed or name",
                    "Update cat information",
                    "Delete cats"
                }
            };
            return homeInfo;
        }
    }
}
