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
                                IReviewService reviewService, IUserService userService) 
        {
            _purchaseService = purchaseService;
            _favoriteService = favoriteService;
            _reviewService = reviewService;
            _userService = userService;
        }

        /*public UserController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public UserController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        public UserController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }*/


        [HttpPost] 
        public async Task<IActionResult> Purchase(UserPurchaseRequestModel requestModel)
        {
            // purchase movie when user clicks on buy button in Movie Details page

            //var userId = _currentUserService.UserId;
            //pass the user id to the UserService, that will pass to UserRepository

            var purchase = _purchaseService.PurchaseMovie(requestModel);
            
            return View(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            // Favorite movie when user clicks on favorite button in Movie Details page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review(UserReviewRequestModel requestModel)
        {
            // Review movie when user clicks on review button in Movie Details page
            var review = _reviewService.PostUserReview(requestModel);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // get all the movies purchased by user => List<MovieCard>
            var purchases = await _purchaseService.GetUserPurchases(_currentUserService.UserId);
            return View(purchases);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            // get all the movies favored by user
            var favorites = await _favoriteService.GetUserFavorites(_currentUserService.UserId);
            return View(favorites);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            // get all the movie reviews by user
            var reviews = await _reviewService.GetUserReviews(_currentUserService.UserId);
            return View(reviews);
        }


    }
}
