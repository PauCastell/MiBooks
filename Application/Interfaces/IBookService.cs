using Application.Dtos;
using MyBooks.Application.Dtos;
using MyBooks.Domain.Common;
using MyBooks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Interfaces;

public interface IBookService
{
    Task<Result<List<BookDto>>> GetAllBooksAsync();
    Task<Result<BookCreatedDto>> AddBookAsync(CreateBookDto book);
}
