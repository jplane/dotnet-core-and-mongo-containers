using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace app.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IMongoCollection<Order> _orders = null;

        public OrdersController(IConfiguration config)
        {
            var mongoCS = config["mongo-cs"];
            var mongo = new MongoClient(mongoCS);
            var db = mongo.GetDatabase("default");
            _orders = db.GetCollection<Order>("orders");
        }

        // GET api/values
        [HttpGet]
        public Task<IEnumerable<Order>> Get()
        {
            return _orders.AsQueryable().ToListAsync().ContinueWith(t => t.Result.AsEnumerable());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Order> Get(Guid id)
        {
            var cursor = await _orders.FindAsync(o => o.Id == id);

            return await cursor.SingleOrDefaultAsync();
        }

        // POST api/values
        [HttpPost]
        public Task Post([FromBody]Order value)
        {
            return _orders.InsertOneAsync(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Task Put(Guid id, [FromBody]Order value)
        {
            return _orders.FindOneAndReplaceAsync(o => o.Id == id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Task Delete(Guid id)
        {
            return _orders.FindOneAndDeleteAsync(o => o.Id == id);
        }
    }
}
