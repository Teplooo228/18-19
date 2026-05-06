namespace Goods
{
    class Set : Item
    {
        public List<Item> ItemList { get; set; }
        public Set() : base()
        {
            this.ItemList = new List<Item>();
        }
        public Set(string Name,
                   double Price,
                   List<Item> ItemList)
        : base(Name, Price)
        {
            this.ItemList = ItemList;
        }
        public Set(Set other) : base(other)
        {
            this.ItemList = other.ItemList;
        }
        public override string ToString()
        {
            var Names = from item in ItemList select item.Name;
            return $"{Name}, {Price}, {string.Join(", ", Names)}";
        }
        public override bool Expired()
        {
            foreach (Item item in ItemList)
            {
                if (item.Expired()) { return true; }
            }
            return false;
        }
    }
}