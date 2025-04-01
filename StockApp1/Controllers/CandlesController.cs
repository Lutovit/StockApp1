using Microsoft.AspNetCore.Mvc;
using Repository.Abstract;
using Repository.Entities;
using StockApp1.Models;

namespace StockApp1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CandlesController : ControllerBase
    {
        private ILogger<CandlesController> _logger;
        private ICandleRepository _candleRepository;


        public CandlesController(ILogger<CandlesController> logger, ICandleRepository candleRepository)
        {
            _logger = logger;
            _candleRepository = candleRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {          
            return Ok(await _candleRepository.GetAllAsync());
        }

     
        [HttpPost]   
        public async Task<ActionResult> Create(CandleModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ValidationState);
            }

            Candle candle = new Candle
            {
                Time = item.T,
                Open = item.O,
                Close = item.C,
                High = item.H,
                Low = item.L,
                Volume = item.V
            };

            await _candleRepository.CreateAsync(candle);

            return Ok(new { Message = "Candle has been added. " });
        }


    
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(string id)
        {
            var item = await _candleRepository.FindByIdAsync(id);
            if (item == null)
            {
                return new BadRequestObjectResult(new { Message = "There is NO candle with this ID in database." });
            }

            return Ok(item);
        }



        [HttpGet("{time}")]  
        public async Task<ActionResult> FindByTicker(string ticker)
        {
            var items = await _candleRepository.FindByTickerAsync(ticker);
            if (items == null || items.Count() == 0)
            {
                return new BadRequestObjectResult(new { Message = "There is NO Candles with this Ticker in database." });
            }

            return Ok(items);
        }

    
        [HttpPut]  
        public async Task<IActionResult> Update(Candle item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Candle candle = await _candleRepository.FindByIdAsync(item.Id.ToString());
            if (candle == null)
            {
                return new BadRequestObjectResult(new { Message = "There is NO Candle with this ID in database." });
            }

            candle.Time = item.Time;
            candle.Open = item.Open;
            candle.Close = item.Close;
            candle.High = item.High;
            candle.Low = item.Low;
            candle.Volume = item.Volume;

            await _candleRepository.UpdateAsync(candle);

            return Ok(new { Message = "Candle  Id = " + item.Id + " has been updated!" });
        }


        [HttpDelete("{id}")]  
        public async Task<IActionResult> Remove(string id)
        {
            var item = await _candleRepository.FindByIdAsync(id);
            if (item == null)
            {
                return new BadRequestObjectResult(new { Message = "There is NO Candlet with this ID in database." });
            }

            await _candleRepository.RemoveAsync(id);

            return Ok(new { Message = "Candle  Id = " + id + " has been deleted!" });
        }


       
    }
}
