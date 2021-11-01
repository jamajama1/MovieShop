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
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }
        public async Task<List<MovieCardResponseModel>> GetUserFavorites(int id)
        {
            var favoriteList = await _favoriteRepository.Get(f=> f.UserId == id);

            if(favoriteList == null)
            {
                throw new Exception("No Favorites found");
            }

            var favoriteMovieCard = new List<MovieCardResponseModel>();
            foreach (var favorite in favoriteList)
            {
                favoriteMovieCard.Add(new MovieCardResponseModel
                {
                    Id = favorite.Movie.Id,
                    Title = favorite.Movie.Title,
                    PosterUrl = favorite.Movie.PosterUrl
                });
            }



            return favoriteMovieCard;
        }

        public async Task<int> PostFavorite(UserFavoriteRequestModel requestModel)
        {
            var favorite = new Favorite
            {
                MovieId = requestModel.MovieId,
                UserId = requestModel.UserId
            };

            var favoriteId = await _favoriteRepository.Add(favorite);
            return favoriteId.Id;
        }
    }
}
