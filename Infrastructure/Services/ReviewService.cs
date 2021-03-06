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

        public async Task<ReviewResponseModel> GetUserReviews(int id)
        {
            var reviews = await _reviewRepository.GetUserReviews(id);

            var userReviews = new ReviewResponseModel { MovieReviews = new List<MovieReviewResponseModel>() };

            foreach (var review in reviews)
            {
                var reviewModel = new MovieReviewResponseModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Name = review.Movie.Title,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                };
                userReviews.UserId = review.UserId;
                userReviews.MovieReviews.Add(reviewModel);
            }

            return userReviews;
        }

        public async Task PostUserReview(ReviewRequestModel requestModel)
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
