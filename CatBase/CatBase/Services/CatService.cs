using CatBase.Models;
using CatBase.DTOs;
using CatBase.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using CatBase.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace CatBase.Servises
{
    public class CatService : ICatService
    {
        public readonly AppDBContext _appDBContext;
        private readonly HttpClient _httpClient;

        public CatService(AppDBContext appDBContext, HttpClient httpClient)
        {
            _appDBContext = appDBContext;
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetCatBreedsAsync()
        {
            string apiUrl = "https://catfact.ninja/breeds";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var breedsResponse = JsonSerializer
                        .Deserialize<BreedsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return breedsResponse?.Data.Select(b => b.Breed).ToList() ?? new List<string>();

                }
                return new List<string> { "Error!" };
            }
            catch (Exception ex)
            {
                return new List<string> { $"Ошибка: {ex.Message}" };
            }
        }

        private class BreedsResponse
        {
            public List<BreedItem> Data { get; set; }
        }

        private class BreedItem
        {
            public string Breed { get; set; }
        }
        public async Task<CatDTO> GetCatsAsync(string searchString = null, int page = 1, int pageSize = 10)
        {
            if (page < 1)
            {
                page = 1;
            }
            var catsQuery = _appDBContext.Cats
                .SearchCat(searchString); 

            var catsCount = await catsQuery.CountAsync();
            var paginatedCat = await catsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            int startCat = (page - 1) * pageSize;

            int endCat = Math.Min(startCat + pageSize - 1, startCat);
            var modelCat = new CatDTO
            {
                Cats = paginatedCat,
                CurrentPage = page,
                TotalItems = catsCount,
                PageSize = pageSize,
                SearchTerm = searchString,
                TotalPages = (int)Math.Ceiling(catsCount / (double)pageSize)
            };

            return modelCat;
        }

        public async Task<string> GetCatByIdAsync(Guid id)
        {
            var cat = await _appDBContext.Cats
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (cat != null)
            {
                return cat.Breeds;
            }
            else
            {
                return null;
            }
        }
       
        public async Task CreateCatAsync(CreateCatDTO createCatDTO)
        {
            var cat = new Cat
            {
                Id = Guid.NewGuid(),
                CatsName = createCatDTO.CatsName,
                Age = createCatDTO.Age,
                Breeds = createCatDTO.Breeds,
                Gender = createCatDTO.Gender,
                Img = createCatDTO.Img
            };
            _appDBContext.Cats.Add(cat);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteCatAsync(Guid id)
        {
            
            var cat = await _appDBContext.Cats.FindAsync(id);
            if (cat == null)
            {
                return false;
            }
            _appDBContext.Cats.Remove(cat);
            await _appDBContext.SaveChangesAsync(); 

            var deleteCatDTO = new DeleteCatDTO
            {
                Id = cat.Id,
                CatsName = cat.CatsName
            };
            
            return true;
        }

    }
}
