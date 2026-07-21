namespace Note.Models.NoteModels
{
    // Модель запроса на создание заметки, которая будет использоваться в приложении Note
    public record CreateNoteRequest (string Name, string Description);

}
