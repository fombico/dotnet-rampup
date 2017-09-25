using Microsoft.EntityFrameworkCore;

namespace NotesApp.Models
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options)
            : base(options)
        {
        }

        public DbSet<Note> NotesSet { get; set; }
    }
}