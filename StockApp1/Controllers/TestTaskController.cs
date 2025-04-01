using Microsoft.AspNetCore.Mvc;
using Repository.Abstract;
using Repository.Entities;
using StockApp1.Services;
using System.Collections.Generic;

namespace StockApp1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestTaskController : ControllerBase
    {
        private ILogger<TestTaskController> _logger;
        private ICandleRepository _candleRepository;
        private IPriceDifferenceRepository _priceDifferenceRepository;
        private IStockService _stockService;
        private string[] tickers = ["BTC_USDT", "BTC_USDC"];


        public TestTaskController(ILogger<TestTaskController> logger, ICandleRepository candleRepository, IStockService stockService,
            IPriceDifferenceRepository priceDifferenceRepository)
        {
            _logger = logger;
            _candleRepository = candleRepository;
            _stockService = stockService;
            _priceDifferenceRepository = priceDifferenceRepository;
        }


        [HttpGet]
        public async Task<IActionResult> InitFirstCollection()
        {
            IEnumerable<Candle> candles = await _stockService.GetCandles(tickers[0], "D", 1735731434, 1743248336);

            await _candleRepository.CreateRangeAsync(candles);

            return Ok(candles);
        }


        [HttpGet]
        public async Task<IActionResult> InitSecondCollection()
        {
            IEnumerable<Candle> candles = await _stockService.GetCandles(tickers[1], "D", 1735731434, 1743248336);

            await _candleRepository.CreateRangeAsync(candles);

            return Ok(candles);
        }


        [HttpGet]
        public async Task<IActionResult> DoTheTask() 
        {
            IEnumerable<Candle> BTC_USDTs = await _candleRepository.FindByTickerAsync(tickers[0]);
            IEnumerable<Candle> BTC_USDCs = await _candleRepository.FindByTickerAsync(tickers[1]);

            List<Candle> list1 = BTC_USDTs.OrderBy(x => x.Time).ToList();
            List<Candle> list2 = BTC_USDCs.OrderBy(x => x.Time).ToList();

            //Удостоверяюсь , что отсортированно верно и что обе коллекции были получены со значениями цены для одних и тех же моментов времени.            
            int i = 0;
            int j = 0;

            for (int k = 0; k < list1.Count; k++) 
            {
                if (list2[k].Time != list1[k].Time) i++;
                if (list2[k].Time == list1[k].Time) j++;
            }

            List<PriceDifference> differences = new List<PriceDifference>();

            for (int k = 0; k < list1.Count; k++)
            {
                Candle? a = null;
                Candle? b = null;

                if (HasFullValue(list1[k]))
                {
                    a = list1[k];
                }
                else
                {
                    a = GetLastWithFullValue(k, list1);              
                }
                

                if (HasFullValue(list2[k]))
                {
                    b = list2[k];
                }
                else
                {
                    b = GetLastWithFullValue(k, list2);           
                }

                if (a == null && b == null) continue;

                differences.Add(new PriceDifference
                {
                    Ticker = b.Ticker + " - " + a.Ticker,
                    Time = a.Time,

                    Open = b.Open - a.Open,
                    Close = b.Close - a.Close,
                    High = b.High - a.High,
                    Low = b.Low - a.Low
                });
            }


            if (differences != null && differences.Count > 0) 
            {
                await _priceDifferenceRepository.CreateRangeAsync(differences);
            }

            return Ok(differences);
        }


        private bool HasFullValue(Candle item) 
        {
            if (item == null) return false;
            if (item.Open == null || item.Close == null || item.High == null || item.Low == null) return false;

            return true;
        }


        private Candle? GetLastWithFullValue( int k, List<Candle> list) 
        {
            if (k < 1) return null;

            for (int i = k - 1; i >= 0; i--) 
            {
                if (HasFullValue(list[i])) return list[i];
            }

            return null;
        }
    }
}
