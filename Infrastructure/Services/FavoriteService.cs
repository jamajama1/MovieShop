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
        private readonly IMovieRepository _movieRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository, IMovieRepository movieRepository)
        {
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var newFavorite = await _favoriteRepository.Add(new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            });
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.GetAll();
            if (favorites == null) throw new Exception("No favorite movies found");
            var favoriteMovie = new FavoriteResponseModel();
            var favoriteCard = new MovieCardResponseModel();
            favoriteMovie.UserId = id;
            foreach (var favorite in favorites)
            {
                favoriteCard.Id = favorite.Id;
                var movie = _movieRepository.GetMovieById(favorite.MovieId);
                favoriteCard.PosterUrl = movie.Result.PosterUrl;
                favoriteCard.Title = movie.Result.Title;
                favoriteMovie.FavoriteMovies.Add(favoriteCard);
            }

            return favoriteMovie;
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

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }
    }
}
