using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Dtos.GoogleBooksDtos
{
    public class GoogleBooksResponse
    {
        public List<GoogleBookItem> Items { get; set; }
    }
}
