﻿using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Repository.Interfaces
{
    public interface IManufactureRepository
    {
        int GetRawTobaccoBalance(DateTime endDate);
        bool AddRawTobacco(int quantity, DateTime manufactureDate);
        bool RemoveRawTobacco(int quantity,DateTime manufactureDate);
        bool AddMixedTobacco(TobaccoMixture tobaccoMixture,DateTime manufactureDate);
        int GetMixedTobaccoBalance(DateTime endDate);
        bool RemoveMixedTobacco(int quantity, DateTime manufactureDate);
        int GetReadyStockBalance(DateTime endDate);
        bool AddReadyStockTobacco(int quantity, DateTime manufactureDate);
        bool RemoveReadyStock(int quantity, DateTime manufactureDate);
        int GetSoldStockBalanceByDate(DateTime endDate);
        int GetSoldStockBalanceByDate(DateTime startDate, DateTime endDate);
        bool AddSoldStock(int quantity,DateTime manufactureDate);
        bool RemoveSoldStock(int quantity, DateTime manufactureDate);
        List<Manufacture> GetManufactureItemsByDateAndType(Models.Entity.ProductionStage stage,DateTime? startDate,DateTime? endDate);

    }
}
