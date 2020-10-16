using MicroCredentials.Data.Models;
using MicroCredentials.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity.Infrastructure;

namespace MicroCredentials.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [ApiVersion("1.0")]
        [HttpGet("{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _service.GetCustomer(id);

            if (customer == null)
            {
                return NotFound("Didn't find customer with id " + id);
            }
            return Ok(customer);
        }

        [ApiVersion("2.0")]
        [HttpGet("{id}")]
        public IActionResult GetCustomerV2([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _service.GetCustomer(id +1);

            if (customer == null)
            {
                return NotFound("Didn't find customer with id " + id);
            }
            return Ok(customer);
        }

        [Route("api/Customer")]
        [HttpGet("{name}")]
        public IActionResult GetCustomerByName([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _service.GetCustomerByName(name);

            if (customer == null)
            {
                return NotFound("Didn't find customer with name " + name);
            }
            return Ok(customer);
        }

        [Route("/api/Customer")]
        [ApiVersion("1.0")]
        [HttpPost]
        public IActionResult PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (_service.IsCustomerExist(customer.Username))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    _service.AddCustomer(customer);
                }
            }
            catch (Exception)
            {
                return BadRequest("Please check the customer details that you have entered.");
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Id, ApiVersion= 1.0 }, customer);
        }

        [Route("/api/Customer")]
        [ApiVersion("2.0")]
        [HttpPost]
        public IActionResult PostCustomerV2([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (_service.IsCustomerExist(customer.Username))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    _service.AddCustomer(customer);
                }
            }
            catch (Exception)
            {
                return BadRequest("Please check the customer details that you have entered.");
            }
             return CreatedAtAction("GetCustomer", new { id = customer.Id, ApiVersion= 2.0  }, customer);
        }

        [ApiVersion("1.0")]
        [HttpPut]
        public IActionResult PutCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.EditCustomer(customer);
                return Ok(customer);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.IsCustomerExist(customer.Username))
                {
                    return NotFound("Customer does not exist, please check your input");
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [ApiVersion("2.0")]
        [HttpPut]
        public IActionResult PutCustomerV2([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.EditCustomer(customer);
                return Ok(customer);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.IsCustomerExist(customer.Username))
                {
                    return NotFound("Customer does not exist, please check your input");
                }
                else
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [ApiVersion("1.0")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _service.GetCustomer(id);

            if (customer == null)
            {
                return NotFound("Customer doesn't exist");
            }

            _service.DeleteCustomer(id);
            return Ok(customer);
        }

        [ApiVersion("2.0")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerV2([FromRoute] int id)
        {
            var customer = _service.GetCustomer(id);

            if (customer == null)
            {
                return NotFound("Customer doesn't exist");
            }

            _service.DeleteCustomer(id);
            return Ok(customer);
        }
    }
}
