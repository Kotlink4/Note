using Note.Models.NoteModels;
using Note.Servises.NoteServises;
using Note.Validator;

namespace Note.Endpoints
{
    public static class NoteEndpoints
    {
        // Метод расширения для настройки маршрутов API, связанных с заметками
        public static void MapNoteEndpoints(this WebApplication app)
        {
            // Создание группы маршрутов для заметок с тегом "Notes"
            var noteGroup = app.MapGroup("/notes").WithTags("Notes");

            // Определение маршрута для получения всех заметок
            noteGroup.MapGet("/", async (
                INoteServise noteService, 
                CancellationToken cancellationToken) =>
            {
                var notes = await noteService.GetAllNotesAsync(cancellationToken);
                return Results.Ok(notes);
            })
            .WithName("GetAllNotes")
            .WithTags("Notes")
            .Produces<List<NoteM>>(StatusCodes.Status200OK);

            // Определение маршрута для получения заметки по идентификатору
            noteGroup.MapGet("/{id:guid}", async (
                Guid id, 
                INoteServise noteService,
                NoteValidator noteValidator,
                CancellationToken cancellationToken) =>
            {
                var note = await noteService.GetNoteByIdAsync(id, cancellationToken);
                return note is not null ? Results.Ok(note) : Results.NotFound();
            })
            .WithName("GetNoteById")
            .WithTags("Notes")
            .Produces<NoteM>(StatusCodes.Status200OK);

            // Определение маршрута для создания новой заметки
            noteGroup.MapPost("/", async (
                CreateNoteRequest request, 
                INoteServise noteService, 
                NoteValidator noteValidator,
                CancellationToken cancellationToken) =>
            {
                var validationError = noteValidator.Validate(request.Name, request.Description);

                if (validationError is not null)
                {
                    return Results.BadRequest(new { Error = validationError });
                }

                var createdNote = await noteService.CreateNoteAsync(request, cancellationToken);
                return Results.Created($"/notes/{createdNote.Id}", createdNote);
            })
            .WithName("CreateNote")
            .WithTags("Notes")
            .Produces<NoteM>(StatusCodes.Status201Created);

            // Определение маршрута для обновления существующей заметки
            noteGroup.MapPut("/{id:guid}", async (
                Guid id, 
                UpdateNoteRequest request, 
                INoteServise noteService, 
                NoteValidator noteValidator,
                CancellationToken cancellationToken) =>
            {
                var validationError = noteValidator.Validate(request.Name, request.Description);

                if (validationError is not null)
                {
                    return Results.BadRequest(new { Error = validationError });
                }

                var updatedNote = await noteService.UpdateNoteAsync(id, request, cancellationToken);
                return updatedNote is not null ? Results.Ok(updatedNote) : Results.NotFound();
            })
            .WithName("UpdateNote")
            .WithTags("Notes")
            .Produces<NoteM>(StatusCodes.Status200OK);

            // Определение маршрута для удаления заметки по идентификатору
            noteGroup.MapDelete("/{id:guid}", async (Guid id, INoteServise noteService, CancellationToken cancellationToken) =>
            {
                var deleted = await noteService.DeleteNoteAsync(id, cancellationToken);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteNote")
            .WithTags("Notes")
            .Produces(StatusCodes.Status204NoContent);
        }
    }
}
