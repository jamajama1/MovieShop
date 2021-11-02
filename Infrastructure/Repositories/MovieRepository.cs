using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int id)
        {
            var movies = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            var movieGenre = new Movie();
            var movieGenres = new List<Movie>();
            foreach (var movie in movies.Genres)
            {
                if (movie.GenreId == id)
                {
                    movieGenre = await GetMovieById(movie.MovieId);
                    movieGenres.Add(movieGenre);
                }
            }

            return movieGenres;
        }

        public async Task<IEnumerable<Movie>> GetTop30RatedMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Rating).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            // we are gonna use EF with LINQ to get top 30 movies by revenue
            // SQL select top 30 * from Movies order by Revenue
            // I/o bound operation
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
    }
}