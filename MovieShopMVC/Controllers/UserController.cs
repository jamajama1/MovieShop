using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    // All the action methods in User controller should only work for authenticated users
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            // purchase movie when user clicks on buy button in Movie Details page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            // Favorite movie when user clicks on favorite button in Movie Details page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // Review movie when user clicks on review button in Movie Details page
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // get all the movies purchased by user => List<MovieCard>
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            // get all the movies favored by user
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            // get all the movie reviews by user
            return View();
        }


    }
}
