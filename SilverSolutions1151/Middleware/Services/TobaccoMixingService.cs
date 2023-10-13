using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Repository.Interfaces;
using System.Web.Http.ModelBinding;

namespace SilverSolutions1151.Middleware.Services
{
    public class TobaccoMixingService : ITobaccoMixingService
    {
        private ILogger<TobaccoMixingService> _logger;
        private IManufactureRepository _manufactureRepository;
        private IRawTobaccoService _tobaccoService;
        private bool _isValidate;
       
        public TobaccoMixingService(ILogger<TobaccoMixingService> logger
            ,IManufactureRepository manufactureRepository
            , IRawTobaccoService tobaccoService
            , IConfiguration config
            )
        {
            _logger = logger;
            _manufactureRepository = manufactureRepository;
            _tobaccoService = tobaccoService;
            _isValidate = config.GetValue<bool>("ValidateStage:IsValidate");
           
        }
        public bool AddTobaccoMixing(int quantity, DateTime manufactureDate, decimal? glycerineQty, decimal? flavourQty
            , decimal? syrupQty, decimal? preservativeQty)
        {

            var mixedTobacco = new TobaccoMixture
            {
                FlavorQty = flavourQty ?? 0,
                GlycerinQty = glycerineQty ?? 0,
                SyrupQty = syrupQty ?? 0,
                PreservativeQty = preservativeQty ?? 0,
                Tobacco = quantity
            };
            
            mixedTobacco.MixtureTotal = mixedTobacco.FlavorQty + mixedTobacco.GlycerinQty + mixedTobacco.SyrupQty + mixedTobacco.PreservativeQty + mixedTobacco.Tobacco;
            if(_manufactureRepository.AddMixedTobacco(mixedTobacco, manufactureDate))
                return _tobaccoService.RemoveRawTobacco(quantity, manufactureDate);

            return false;
        }

        public int GetMixedTobaccoByDate(DateTime endDate)
        {

            return _manufactureRepository.GetMixedTobaccoBalance(endDate.EndOfDay());
        }

        public bool RemoveMixedTobacco(int quantity, DateTime manufactureDate)
        {
            return _manufactureRepository.RemoveMixedTobacco(quantity,manufactureDate);
        }
    }
}
