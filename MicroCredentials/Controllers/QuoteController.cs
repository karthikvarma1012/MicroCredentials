using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using MicroCredentials.Data.Models;
using MicroCredentials.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroCredentials.Controllers
{
    [Produces("application/json")]
    [Route("api/quote")]
    [ApiController]
    public class QuoteController : Controller
    {
        private readonly IQuoteService _service;

        public QuoteController(IQuoteService service)
        {
            _service = service;
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public IActionResult Get()
        {
            var quotes = _service.GetAllQuotes();
            return Ok(quotes);
        }

        [ApiVersion("1.0")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quote = _service.GetQuote(id);
            return Ok(quote);
        }

        [ApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetV2()
        {
            var quotes = _service.GetAllQuotes();
            return Ok(quotes);
        }

        [ApiVersion("2.0")]
        [HttpGet("{id}")]
        public IActionResult GetV2(int id)
        {
            var quote = _service.GetQuote(id);
            return Ok(quote);
        }


        [ApiVersion("1.0")]
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            try
            {
                if (_service.IsQuoteExist(quote.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    _service.AddQuote(quote);
                }
            }
            catch (Exception)
            {
                return BadRequest("Please check the quote details that you have entered.");
            }
            return Ok(quote);
        }

        [ApiVersion("2.0")]
        [HttpPost]
        public IActionResult PostV2([FromBody] Quote quote)
        {
            try
            {
                if (_service.IsQuoteExist(quote.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    _service.AddQuote(quote);
                }
            }
            catch (Exception)
            {
                return BadRequest("Please check the quote details that you have entered.");
            }
            return Ok(quote);
        }

        [ApiVersion("1.0")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
                try
                {
                    _service.EditQuote(quote);
                    return Ok(quote);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
        }

        [ApiVersion("2.0")]
        [HttpPut("{id}")]
        public IActionResult PutV2(int id, [FromBody] Quote quote)
        {
                try
                {
                    _service.EditQuote(quote);
                    return Ok(quote);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
        }

        [ApiVersion("1.0")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = _service.GetQuote(id);

            if (quote == null)
            {
                return NotFound("Customer doesn't exist");
            }

            _service.DeleteQuote(id);
            return Ok(quote);
        }

        [ApiVersion("2.0")]
        [HttpDelete("{id}")]
        public IActionResult DeleteV2(int id)
        {
            var quote = _service.GetQuote(id);

            if (quote == null)
            {
                return NotFound("Customer doesn't exist");
            }

            _service.DeleteQuote(id);
            return Ok(quote);
        }
    }
}
