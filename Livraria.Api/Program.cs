using Livraria.Application.Interfaces;
using Livraria.Application.Services;
using Livraria.Application.Validators;
using Livraria.Domain.Repositories;
using Livraria.Infra.Context;
using Livraria.Infra.Repositories;
using Livraria.Shared.Data;
using Livraria.Shared.DomainValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();

builder.Services.AddScoped<ILivroValidator, LivroValidator>();
builder.Services.AddScoped<IAutorValidator, AutorValidator>();
builder.Services.AddScoped<IGeneroValidator, GeneroValidator>();

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

builder.Services.AddScoped<IDomainValidation, DomainValidation>();
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

// OpenAPI/Swagger (requer Microsoft.AspNetCore.OpenApi)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
