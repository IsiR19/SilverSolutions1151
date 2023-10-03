using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity

{
    public abstract class Entity
    {
        private DateTime createdDate;

        [Key]
        public virtual Guid Id { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate
        {
            get
            {
                this.createdDate = DateTime.Now;
                return this.createdDate;
            }
            set { this.createdDate = value; }
        }

        public bool IsDeleted { get; set; }
    }
}