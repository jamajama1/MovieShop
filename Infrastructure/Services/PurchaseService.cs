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
        private readonly IMovieRepository _movieRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository)
        { 
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetAllPurchases(int id)
        {
            {
                var purchases = await _purchaseRepository.GetAllPurchases(id);

                if (purchases == null)
                {
                    throw new Exception("No purchase found");
                }

                var purchaseMovieCard = new List<MovieCardResponseModel>();
                foreach (var purchase in purchases)
                {
                    var movie = _movieRepository.GetMovieById(purchase.MovieId);
                    purchaseMovieCard.Add(new MovieCardResponseModel
                    {
                        Id = purchase.Id,
                        Title = movie.Result.Title,
                        PosterUrl = movie.Result.PosterUrl
                    });
                }



                return purchaseMovieCard;
            }
        }
    }
}
