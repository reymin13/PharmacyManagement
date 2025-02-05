using PharmacyManagement.Application.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Füge die Controller hinzu (sonst funktionieren die API-Routen nicht!)
builder.Services.AddControllers();

// 🔹 Verbindung zur PostgreSQL-Datenbank herstellen
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Füge Swagger für API-Dokumentation hinzu
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🚀 Anwendung bauen (darf nur EINMAL aufgerufen werden!)
var app = builder.Build();

// 🔹 Middleware für Entwicklungsumgebung (Swagger UI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 HTTPS & Autorisierung aktivieren
app.UseHttpsRedirection();
app.UseAuthorization();

// 🔹 Füge Controller-Endpoints hinzu
app.MapControllers();

// 🚀 Starte die Anwendung
app.Run();
