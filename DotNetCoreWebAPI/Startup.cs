using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace DotNetCoreWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DBContextClass>(opt =>
            //   opt.UseInMemoryDatabase("CustomerList"));

            //var connection = @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;ConnectRetryCount=0";



            services.AddDbContext<DBContextClass>(options => options.UseSqlServer(Configuration.GetConnectionString("CustomerDB")));
            //            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Fastest);

            //adding gzip compression
            services.AddResponseCompression(options =>
                        {
                            options.Providers.Add<GzipCompressionProvider>();
                            options.EnableForHttps = true;
                        });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationFilter));
            });
            services.AddResponseCompression();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
           

             //app.UseHttpsRedirection();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.UseResponseBuffering();
            app.UseResponseCompression();
            app.UseStaticFiles();


            app.UseMvc();

        }
    }
}
