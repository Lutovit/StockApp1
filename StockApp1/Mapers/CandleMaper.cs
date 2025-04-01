using Repository.Entities;
using StockApp1.Models;

namespace StockApp1.Mapers
{
    public class CandleMaper : IMaper<CandleModel, Candle>
    {
        public Candle Map(CandleModel t, string symbol)
        {
            if (t == null) return null;

            return new Candle
            {
                Ticker = symbol,
                Time = t.T,
                Open = t.O,
                Close = t.C,
                High = t.H,
                Low = t.L,
                Volume = t.V
            };
        }
    }
}
