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
    public class MovieCastRepository: EfRepository<MovieCast>, IMovieCastRepository
    {
        public MovieCastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<MovieCast>> GetMovieByCastId(int id)
        {
            var casts = await _dbContext.MovieCasts.Include(m=>m.Movie).Where(mc => mc.CastId == id).ToListAsync();
            return casts;
        }
    }
}
