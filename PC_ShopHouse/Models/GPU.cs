namespace PC_ShopHouse.Models
{
    public class GPU
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string Memory { get; set; }        // VD: 8GB 
        public string MemoryType { get; set; }    // VD: GDDR6X
        public string MenmoryBus { get; set; }      // VD: 256-bit
        public string BusInterface { get; set; }      // VD: PCIe 4.0
        public string CoreClock { get; set; }    // VD: 1830 MHz
        public string BoostClock { get; set; }     // VD: 2100 MHz
        public int TDP { get; set; }                    // VD: 200 (Watts)
        public string OutputPorts { get; set; }   // VD: HDMI x1, DP x3
    }
}
