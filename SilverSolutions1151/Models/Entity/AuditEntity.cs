namespace SilverSolutions1151.Data.Entity
{
    public abstract class AuditEntity : Entity
    {
        private DateTime createdModifiedDate;
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedModifiedDate
        {
            get
            {
                this.createdModifiedDate = DateTime.Now;
                return this.createdModifiedDate;
            }
            set { this.createdModifiedDate = value; }
        }
    }
}