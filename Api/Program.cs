using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MyBooks.Application.Interfaces;
using MyBooks.Infrastructure.GoogleBooks.Service;
using MyBooks.Infrastructure.Services;
using MyBooks.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registrar el DbContext con SQL Server
builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Inyecta los valores de configuración para GoogleBooksOptions.
builder.Services.Configure<GoogleBooksOptions>(builder.Configuration.GetSection("GoogleBooks"));

builder.Services.AddHttpClient<IGoogleBooksService, GoogleBookService>();

// Registrar BookRepository
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Registrar BookService
builder.Services.AddScoped<IBookService, BookService>();

//Registrar FileService
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
