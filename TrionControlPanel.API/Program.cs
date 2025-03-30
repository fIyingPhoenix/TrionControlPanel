
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json;
using TrionControlPanel.API.Classes;
using TrionControlPanel.API.Classes.Database;

namespace TrionControlPanel.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add memory cache service
            builder.Services.AddMemoryCache();
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<DatabaseManager>();
            builder.Services.AddSingleton<AccessManager>();
            builder.Services.AddSingleton<SqlQueryManager>();
            builder.Services.AddSingleton<Network>();
            var app = builder.Build();

            // Proxy setup
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsProduction() || app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
