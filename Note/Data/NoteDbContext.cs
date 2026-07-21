using Microsoft.EntityFrameworkCore;

namespace Note.Data
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {
        }
        // DbSet для работы с заметками
        public DbSet<Models.NoteModels.NoteM> Notes => Set<Models.NoteModels.NoteM>();
    }
}
