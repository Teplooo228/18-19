using Microsoft.VisualBasic;

namespace Goods
{
    class Product:Item
    {
        public int ShelfLife { get; set; }
        public DateOnly ProductionDate { get; set; }
        public Product():base(){}
        public Product(string Name,
                       double Price,
                       DateOnly ProductionDate,
                       int ShelfLife)
        :base(Name, Price)
        {
            this.ProductionDate = ProductionDate;
            this.ShelfLife = ShelfLife;

        }
        public override string ToString()
        {
            return $"{Name}, {Price}, {ShelfLife}, {ProductionDate}";
        }

        public override bool Expired()
        {
            return DateOnly.FromDateTime(DateTime.Now) > ProductionDate.AddDays(ShelfLife);
        }
    }
}