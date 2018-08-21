namespace Assignment3
{
    public class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public Software(string name, string version, int quantity, int price)
        {
            Name = name;
            Version = version;
            Quantity = quantity;
            Price = price;
        }

        public Software()
        {
            
        }
    }
}