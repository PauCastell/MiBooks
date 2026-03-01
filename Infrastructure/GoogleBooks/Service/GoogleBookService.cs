using Microsoft.Extensions.Options;
using MyBooks.Application.Dtos;
using MyBooks.Application.Dtos.GoogleBooksDtos;
using MyBooks.Application.Interfaces;
using MyBooks.Shared.Const;
using MyBooks.Shared.Settings;
using System.Net.Http.Json;

namespace MyBooks.Infrastructure.GoogleBooks.Service
{
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


        internal string CreateQuery(string title, string author)
        {
            var query = $"{ConstValues.GoogleQueryPrefix}{ConstValues.GoogleIntitle}{Uri.EscapeDataString(title)}" +
                $"+{ConstValues.GoogleInAuthor}{Uri.EscapeDataString(author)}" +
                $"{ConstValues.Googlekey}{_apiKey}";
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
    }
}
