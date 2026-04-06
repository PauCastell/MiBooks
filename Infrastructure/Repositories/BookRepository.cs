using MyBooks.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MyBooks.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BooksDbContext _context;
    public BookRepository(BooksDbContext context)
    {
        _context = context;
    }

    public async Task<List<LibroRefactor>> GetAllBooks()
    {
        return await _context.LibroRefactor
            .Include(l => l.LibroAutores) //Include trae la coleccion relacionada de LibroAutores
            .ThenInclude(la => la.Autor) //ThenInclude trae la entidad Autor dentro de la coleccion LibroAutores
            .ToListAsync();
    }

    public async Task<Autor?> GetAutorByNameAsync(string name)
    {
        return await _context.Autor
            .FirstOrDefaultAsync(a => a.Nombre == name);
    }

    public async Task<LibroRefactor> AddBookAsync(LibroRefactor book)
    {
        _context.LibroRefactor.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Autor> AddAutorAsync(Autor autor)
    {
        _context.Autor.Add(autor);
        await _context.SaveChangesAsync();
        return autor;
    }

    public async Task<LibroRefactor?> GetBookByTitle(string title)
    {
        return await _context.LibroRefactor
            .Include(l => l.LibroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.FuenteExterna)
            .Include(l => l.Lectura)
            .Include(l => l.LibroInput)
            .FirstOrDefaultAsync(l => l.Titulo == title);
    }
}
