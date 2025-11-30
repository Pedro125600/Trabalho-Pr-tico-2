using Microsoft.EntityFrameworkCore;
using TrabalhoPratico;
using TrabalhoPratico.Models;

var builder = WebApplication.CreateBuilder(args);

// Controllers + suporte ao JSON Patch
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // ← ESSENCIAL

// DB
builder.Services.AddDbContext<LocadoraBD>(options =>
    options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=LocadoraDB;Trusted_Connection=True;TrustServerCertificate=True;"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
        b.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Arquivos estáticos
app.UseStaticFiles();

// CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Não usar fallback aqui enquanto testar as páginas
// app.MapFallbackToFile("home.html");

app.Run();
