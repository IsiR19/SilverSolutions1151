using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class ManufacturingStage
    {
        public Guid Id { get; set; }
        public ProductionStage ProductionStage { get; set; }
        public Guid ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public Guid IngredientId { get; set; }
        public virtual Ingredients Ingredient { get; set; }

        public decimal Quantity { get; set; }
        private DateTime createdDate;

        [Column("CreatedDate")]
        public DateTime CreatedDate
        {
            get;set;
        }
    }
}

public enum ProductionStage
{
    RawTobacco = 0,
    Mixing = 1, // Will stay for a while depends on season, Expectation for repoting
    Packing = 2,
    Complete = 3,
    Sold = 4
}

public enum ManufacturingType
{
    Fruit = 0,
    Mollases= 1,
}

public enum FruitIngredients{
    Glycerine = 0,
    Syrup =1,
    Flavour =2,
}

public enum MolasesIngredients
{
    Molases = 0,
}

//Fruit
//Flavour calc to be added glycerine 1kg per kg of tobbaco, syurp 1 kg per 1 kg tobacoo, flavour 20% of tobaco

//Molases 
//1kg tobaco to 500g molas