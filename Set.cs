namespace Goods
{
    [Serializable]
    class Set:Item
    {
        public List<Product> ProductList { get; set; }
        public Set() : base()
        {
            this.ProductList = new List<Product>();
        }
        public Set(string Name,
                   double Price,
                   List<Product> ItemList)
        :base(Name, Price)
        {
            this.ProductList = ItemList;
        }
        public override string Info()
        {
            var Names = from item in ProductList select item.Name;
            return $"{Name}, {Price}, {string.Join(", ", Names)}";
        }
        public override bool Expired()
        {
            foreach(Item item in ProductList)
            {
                if (item.Expired()){return true;}
            }
            return false;
        }
    }
}