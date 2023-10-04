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
    var h = heroesList.Contains(heroName);
    if (h == true)
    {
        return Results.Ok($"{heroName} found!");
    }
    else
    {
        return Results.NotFound($"{heroName} not found!");
    }
   
});


app.Run();


