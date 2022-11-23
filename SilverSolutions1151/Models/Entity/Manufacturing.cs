namespace SilverSolutions1151.Data.Entity
{
    public class Manufacturing : AuditEntity
    {
        public ProductionStage Stage { get; set; }

        public ProductionStage NextStage { get; set; }

        //Autogeneration
        //Non nullable
        public string? BatchNumber { get; set; }

        public decimal? Quantity { get; set; }

        public List<RawMaterial>? Materials { get; set; }
    }
}