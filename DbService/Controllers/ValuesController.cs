using System;
using System.Collections.Generic;
using System.Linq;
using DbService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DbService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SampleDbContext _context;
        private readonly Random _random = new Random();

        public ValuesController(SampleDbContext dbContext)
        {
            _context = dbContext;

            _context.Database.EnsureCreated();

            if (_context.Sample.Count() == 0) _context.Sample.Add(new Sample {Value = 10000000});

            _context.SaveChanges();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<long> Get()
        {
            var rnd = _random.Next(5);

            if (rnd == 0)
                //randomly do work to generate high CPU usage
                Prime(100000);

            if (rnd == 1)
                //randomly throw exceptions to generate errors
                throw new Exception("Whuuuuuu");

            var val = _context.Sample.First().Value;
            return val;
        }

        private List<long> Prime(long num)
        {
            var retVal = new List<long>();

            for (long i = 0; i <= num; i++)
            {
                var isPrime = true; // Move initialization to here
                for (long j = 2; j < i; j++) // you actually only need to check up to sqrt(i)
                    if (i % j == 0) // you don't need the first condition
                    {
                        isPrime = false;
                        break;
                    }

                if (isPrime) retVal.Add(i);
            }

            return retVal;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}