using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NoteService _noteService;

        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }
        
        // GET api/notes
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _noteService.GetNotes();
        }

        // GET api/notes/5
        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            var todoItem = _noteService.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return new ObjectResult(todoItem);
        }

        // POST api/notes
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            _noteService.Add(note);

            return CreatedAtRoute("GetNote", new Note { Id = note.Id}, note);
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