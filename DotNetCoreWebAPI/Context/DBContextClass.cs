using DotNetCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace DotNetCoreWebAPI.Context
{
    public class DBContextClass:DbContext
    {

        
        public DBContextClass(DbContextOptions<DBContextClass> options)
           : base()
        {
           
        }
        //setting dbpath for unit testing
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            //read json file from test debug folder
            var build = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = build.Build();
            builder.UseSqlServer(configuration.GetConnectionString("CustomerDB"));
        }                                                                                                 
        public DbSet<Customer> Customers { get; set; }

    }
}
