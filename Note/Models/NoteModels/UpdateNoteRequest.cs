namespace Note.Models.NoteModels
{
    // Модель запроса на обновление заметки, которая будет использоваться в приложении Note
    public record UpdateNoteRequest (string Name, string Description);
}
