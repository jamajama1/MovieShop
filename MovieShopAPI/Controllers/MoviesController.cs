using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // create an api method that shows top 30 movies
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly IGenreService _genreService;

        public MoviesController(IMovieService imovieService, IUserService userService,
                                IReviewService reviewService, IGenreService genreService)
        {
            _movieService = imovieService;
            _userService = userService;
            _reviewService = reviewService;
            _genreService = genreService;
        }



        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll ()
        {
            var movie = await _movieService.GetAll();

            if (movie == null)
            {
                return NotFound("No Movies Found");
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if(movie == null)
            {
                return NotFound($"No Movie Found for {id}");
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _reviewService.GetUserReviews(id);

            if (reviews == null)
            {
                return NotFound($"No Movie Found for {id}");
            }

            return Ok(reviews);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTop30RatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();

            // JSON data and Https status code
            if (!movies.Any())
                //404
                return NotFound("No Movies Found");

            return Ok(movies);

            // for converting c# objects to json objects there are 2 ways
            // before .Net Core, we use NewSoft.JSON library
            // since .Net Core, Microsoft created its own JSON serialization library
            // System.Text.Json, preferred method
        }

        [HttpGet]
        //attribute based routing
        // ex. local/api/movies/topmovies
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30RevenueMovies();

            // JSON data and Https status code
            if (!movies.Any())
                //404
                return NotFound("No Movies Found");

            return Ok(movies);

            // for converting c# objects to json objects there are 2 ways
            // before .Net Core, we use NewSoft.JSON library
            // since .Net Core, Microsoft created its own JSON serialization library
            // System.Text.Json, preferred method
        }

        [HttpGet]
        [Route("genre")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetGenres();
            return Ok(genres);
        }

        [HttpGet]
        [Route("genre/{id:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movies = await _movieService.GetMoviesByGenre(id);

            // JSON data and Https status code
            if (!movies.Any())
                //404
                return NotFound("No Movies Found");

            return Ok(movies);

            // for converting c# objects to json objects there are 2 ways
            // before .Net Core, we use NewSoft.JSON library
            // since .Net Core, Microsoft created its own JSON serialization library
            // System.Text.Json, preferred method
        }
    }
}
