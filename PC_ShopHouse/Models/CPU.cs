namespace PC_ShopHouse.Models
{
    public class CPU
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string Socket { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public string BaseClock { get; set; }
        public string BoostClock { get; set; }
        public string TDP { get; set; }
        public int Cache { get; set; }
        public bool IntegratedGraphics { get; set; }

    }
}
