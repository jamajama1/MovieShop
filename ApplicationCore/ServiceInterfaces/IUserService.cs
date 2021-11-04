using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);
        public Task<PurchaseDetailsResponseModel> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);

        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);

        public Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);


        public Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId);


        public  Task AddMovieReview(ReviewRequestModel reviewRequest);


        public Task UpdateMovieReview(ReviewRequestModel reviewRequest);


        public Task DeleteMovieReview(int userId, int movieId);


        public Task<ReviewResponseModel> GetAllReviewsByUser(int id);
    }
}
