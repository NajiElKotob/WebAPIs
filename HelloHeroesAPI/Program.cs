using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Declare the in-memory storage variable here
List<string> heroesList = new();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello Heroes!");

app.MapGet("/getheroes", () => $"Heroes: {string.Join(", ", heroesList)}!");

app.MapPost("/addhero",  (string heroName) =>
{
    heroesList.Add(heroName);
    return Results.Ok($"'{heroName}' added to the list of heroes!");

});

app.MapGet("/searchhero", (string heroName) =>
{
    var h = heroesList.Contains(heroName, StringComparer.OrdinalIgnoreCase);
    if (h == true)
    {
        return Results.Ok($"{heroName} found!");
    }
    else
    {
        return Results.NotFound($"{heroName} not found!");
    }
   
});


app.MapDelete("/delhero", (string heroName) =>
{
    if (heroesList.Contains(heroName, StringComparer.OrdinalIgnoreCase))
    {
        heroesList.Remove(heroName);
        return Results.Ok($"'{heroName}' removed from the list of heroes!");
    }
    else
    {
        return Results.NotFound($"{heroName} not found in the list of heroes.");
    }
});


app.MapPut("/updatehero", (string originalName, string newName) =>
{
    if (heroesList.Contains(originalName, StringComparer.OrdinalIgnoreCase))
    {
        // Removing the original name and adding the new name simulates an update.
        heroesList.Remove(originalName);
        heroesList.Add(newName);
        return Results.Ok($"'{originalName}' updated to '{newName}' in the list of heroes.");
    }
    else
    {
        return Results.NotFound($"{originalName} not found in the list of heroes.");
    }
});

app.Run();


