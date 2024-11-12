using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using cards_against_humanity_backend.Services;
using cards_against_humanity_backend.Models;

namespace cards_against_humanity_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public TestController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // GET: api/test
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mongoDbService.GetAllAsync();
            return Ok(items);
        }

        // GET: api/test/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) // Ändere den Typ von int zu string
        {
            var item = await _mongoDbService.GetAsync(id);
            if (item == null)
                return NotFound("Item not found");

            return Ok(item);
        }

        // POST: api/test
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
                return BadRequest("Item cannot be empty");

            var newItem = new Item { Name = request.Name }; // Id bleibt null und wird von MongoDB generiert
            await _mongoDbService.CreateAsync(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }
        
        // PUT: api/test/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateItemRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
                return BadRequest("Name cannot be empty");

            var existingItem = await _mongoDbService.GetAsync(id);
            if (existingItem == null)
                return NotFound("Item not found");

            // Aktualisiere nur das Feld "Name"
            existingItem.Name = request.Name;
            await _mongoDbService.UpdateAsync(id, existingItem);

            return NoContent(); // Erfolgreiche Bearbeitung ohne Rückgabe
        }

        
        // DELETE: api/test/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) // Ändere den Typ von int zu string
        {
            var item = await _mongoDbService.GetAsync(id);
            if (item == null)
                return NotFound("Item not found");

            await _mongoDbService.DeleteAsync(id);
            return NoContent();
        }
    }
}
