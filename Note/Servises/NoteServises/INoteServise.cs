using Note.Models.NoteModels;

namespace Note.Servises.NoteServises
{
    // Интерфейс сервиса для работы с заметками, который будет использоваться в приложении Note
    public interface INoteServise
    {
        public Task<List<NoteM>> GetAllNotesAsync(CancellationToken cancellationToken);

        public Task<NoteM?> GetNoteByIdAsync(Guid id, CancellationToken cancellationToken);

        public Task<NoteM> CreateNoteAsync(CreateNoteRequest request, CancellationToken cancellationToken);

        public Task<NoteM> UpdateNoteAsync(Guid id, UpdateNoteRequest request, CancellationToken cancellationToken);

        public Task<bool> DeleteNoteAsync(Guid id, CancellationToken cancellationToken);
    }
}