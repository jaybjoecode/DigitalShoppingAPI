using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCarController : ControllerBase
    {
        private readonly DigitalShoppingDbContext context;

        public ShoppingCarController(DigitalShoppingDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShoppingCarController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShoppingCarController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE api/<ShoppingCarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
