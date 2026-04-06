using MyBooks.Application.Interfaces;
using MyBooks.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyBooks.Application.Services
{
    public class GoogleBooksUseCase : IGoogleBooksUseCase
    {
        private readonly IGoogleBooksService _googleBooksService;
        public GoogleBooksUseCase(IGoogleBooksService googleBookService)
        { 
            _googleBooksService = googleBookService;
        }

        public async Task<List<GoogleBookDto>> GetBookByTitleAndAuthor(string title, string author)
        {
            title = title.Trim();
            author = author.Trim();

            if(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
            {
                return [];
            }

            return await _googleBooksService.GetBookByTitleAndAuthor(title, author);
        }

        //TODO: Crear Test.
        public async Task<List<GoogleBookDto>> GetBookByQuery(string searchText)
        {
            searchText = searchText.Trim();

            if (string.IsNullOrEmpty(searchText)) return [];

            return await _googleBooksService.GetBooksByQuery(searchText);
        }
    }
}
