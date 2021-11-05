using ApplicationCore.Entities;
using ApplicationCore.Models;
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
    public class FavoriteRepository: EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }

        public async Task<List<Favorite>> GetUserFavorites(int id)
        {
            var favorites = await _dbContext.Favorites.Include(m => m.Movie).Where(m => m.UserId == id).OrderByDescending(o => o.Id).DefaultIfEmpty().ToListAsync();
            return favorites;
        }
    }
}
