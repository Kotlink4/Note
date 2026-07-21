using Microsoft.EntityFrameworkCore;
using Note.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<NoteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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


app.Run();


