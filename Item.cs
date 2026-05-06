using System.Text.Json.Serialization;

namespace Goods
{
    [JsonDerivedType(typeof(Product), "product")]
    [JsonDerivedType(typeof(Batch), "batch")]
    [JsonDerivedType(typeof(Set), "set")]

    abstract public class Item:IComparable<Item>
    {
        public string Name { get; set; }
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = Math.Round(value, 2);
            }
        }
        public Item()
        {
            Name = "Name";
        }
        public Item(string Name,
                    double Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
        public int CompareTo(Item item)
        {
            return this.price.CompareTo(item.price);
        }
        abstract public bool Expired();
    }
}