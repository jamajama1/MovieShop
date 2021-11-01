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
        Task<List<MovieCardResponseModel>> GetUserFavorites(int id);
        Task<int> PostFavorite(UserFavoriteRequestModel requestModel);
    }
}
