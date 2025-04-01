namespace Repository.Entities
{
    public class PriceDifference
    {
        public Guid Id { set; get; }
        public string Ticker { set; get; }

        public long Time { set; get; }
        public double? Open { set; get; }
        public double? Close { set; get; }
        public double? High { set; get; }
        public double? Low { set; get; }

        public PriceDifference()
        {
            Id = Guid.NewGuid();
        }
    }
}
