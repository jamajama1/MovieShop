using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        // method thats going to get 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<Movie>> GetTop30RatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int id);
        Task<Movie> GetMovieById(int id);
        Task<List<Movie>> GetAllMovies();
    }
}