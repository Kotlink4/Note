namespace Note.Validator
{
    // Класс для валидации данных заметки
    public class NoteValidator
    {
        public string? Validate(string name, string description)
        {
           if (string.IsNullOrWhiteSpace(name))
            {
                return "Name is required.";
            }
            if (name.Length > 100)
            {
                return "Name cannot exceed 100 characters.";
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return "Description is required.";
            }
            if (description.Length > 500)
            {
                return "Description cannot exceed 500 characters.";
            }
            return null; // No validation errors
        }
    }
}
