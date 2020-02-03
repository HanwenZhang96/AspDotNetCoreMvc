using AspDotNetCoreMvc.Models;
using AspDotNetCoreMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetCoreMvc.Controllers
{
    public class MovieController: Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;

        public MovieController(IMovieService movieService,
            ICinemaService cinemaService)
        {
            _movieService = movieService;
            _cinemaService = cinemaService;
        }
        public async Task<IActionResult> Index(int cinemaId)
        {
            var cinema = await _cinemaService.GetByIdAsync(cinemaId);
            ViewBag.Title = $"{cinema.Name}上映的电影有：";
            ViewBag.CinemaId = cinemaId;
            return View(await _movieService.GetByCinemaAsync(cinemaId));
        }

        public IActionResult Add(int cinemaId)
        {
            ViewBag.Title = "添加电影";
            return View(new Movie() { CinemaId = cinemaId });
        }

        public IActionResult Edit(int movieId)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddAsync(movie);
            }
            return RedirectToAction("Index", new { cinemaId = movie.CinemaId });
        }
    }
}
