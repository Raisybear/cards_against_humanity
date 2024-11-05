using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace cards_against_humanity_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // In-Memory-Daten für Testzwecke
        private static List<string> _items = new List<string> { "Item1", "Item2", "Item3" };

        // GET: api/test
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_items); // Gibt alle Items zurück
        }

        // GET: api/test/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id < 0 || id >= _items.Count)
                return NotFound("Item not found"); // Falls das Item nicht existiert

            return Ok(_items[id]); // Gibt das Item zurück
        }

        // POST: api/test
        [HttpPost]
        public IActionResult Create([FromBody] string newItem)
        {
            if (string.IsNullOrEmpty(newItem))
                return BadRequest("Item cannot be empty");

            _items.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = _items.Count - 1 }, newItem); // Erstellt ein neues Item
        }

        // PUT: api/test/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string updatedItem)
        {
            if (id < 0 || id >= _items.Count)
                return NotFound("Item not found");

            if (string.IsNullOrEmpty(updatedItem))
                return BadRequest("Item cannot be empty");

            _items[id] = updatedItem; // Aktualisiert das Item
            return NoContent(); // Erfolgreiche Aktualisierung
        }

        // DELETE: api/test/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0 || id >= _items.Count)
                return NotFound("Item not found");

            _items.RemoveAt(id); // Löscht das Item
            return NoContent(); // Erfolgreiche Löschung
        }
    }

}
