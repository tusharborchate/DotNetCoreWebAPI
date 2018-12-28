using DotNetCoreWebAPI.Context;
using DotNetCoreWebAPI.Controllers;
using DotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DotnetCoreAPITEsting
{
    public class UnitTest1
    {
        private readonly ILogger<UnitTest1> logger;

        public UnitTest1()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");
            logger = loggerFactory.CreateLogger<UnitTest1>();

        }


        [Fact]
        public void GetCustomerTestMethod()
        {
            

                CustomerController customer = new CustomerController();
                var ok = customer.GetCustomer();
                var items = Assert.IsType<List<Customer>>(ok.Result.Value);
                Assert.Equal(3, items.Count);
            
          

        }


        [Fact]
        public void InsertCustomerTestMethod()
        {
           

                CustomerController customerController = new CustomerController();
                Customer customer = new Customer();
                customer.CustomerName = "tushar";
                customerController.ModelState.AddModelError("Password", "Required");

                var ok = customerController.InsertCustomer(customer);
                Assert.IsType<BadRequestObjectResult>(ok.Result);
            
           
        }
    }
}
