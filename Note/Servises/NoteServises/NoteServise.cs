using Microsoft.EntityFrameworkCore;

using Note.Data;
using Note.Models.NoteModels;

namespace Note.Servises.NoteServises
{
    // Сервис для работы с заметками, который будет использоваться в приложении Note
    public class NoteServise : INoteServise
    {
        // Поля для работы с базой данных и логированием
        private readonly NoteDbContext _context;
        private readonly ILogger<NoteServise> _logger;

        // Конструктор сервиса, который принимает контекст базы данных и логгер
        public NoteServise(
            NoteDbContext context,
            ILogger<NoteServise> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Метод для получения всех заметок из базы данных
        public async Task<List<NoteM>> GetAllNotesAsync(
            CancellationToken cancellationToken)
        {
            var notes = await _context.Notes
                .AsNoTracking()
                .OrderBy(n => n.Id)
                .ToListAsync(cancellationToken);

            return notes;
        }

        public async Task<NoteM?> GetNoteByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
            return note;
        }

        public async Task<NoteM> CreateNoteAsync(
            CreateNoteRequest request,
            CancellationToken cancellationToken)
        {
            var note = new NoteM
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                date = DateOnly.FromDateTime(DateTime.Now),
                time = TimeOnly.FromDateTime(DateTime.Now)
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync(cancellationToken);
            return note;
        }

        public async Task<NoteM?> UpdateNoteAsync(
            Guid id,
            UpdateNoteRequest request,
            CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
            if (note is null)
            {
                return null;
            }
            note.Name = request.Name;
            note.Description = request.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return note;
        }

        public async Task<bool> DeleteNoteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
            if (note is null)
            {
                return false;
            }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
