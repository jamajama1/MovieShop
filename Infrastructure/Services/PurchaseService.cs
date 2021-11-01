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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetUserPurchases(int id)
        {
            {
                var purchases = await _purchaseRepository.GetUserPurchases(id);

                if (purchases == null)
                {
                    throw new Exception("No purchase found");
                }

                var purchaseMovieCard = new List<MovieCardResponseModel>();
                foreach (var purchase in purchases)
                {
                    purchaseMovieCard.Add(new MovieCardResponseModel
                    {
                        Id = purchase.Id,
                        /*Title = purchase.Movie.Title,
                        PosterUrl = purchase.Movie.PosterUrl*/
                    });
                }



                return purchaseMovieCard;
            }
        }

        public async Task<int> PurchaseMovie(UserPurchaseRequestModel requestModel)
        {
            var purchase = new Purchase
            {
                MovieId = requestModel.MovieId,
                UserId = requestModel.UserId,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = requestModel.Total,          
            };

            var purchaseId = await _purchaseRepository.Add(purchase);
            return purchaseId.Id;
        }
    }
}
