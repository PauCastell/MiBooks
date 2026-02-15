using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Shared.Dtos
{
    public class FileBookDto
    {
        public string FileName { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
