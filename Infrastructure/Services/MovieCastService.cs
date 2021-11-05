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
    public class MovieCastService : IMovieCastService
    {
        private readonly IMovieCastRepository _MovieCastRepository;

        public MovieCastService(IMovieCastRepository movieCastRepository)
        {
            _MovieCastRepository = movieCastRepository;
        }
        
        public async Task<List<MovieCardResponseModel>> GetMovieByCastId(int id)
        {
            /*var moviesbycast = await _MovieCastRepository.GetMovieByCastId(id);
            return moviesbycast;*/
            throw null;
        }
    }
}
