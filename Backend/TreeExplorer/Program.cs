using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TreeExplorer.Classes;
using TreeExplorer.Controllers;
using TreeExplorer.Data;
using TreeExplorer.Interfaces;
using TreeExplorer.Models;

namespace TreeExplorer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TreeContext>();
                db.Database.EnsureCreated();
                SetData(db);
            }
            
            host.Run();
        }

        private static void SetData(TreeContext db)
        {
            Tree _tree = new();
            _tree.Set(db.Elements.ToListAsync().Result);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
         
    }
}
