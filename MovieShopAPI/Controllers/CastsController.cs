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
    public class CastsController : ControllerBase
    {
        MovieCastService _movieCastService;

        public CastsController(MovieCastService movieCastService)
        {
            _movieCastService = movieCastService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieByCastId(int id)
        {
            var movie = _movieCastService.GetMovieByCastId(id);

            return Ok(movie);
        }
    }
}
