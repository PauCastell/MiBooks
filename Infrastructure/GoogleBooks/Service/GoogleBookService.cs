using Microsoft.Extensions.Options;
using MyBooks.Application.Dtos;
using MyBooks.Application.Dtos.GoogleBooksDtos;
using MyBooks.Application.Interfaces;
using MyBooks.Shared.Const;
using MyBooks.Shared.Dtos;
using MyBooks.Shared.Settings;
using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace MyBooks.Infrastructure.GoogleBooks.Service;

public class GoogleBookService : IGoogleBooksService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public GoogleBookService(HttpClient httpClient, IOptions<GoogleBooksOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
        _baseUrl = options.Value.BaseUrl;
    }

    public async Task<List<GoogleBookDto>> GetBookByTitleAndAuthor(string title, string author)
    {
        try
        {
            var url = CreateQuery(title, author);

            var googleResponse = await _httpClient.GetFromJsonAsync<GoogleBooksResponse>(url);

            if (googleResponse == null)
            {
                return [];
            }

            return MapGoogleResponseToGoogleDto(googleResponse);
        }
        catch(Exception ex)
        {
            //TODO: Crear middleware para manejar excepciones globalmente y loggear errores de manera centralizada.
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"Error fetching book data: {ex.Message}");
            return [];
        }
    }


    //TODO: Modificar test, he hecho cambios en el método
    internal string CreateQuery(string title, string author)
    {
        var query = "";

        if(!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
        {
            var queryParts = new List<string>();
            queryParts.Add($"{ConstValues.GoogleIntitle}{Uri.EscapeDataString(title)}");
            queryParts.Add($"{ConstValues.GoogleInAuthor}{Uri.EscapeDataString(author)}");
            query = $"{ConstValues.GoogleQueryPrefix}{string.Join("+", queryParts)}";
        }
        else
        {
            var freeText = $"{title}{author}";
            query = $"{ConstValues.GoogleQueryPrefix}{Uri.EscapeDataString(freeText)}";
        }

        query = query
            + $"{ConstValues.Googlekey}{_apiKey}"
            + $"{ConstValues.GoogleMaxResults}";

        var url = $"{_baseUrl}{query}";

        return url;
    }


    internal List<GoogleBookDto> MapGoogleResponseToGoogleDto(GoogleBooksResponse response)
    {
        if(response.Items == null)
        {
            return [];
        }

        return [.. response.Items.Where(item => item.VolumeInfo is not null)
            .Select(item =>
            {
                var volumeInfo = item.VolumeInfo!;

                return new GoogleBookDto
                {
                    Title = volumeInfo.Title,
                    Authors = volumeInfo.Authors ?? [],
                    PublishedYear = ExtractYear(volumeInfo.PublishedDate),
                    PageCount = volumeInfo.PageCount,
                    Description = volumeInfo.Description,
                    SmallImage = volumeInfo.ImageLinks?.SmallThumbnail,
                    BigImage = volumeInfo.ImageLinks?.Thumbnail
                };
            })];
    }


    internal int? ExtractYear(string? publishedDate)
    {
        if (string.IsNullOrEmpty(publishedDate) || publishedDate.Length < 4 )
        {
            return null;
        }
        var yearPart = publishedDate.Split('-')[0];
        if (yearPart.Length == 4 && int.TryParse(yearPart, out int year))
        {
            return year;
        }
        return null;
    }

    //TODO: Control Errores.
    //TODO: Test
    public async Task<List<GoogleBookDto>> GetBooksByQuery(string searchText)
    {
        try
        {
            var query = CreateQuery(searchText, "");
            var googleResponse = await _httpClient.GetFromJsonAsync<GoogleBooksResponse>(query);

            if (googleResponse == null) return [];

            return MapGoogleResponseToGoogleDto(googleResponse);
        }catch (Exception ex)
        {
            Console.WriteLine($"Error fetching book data: {ex.Message}");
            return [];
        }
    }
}
