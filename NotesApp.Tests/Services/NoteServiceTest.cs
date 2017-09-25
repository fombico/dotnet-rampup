using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NotesApp.Models;
using NotesApp.Services;
using Xunit;

namespace NotesApp.Tests.Services
{
    public class NoteServiceTest
    {
        private readonly DbContextOptions<NoteContext> _dbContextOptions;

        public NoteServiceTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<NoteContext>()
                .UseInMemoryDatabase()
                .Options;
        }
        
        [Fact]
        public void SavesNote()
        {
            Note note;
            using (var context = new NoteContext(_dbContextOptions))
            {
                var service = new NoteService(context);
                note = service.Add(new Note() { Body = "Note"});
            }
            Assert.Equal("Note", note.Body);
        }

        [Fact]
        public void GetsListOfNotes()
        {
            List<Note> notes;
            using (var context = new NoteContext(_dbContextOptions))
            {
                var service = new NoteService(context);
                service.Add(new Note() { Body = "Note A"});
                service.Add(new Note() { Body = "Note B"});

                notes = service.GetNotes();
            }
            
            Assert.Equal(3, notes.Count);
            Assert.Equal("Note 1", notes.ElementAt(0).Body); // assume there's one item already in db
            Assert.Equal("Note A", notes.ElementAt(1).Body);
            Assert.Equal("Note B", notes.ElementAt(2).Body);
        }

        [Fact]
        public void GetsNoteById()
        {
            Note note;
            using (var context = new NoteContext(_dbContextOptions))
            {
                var service = new NoteService(context);
                var item = service.Add(new Note() { Body = "Note X"});

                note = service.GetById(item.Id);
            }
            
            Assert.Equal("Note X", note.Body);
        }
    }
}