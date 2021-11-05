using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        //test@abc.com
        //abc123
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IMovieRepository movieRepository, 
                            IReviewRepository reviewRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }

        private string GetSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashedpassword from databse for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null) throw null;

            // hash the user entered password with salt from the database

            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            // check the hashedpassword with database hashed password
            if (hashedPassword == dbUser.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }

            return null;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };

            await _favoriteRepository.Add(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.GetUserFavorites(id); 
            var favoriteResponse = new FavoriteResponseModel
            {
                UserId = id,
                FavoriteMovies = new List<MovieCardResponseModel>()
            };
            

            var favoriteCards = new MovieCardResponseModel();

            favoriteResponse.FavoriteMovies = favorites.Select(f => new MovieCardResponseModel
              {
                  Id = id,
                  PosterUrl = f.Movie.PosterUrl,
                  Title = f.Movie.Title
            }).ToList();

            return favoriteResponse;
        }
        public async Task<PurchaseDetailsResponseModel> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {

            var purchase = new Purchase
            {
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = (Guid)(purchaseRequest.PurchaseNumber),
                UserId = userId
            };

            var addPurchase = await _purchaseRepository.Add(purchase);
            var movie = await _movieRepository.GetMovieById(purchaseRequest.MovieId);
            var purchaseRespone = new PurchaseDetailsResponseModel
            {
                Title = movie.Title,
                PurchaseDateTime = addPurchase.PurchaseDateTime,
                ReleaseDate = (DateTime)movie.ReleaseDate,
                PosterUrl = movie.PosterUrl,
                PurchaseNumber = addPurchase.PurchaseNumber,
                TotalPrice = (decimal)movie.Price,
            };

            return purchaseRespone;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var isFound = await _purchaseRepository.CheckPurchaseByMovieId(purchaseRequest.MovieId);

            if (isFound == null)
            {
                return false;
            }

            return true;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var Review = new Review
            {
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
                UserId = reviewRequest.UserId
            };

            var addReview = await _reviewRepository.Add(Review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
                UserId = reviewRequest.UserId
            };

            var updateReview = await _reviewRepository.Update(review);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            /*            var review = _reviewRepository.Get(r => r.UserId == userId && r.MovieId == movieId);
                        var addReview = await _reviewRepository.Delete(review);*/
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var reviews = await _reviewRepository.GetUserReviews(id);
            //var movieReviews = new MovieReviewResponseModel();
            //var reviewList = new List<MovieReviewResponseModel>();

            var reviewResponse = new ReviewResponseModel
            {

                MovieReviews = new List<MovieReviewResponseModel>(),
                UserId = id
            };

            reviewResponse.MovieReviews = reviews.Select(r => new MovieReviewResponseModel
            {

                UserId = id,
                MovieId = r.MovieId,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CardUrl = r.Movie.PosterUrl
            }).ToList();
         
            return reviewResponse;
        }
    }
}
