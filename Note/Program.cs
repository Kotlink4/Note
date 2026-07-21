using Microsoft.EntityFrameworkCore;

using Note.Data;
using Note.Endpoints;
using Note.Servises.NoteServises;
using Note.Validator;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Регистрация NoteValidator в контейнере зависимостей с областью действия Singleton
builder.Services.AddSingleton<NoteValidator>();

// Регистрация сервиса NoteServise в контейнере зависимостей с областью действия Scoped
builder.Services.AddScoped<INoteServise, NoteServise>();

// Регистрация контекста базы данных NoteDbContext в контейнере зависимостей с использованием SQLite
builder.Services.AddDbContext<NoteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройка CORS для разрешения запросов с фронтенда
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
        .WithOrigins("http://localhost:5173") // Разрешить только этот источник
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Регистрация контроллеров в контейнере зависимостей
var app = builder.Build();

// Применение миграций базы данных при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
    dbContext.Database.Migrate();
}

// Настройка Swagger и OpenAPI только в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Note API";
        options.SwaggerEndpoint("/openapi/v1.json", "Note V1");
    });
}

// Настройка маршрутизации и HTTPS перенаправления
app.UseHttpsRedirection();

app.UseCors("Frontend");

// Настройка маршрутов API для работы с заметками
app.MapNoteEndpoints();

// Запуск приложения
app.Run();


