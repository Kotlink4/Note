var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

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


