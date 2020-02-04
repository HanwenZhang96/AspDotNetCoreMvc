using System.Threading.Tasks;
using AspDotNetCoreMvc.Models;
using AspDotNetCoreMvc.Services;
using AspDotNetCoreMvc.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspDotNetCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IOptions<ConnectionOption> _options;

        public HomeController(ICinemaService cinemaService
             , IOptions<ConnectionOption> options
            )
        {
            _cinemaService = cinemaService;
             _options = options;
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

        public IActionResult GetSettings()
        {
            return Json(_options.Value.DefaultConnection);
        }
    }
}