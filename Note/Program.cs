using Microsoft.EntityFrameworkCore;
using Note.Data;
using Note.Endpoints;
using Note.Servises.NoteServises;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddScoped<INoteServise, NoteServise>();

builder.Services.AddDbContext<NoteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NoteDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Note API";
        options.SwaggerEndpoint("/openapi/v1.json", "Note V1");
    });
}



app.UseHttpsRedirection();

app.MapNoteEndpoints();


app.Run();


