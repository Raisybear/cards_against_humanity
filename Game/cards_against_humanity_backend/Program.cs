using Microsoft.Extensions.Options;
using cards_against_humanity_backend.Services;
using cards_against_humanity_backend;

var builder = WebApplication.CreateBuilder(args);

// CORS-Richtlinie hinzufügen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp",
        policy => policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Konfiguration für die Datenbankeinstellungen
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<MongoDbService>();

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