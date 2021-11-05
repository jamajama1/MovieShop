using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    // All the action methods in User controller should only work for authenticated users
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPurchaseService _purchaseService;
        private readonly IFavoriteService _favoriteService;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        
        public UserController(IPurchaseService purchaseService, IFavoriteService favoriteService, 
                                IReviewService reviewService, IUserService userService, ICurrentUserService currentUserService) 
        {
            _purchaseService = purchaseService;
            _favoriteService = favoriteService;
            _reviewService = reviewService;
            _userService = userService;
            _currentUserService = currentUserService;
        }


        [HttpPost] 
        public async Task<IActionResult> Purchase(PurchaseRequestModel requestModel)
        {
            // purchase movie when user clicks on buy button in Movie Details page

            //pass the user id to the UserService, that will pass to UserRepository

            //var purchase = await _purchaseService.PurchaseMovie(requestModel);
            var purchase = await _userService.PurchaseMovie(requestModel, _currentUserService.UserId);
            
            return View(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteRequestModel requestModel)
        {
            // Favorite movie when user clicks on favorite button in Movie Details page
            requestModel.UserId = _currentUserService.UserId;
            await _favoriteService.AddFavorite(requestModel);
            return View("_Thankyou");
        }

        [HttpPost]
        public async Task<ViewResult> Review(ReviewRequestModel requestModel)
        {
            requestModel.UserId = _currentUserService.UserId;
            await _userService.AddMovieReview(requestModel);
            
            return View("_Thankyou");
        }

        [HttpGet]
        public ViewResult AddReview(ReviewRequestModel requestModel)
        {
            return View("Review");
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // get all the movies purchased by user => List<MovieCard>
            var purchases = await _purchaseService.GetAllPurchases(_currentUserService.UserId);
            if (purchases == null)
            {
                throw new Exception("No purchases found");
            } 
            return View(purchases);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            // get all the movies favored by user
            var favorites = await _userService.GetAllFavoritesForUser(_currentUserService.UserId);
            return View(favorites.FavoriteMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            // get all the movie reviews by user
            var reviews = await _userService.GetAllReviewsByUser(_currentUserService.UserId);
            return View(reviews);
        }
    }
}
