using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMS.App.Helpers;
using TMS.Infrastructure.Data;

namespace TMS.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
            builder.Services.AddDbContext<AppDbContext>(b => b.UseSqlServer(connectionString, x => x.MigrationsAssembly("TMS.Infrastructure")));
            
            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Seed();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}