using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Context;
using DotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWebAPI.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DBContextClass dBContextClass;
        readonly ILogger<CustomerController> log;


        public CustomerController()
        {
            try
            {
                var loggerFactory = new LoggerFactory();
                log = loggerFactory.CreateLogger<CustomerController>();
                var optionsBuilder = new DbContextOptionsBuilder<DBContextClass>();

               
                dBContextClass = new DBContextClass(optionsBuilder.Options);
                if (dBContextClass.Customers.Count() == 0)
                {
                    // Create a new customer if collection is empty,
                    // which means you can't delete all customer.
                    dBContextClass.Customers.Add(new Customer { CustomerName = "Tushar", Email = "srborchate@gmail.com", Password = "1234", Weight = 70.0 });
                    dBContextClass.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                log.LogError(ex.ToString());
            }
            
        }

        [HttpGet]
        [Route("GetCustomerData")]
        public async Task<ActionResult<List<Customer>>> GetCustomer()
        {
            try
            {
                //getting customers list
                var data = await dBContextClass.Customers.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCustomerbyId/{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomerbyId(int id)
        {
            try
            {
                //getting customer by id
                var data = await dBContextClass.Customers.Where(a=>a.CustomerId==id).FirstOrDefaultAsync();
                if(data==null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("DeleteCustomerbyId/{id:int}")]
            public async Task<ActionResult<Customer>> DeleteCustomerbyId(int id)
        {
            try
            {
                //delete customer by id
                var data = await dBContextClass.Customers.Where(a => a.CustomerId == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return NotFound();
                }
                dBContextClass.Customers.Remove(data);
                return Ok();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("Create")]
        public async  Task<ActionResult> InsertCustomer( Customer customer)
        {
            //insert customer by id
            try
            {

           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dBContextClass.Customers.Add(customer);
            await dBContextClass.SaveChangesAsync();
            return Ok();
            }
            catch (Exception ex)
            {

                log.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }





    }
}