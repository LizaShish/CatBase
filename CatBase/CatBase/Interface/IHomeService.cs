using CatBase.DTOs;

namespace CatBase.Interface
{
    public interface IHomeService
    {
        HomeInfoDTO GetHomeInfo();

        Task<List<string>> GetRandomFactsAsync();
    }
}
