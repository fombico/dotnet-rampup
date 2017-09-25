using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        // GET api/notes
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return new Note[] { new Note() { Id = 1, Body = "Note 1"} };
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/notes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}