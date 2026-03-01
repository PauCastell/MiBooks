using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Dtos
{
    public class GoogleBookDto
    {
        public string Title { get; set; } = null!;
        public List<string> Authors { get; set; } = [];
        public int? PublishedYear { get; set; }
        public int? PageCount { get; set; }
        public string? Description { get; set; }
        public string? SmallImage { get; set; }
        public string? BigImage { get; set; }
    }
}
