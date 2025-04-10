using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using Repository.Concret;
using Repository.Context;
using Repository.Entities;
using StockApp1.Mapers;
using StockApp1.Models;
using StockApp1.Services;

namespace StockApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICandleRepository, CandleRepository>();
            builder.Services.AddScoped<IPriceDifferenceRepository, PriceDifferenceRepository>();
            builder.Services.AddScoped<IStockService, ExmoStockService>();
            builder.Services.AddScoped<IMaper<CandleModel, Candle>, CandleMaper>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
