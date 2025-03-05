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
        public IActionResult Index()
        {
            var homeInfo =  _homeService.GetHomeInfo();
            return View(homeInfo);
        }
    }
}
