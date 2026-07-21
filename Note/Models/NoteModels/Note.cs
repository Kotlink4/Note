namespace Note.Models.NoteModels
{
    // Модель заметки, которая будет использоваться в приложении Note
    public class NoteM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
    }
}
