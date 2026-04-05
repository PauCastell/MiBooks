using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Dtos;

public class CreateBookDto
{
    public string Title { get; set; }
    public List<string> Authors { get; set; } = [];
    public int? PublishYear { get; set; }
    public int? PageNumber { get; set; }
    public string? Description { get; set; }
    public string? SmallImage { get; set; }
    public string? BigImage { get; set; }
    public string? ExternalApiTitle { get; set; }
    public string FileName { get; set; }
}
