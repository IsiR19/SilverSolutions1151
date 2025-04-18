﻿using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Middleware.Services
{
    public class ReadyStockService : IReadyStockService
    {
        private readonly ILogger<ReadyStockService> _logger;
        private readonly IManufactureRepository _manufactureRepository;
        private readonly ITobaccoMixingService _tabaccoMixingService;

        public ReadyStockService(ILogger<ReadyStockService> logger, IManufactureRepository manufactureRepository, ITobaccoMixingService tobaccoMixingService )
        {
            _logger = logger;
            _manufactureRepository = manufactureRepository;
            _tabaccoMixingService = tobaccoMixingService;
        }
        public bool AddReadyStock(int numberOfBoxes, DateTime manufactureDate, int grams)
        {
            //Molases KG = number of boxes ---Change
            //Convert g to kg
            var packagingSize = ((decimal)(grams) / 1000);
            var boxes = numberOfBoxes / packagingSize;
            // grams = box size -- grams per box
            if (_manufactureRepository.AddReadyStockTobacco((int)boxes, manufactureDate))
                return _tabaccoMixingService.RemoveMixedTobacco(numberOfBoxes, manufactureDate);

            return false;
        }

        public int GetReadyStockByDate(DateTime endDate)
        {
            return _manufactureRepository.GetReadyStockBalance(endDate);
        }

        public bool RemoveReadyStock(int quantity, DateTime manufactureDate)
        {
            return _manufactureRepository.RemoveReadyStock(quantity, manufactureDate);
        }
    }
}
