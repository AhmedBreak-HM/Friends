using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZwajApp.API.Models;

namespace zwaj.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET api/values
        [HttpGet]
        public ActionResult GetValues()
        {
            var values = _context.values.ToList();
            return Ok(values);
        }

        [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Value> GetValue(int id)
        {
            var value = _context.values.FirstOrDefault(x => x.id == id);
            return Ok(value);
        }
        
        [AllowAnonymous]
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create(string value)
        {
            var Value = new Value(){
                Name = value

            };
            await _context.values.AddAsync(Value);
            await _context.SaveChangesAsync();
            return StatusCode(201);
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
