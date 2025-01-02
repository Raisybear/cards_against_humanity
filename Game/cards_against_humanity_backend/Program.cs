using Microsoft.Extensions.Options;
using cards_against_humanity_backend.Services;
using cards_against_humanity_backend;
using CardsAgainstHumanity.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS-Richtlinie hinzufügen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp", policy =>
        policy.AllowAnyOrigin() // Alle Origins erlauben (Entwicklung)
              .AllowAnyMethod() // Alle HTTP-Methoden erlauben (GET, POST, etc.)
              .AllowAnyHeader() // Alle Header erlauben
    );
});


// Konfiguration für die Datenbankeinstellungen
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<CardService>();


// Weitere Dienste und Middleware hinzufügen
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS-Middleware aktivieren
app.UseCors("AllowFrontendApp");

// Middleware-Konfiguration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();