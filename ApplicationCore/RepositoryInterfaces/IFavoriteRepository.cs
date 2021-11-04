using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IFavoriteRepository: IAsyncRepository<Favorite>
    {
        public Task AddFavorite(FavoriteRequestModel favoriteRequest);
        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        public Task<IEnumerable<Favorite>> GetAllFavoritesForUser(int id);


    }
}
