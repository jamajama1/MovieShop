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
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Genre>> GetGenres()
        {
            var genres = await _dbContext.Genres.Select(g=> new Genre (g.Id, g.Name)).DefaultIfEmpty().ToListAsync();
            
            return genres;
        }
    }
}
