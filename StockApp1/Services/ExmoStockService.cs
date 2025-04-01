using Repository.Entities;
using StockApp1.Mapers;
using StockApp1.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace StockApp1.Services
{
    public class ExmoStockService : IStockService
    {
        JsonSerializerOptions options;
        HttpClient client;
        IMaper<CandleModel, Candle> _maper;


        public ExmoStockService(IMaper<CandleModel, Candle> maper) 
        {
            options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _maper = maper;
        }


        public async Task<IEnumerable<Candle>> GetCandles(string symbol, string resolution, long from, long to)
        {
            string url = $"https://api.exmo.com/v1.1/candles_history?symbol={symbol}&resolution={resolution}&from={from}&to={to}";

            client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            List<Candle> candels = new List<Candle>();

            var json = await response.Content.ReadAsStringAsync();

            var res = await System.Text.Json.JsonSerializer.
                DeserializeAsync<CandlesArrayModel>(new MemoryStream(Encoding.UTF8.GetBytes(json)), options);

            if (res == null || res.Candles == null || res.Candles.Count() == 0) return null;
     
            foreach (CandleModel item in res.Candles)
            {
                candels.Add(_maper.Map(item, symbol));
            }

            return candels;
        }
    }
}
