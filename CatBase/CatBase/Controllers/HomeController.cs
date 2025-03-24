using Microsoft.AspNetCore.Mvc;
using CatBase.Servises;
using CatBase.Interface;

namespace CatBase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        public HomeController (IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var homeInfo = _homeService.GetHomeInfo();
            var randomFacts = await _homeService.GetRandomFactsAsync();

            ViewBag.RandomFacts = randomFacts;
            return View(homeInfo);
        }

        

    }
}
