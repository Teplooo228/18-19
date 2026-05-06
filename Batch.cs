namespace Goods
{
    [Serializable]
    class Batch:Item
    {
        public int Quantity { get; set; }
        public DateOnly ProductionDate { get; set; }
        public int ShelfLife { get; set; }
        public Batch():base(){}
        public Batch(string Name,
                     double Price,
                     int Quantity,
                     DateOnly ProductionDate,
                     int ShelfLife)
        :base(Name, Price)
        {
           this.Quantity = Quantity;
           this.ProductionDate = ProductionDate;
           this.ShelfLife = ShelfLife;
        }
        public override string Info()
        {
            return $"{Name}, {Price}, {Quantity}, {ShelfLife}, {ProductionDate}";
        }
        public override bool Expired()
        {
            return DateOnly.FromDateTime(DateTime.Now) > ProductionDate.AddDays(ShelfLife);
        }
    }
}