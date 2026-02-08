using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Shared.Dtos
{
    public class FileBookDto
    {
        public string FileName { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string Path { get; set; }
    }
}
