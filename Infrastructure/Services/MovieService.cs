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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsResponseModel> GetAll()
        {
            var movies = await _movieRepository.GetAll();
            if (movies == null) throw new Exception("Movie");

            var movieDetails = new MovieDetailsResponseModel();
            foreach (var movie in movies)
            {
                movieDetails = new MovieDetailsResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget,
                    Overview = movie.Overview,
                    Price = movie.Price,
                    PosterUrl = movie.PosterUrl,
                    Revenue = movie.Revenue,
                    ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                    Rating = movie.Rating,
                    Tagline = movie.Tagline,
                    Title = movie.Title,
                    RunTime = movie.RunTime,
                    BackdropUrl = movie.BackdropUrl,
                    ImdbUrl = movie.ImdbUrl,
                    TmdbUrl = movie.TmdbUrl
                };

                foreach (var genre in movie.Genres)
                    movieDetails.Genres.Add(new GenreModel
                    {
                        Id = genre.Genre.Id,
                        Name = genre.Genre.Name
                    });

                foreach (var cast in movie.Casts)
                    movieDetails.Casts.Add(new CastResponseModel
                    {
                        Id = cast.Cast.Id,
                        Name = cast.Cast.Name,
                        Character = cast.Character,
                        ProfilePath = cast.Cast.ProfilePath
                    });

                foreach (var trailer in movie.Trailers)
                    movieDetails.Trailers.Add(new TrailerResponseModel
                    {
                        Id = trailer.Id,
                        Name = trailer.Name,
                        TrailerUrl = trailer.TrailerUrl,
                        MovieId = trailer.MovieId
                    });
            }

            return movieDetails;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null) throw new Exception("Movie");
            
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };

            foreach (var genre in movie.Genres)
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = genre.Genre.Id,
                    Name = genre.Genre.Name
                });

            foreach (var cast in movie.Casts)
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.Cast.Id,
                    Name = cast.Cast.Name,
                    Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath
                });

            foreach (var trailer in movie.Trailers)
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl,
                    MovieId = trailer.MovieId
                });
            return movieDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenre(int id)
        {
            var movies = await _movieRepository.GetAll();
            //error maybe becuase we ignored rating column?

            var moviesByGenre = new List<Movie>();
            foreach (var movie in movies)
            {
                var movieById = await _movieRepository.GetMovieById(movie.Id);
                foreach (var genre in movieById.Genres)
                {              
                    if (genre.GenreId == id)
                    {
                        moviesByGenre.Add(movieById);
                    }
                }
            }
 
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in moviesByGenre)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RatedMovies()
        {
            var movies = await _movieRepository.GetTop30RatedMovies();
            
            //error maybe becuase we ignored rating column?
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            // calling MovieRepository with DI based on IMovieRepository
            //I/O bound operation
            var movies = await _movieRepository.GetTop30RevenueMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

    }
}