using CatBase.DTOs;
using CatBase.Models;

namespace CatBase.Interface
{
    public interface ICatService
    {
        Task<CatDTO> GetCatsAsync(string searchString = null, int page = 1, int pageSize = 10);
        Task<string> GetCatByIdAsync(Guid id);
        Task<List<string>> GetCatBreedsAsync(); 
        Task CreateCatAsync(CreateCatDTO createCatDTO);
        Task<bool> DeleteCatAsync(Guid id);

    }
}
