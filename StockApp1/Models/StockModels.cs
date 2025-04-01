namespace StockApp1.Models
{
    public class CandleModel
    {
        public long T { set; get; }
        public double O { set; get; }
        public double C { set; get; }
        public double H { set; get; }
        public double L { set; get; }
        public double V { set; get; }
    }

    public class CandlesArrayModel
    {
        public IEnumerable<CandleModel> Candles { set; get; }
    }
}
