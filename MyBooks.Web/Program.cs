using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MyBooks.Application.Interfaces;
using MyBooks.Application.Services;
using MyBooks.Infrastructure.GoogleBooks.Service;
using MyBooks.Infrastructure.Services;
using MyBooks.Shared.Settings;
using MyBooks.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar el DbContext con SQL Server
builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Inyecta los valores de configuración para GoogleBooksOptions.
//TODO: La clase GoogleBooksOptions debería tener validaciones para asegurar que los valores de configuración sean correctos?
builder.Services.Configure<GoogleBooksOptions>(builder.Configuration.GetSection("GoogleBooks"));

builder.Services.AddHttpClient<IGoogleBooksService, GoogleBookService>();
builder.Services.AddScoped<IGoogleBooksUseCase, GoogleBooksUseCase>();

// Registrar BookRepository
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Registrar BookService
builder.Services.AddScoped<IBookService, BookService>();

//Registrar FileService
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
