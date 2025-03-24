using CatBase.DTOs;
using CatBase.Interface;
using System.Net.Http;
using System.Text.Json;

namespace CatBase.Servises
{
    public class HomeService: IHomeService
    {
        private readonly HttpClient _httpClient;

        public HomeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
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
        public async Task<List<string>> GetRandomFactsAsync()
        {
            var facts = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                string fact = await GetOneRandomFactAsync();
                facts.Add(fact);
            }

            return facts;
        }
        private async Task<string> GetOneRandomFactAsync()
        {
            string apiUrl = "https://catfact.ninja/fact";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var factResponse = JsonSerializer
                        .Deserialize<CatFactResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return factResponse?.Fact ?? "Факт не найден.";
                }

                return "Ошибка при получении факта.";
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }
        private class CatFactResponse
        {
            public string Fact { get; set; }
        }
        private class FactsItem
        {
            public string Fact { get; set; }
        }
    }
}
