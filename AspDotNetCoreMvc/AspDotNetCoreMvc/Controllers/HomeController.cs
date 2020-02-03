using System.Threading.Tasks;
using AspDotNetCoreMvc.Models;
using AspDotNetCoreMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICinemaService _cinemaService;
        public HomeController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "电影院";

            return View(await _cinemaService.GetllAllAsync());
        }

        public IActionResult Add()
        {
            ViewBag.Title = "添加电影院";

            return View(new Cinema());
        }

        public IActionResult Edit(int cinemaId)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Cinema model)
        {
            if (ModelState.IsValid)
            {
                await _cinemaService.AddAsync(model);
            }
            // 跳转至当前controller下的action
            return RedirectToAction("Index");
        }
    }
}