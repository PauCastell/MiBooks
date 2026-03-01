using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Dtos.GoogleBooksDtos
{
    public class VolumeInfo
    {
        public string Title { get; set; }
        public List<string>? Authors { get; set; }
        public string? PublishedDate { get; set; }
        public int? PageCount { get; set; }
        public string? Description { get; set; }
        public ImageLinks? ImageLinks { get; set; }
    }
}
