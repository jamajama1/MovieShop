using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IFavoriteService
    {
        public Task AddFavorite(FavoriteRequestModel favoriteRequest);
        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        public Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
    }
}
