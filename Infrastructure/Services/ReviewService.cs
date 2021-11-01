using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReviewService: IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetUserReviews(int id)
        {
            var reviews = await _reviewRepository.Get(r => r.UserId == id);
            return reviews;
        }

        public async Task PostUserReview(UserReviewRequestModel requestModel)
        {
            var review = new Review
            {
                UserId = requestModel.UserId,
                MovieId = requestModel.MovieId,
                Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };

            var newReview = await _reviewRepository.Add(review);
        }
    }
}
