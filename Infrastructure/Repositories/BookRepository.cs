using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
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
                .Include(l => l.LibroAutores) //Include trae trae la coleccion relacionada de LibroAutores
                .ThenInclude(la => la.Autor) //ThenInclude trae la entidad Autor dentro de la coleccion LibroAutores
                .ToListAsync();
        }
    }
}
