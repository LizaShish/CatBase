
using CatBase.DTOs;
using CatBase.Interface;
using CatBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CatBase.Controllers
{
    public class CatController : Controller
    {
        private readonly ICatService _catService;
        private readonly AppDBContext _appDBContext; 

        public CatController(ICatService catService, AppDBContext appDBContext)
        {
            _catService = catService;
            _appDBContext = appDBContext; 
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 10)
        {
            var catDTO = await _catService.GetCatsAsync(searchString, page, pageSize);
            ViewBag.PageSize = pageSize;
            return View(catDTO);
            
        }

        [HttpGet]
        public async Task<IActionResult> CreateCat()
        {
            var breeds = await _catService.GetCatBreedsAsync();

            var model = new CreateCatDTO
            {
                BreedsList = breeds.Select(b => new SelectListItem
                {
                    Value = b,
                    Text = b
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCat(CreateCatDTO createCatDTO)
        {
            if (!ModelState.IsValid)
            {
                createCatDTO.BreedsList = (await _catService.GetCatBreedsAsync())
                    .Select(b => new SelectListItem { Value = b, Text = b }).ToList();
                return View(createCatDTO);
            }
            await _catService.CreateCatAsync(createCatDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cat = await _appDBContext.Cats.FindAsync(id); 

            if(cat == null)
            {
                return NotFound();
            }

            var model = new DeleteCatDTO
            {
                Id = cat.Id,
                CatsName = cat.CatsName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCat(Guid id)
        {

            var cat = await _appDBContext.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _appDBContext.Cats.Remove(cat);
            await _appDBContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
