using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using MyBooks.Application.Dtos.GoogleBooksDtos;
using MyBooks.Infrastructure.GoogleBooks.Service;
using MyBooks.Shared.Const;
using MyBooks.Shared.Settings;
using System.Buffers.Text;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Infrastructure.Tests.Services
{
    public class GoogleBookServiceTest
    {
        private readonly GoogleBookService _googleBookService;

        public GoogleBookServiceTest()
        {
            _googleBookService = null!;

            var apiKey = "test_api_key";
            var baseUrl = "https://www.googleapis.com/books/v1/volumes";

            _googleBookService = new GoogleBookService(new HttpClient(), Options.Create(new GoogleBooksOptions
            {
                ApiKey = apiKey,
                BaseUrl = baseUrl
            }));
        }

        private readonly GoogleBooksResponse response = new()
        {
            Items =
    [
        new GoogleBookItem
                    {
                        VolumeInfo = new VolumeInfo
                        {
                            Title = "Test Book",
                            Authors = ["Test Author"],
                            PublishedDate = "2020-01-01",
                            Description = "Test Description",
                            ImageLinks = new ImageLinks
                            {
                                Thumbnail = "http://example.com/thumbnail.jpg",
                                SmallThumbnail = "http://example.com/smallThumbnail.jpg"
                            }
                        }
                    }
    ]
        };

        private HttpClient CreateMockHttpClient(string response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(response),
                });
            return new HttpClient(handlerMock.Object);
        }

        [Fact]
        public void CreateQuery_WithTitleAndAuthor_ReturnCorrectUrl()
        {
            // Arrange
            var title = "Test Book";
            var author = "Test Author";
            var apiKey = "test_api_key";
            var baseUrl = "https://www.googleapis.com/books/v1/volumes";

            //Act
            var url = _googleBookService.CreateQuery(title, author);

            //Assert
            Assert.Contains($"{ConstValues.GoogleIntitle}{Uri.EscapeDataString(title)}", url);
            Assert.Contains($"{ConstValues.GoogleInAuthor}{Uri.EscapeDataString(author)}", url);
            Assert.Contains(apiKey, url);
            Assert.StartsWith(baseUrl, url);
        }

        [Fact]
        public void CreateQuery_Onlytitle_ReturnCorrectUrl()
        {
            // Arrange
            var title = "Test Book";
            var apiKey = "test_api_key";
            var baseUrl = "https://www.googleapis.com/books/v1/volumes";

            //Act
            var url = _googleBookService.CreateQuery(title, "");

            //Assert
            Assert.DoesNotContain(ConstValues.GoogleInAuthor, url);
            Assert.Contains($"{ConstValues.GoogleIntitle}{Uri.EscapeDataString(title)}", url);
            Assert.Contains(apiKey, url);
            Assert.StartsWith(baseUrl, url);
        }


        [Fact]
        public void CreateQuery_OnlyAuthor_ReturnCorrectUrl()
        {
            // Arrange
            var author = "Test Author";
            var apiKey = "test_api_key";
            var baseUrl = "https://www.googleapis.com/books/v1/volumes";

            //Act
            var url = _googleBookService.CreateQuery("", author);

            //Assert
            Assert.DoesNotContain(ConstValues.GoogleIntitle, url);
            Assert.Contains($"{ConstValues.GoogleInAuthor}{Uri.EscapeDataString(author)}", url);
            Assert.Contains(apiKey, url);
            Assert.StartsWith(baseUrl, url);
        }


        [Fact]
        public void ExtractYear_ValidDate_ReturnYear()
        {
            //Arrange
            var publishedDate = "2020-01-01";
            //Act
            var year = _googleBookService.ExtractYear(publishedDate);
            //Assert
            Assert.Equal(2020, year);
        }


        [Fact]
        public void ExtractYear_InvalidDate_ReturnNull()
        {
            //Arrange
            var publishedDate = "200000";

            //Act
            var year = _googleBookService.ExtractYear(publishedDate);

            //Assert
            Assert.Null(year);
        }


        [Fact]
        public void ExtractYear_NullDate_ReturnNull()
        {
            //Arrange
            string? publishedDate = null;

            //Act
            var year = _googleBookService.ExtractYear(publishedDate);

            //Assert
            Assert.Null(year);
        }

        [Fact]
        public void MapGoogleResponseToGoogleDto_ItemNull_ReturnEmptyList()
        {
            //Arrange
            var googleBooksResponse = new GoogleBooksResponse
            {
                Items = null
            };
            //Act
            var result = _googleBookService.MapGoogleResponseToGoogleDto(googleBooksResponse);
            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void MapGoogleResponseToGoogleDto_VolumeInfoNull_ReturnEmptyList()
        {
            //Arrange
            var response = new GoogleBooksResponse
            {
                Items =
                [
                    new GoogleBookItem
                    {
                        VolumeInfo = null
                    }
                ]
            };
            //Act
            var result = _googleBookService.MapGoogleResponseToGoogleDto(response);
            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void MapGoogleResponseToGoogleDto_ValidData_ReturnMappedDto()
        {
            //Act
            var googleDto = _googleBookService.MapGoogleResponseToGoogleDto(response);

            //Assert
            Assert.NotNull(googleDto);
            Assert.Equal("Test Book", googleDto[0].Title);
            Assert.Equal("Test Author", googleDto[0].Authors[0]);
            Assert.Equal(2020, googleDto[0].PublishedYear);
            Assert.Equal("Test Description", googleDto[0].Description);
            Assert.Equal("http://example.com/thumbnail.jpg", googleDto[0].BigImage);
            Assert.Equal("http://example.com/smallThumbnail.jpg", googleDto[0].SmallImage);
        }


        [Fact]
        public async Task GetBookByTitleAndAuthor_ValidJson_ReturnMappedList()
        {
            //Arrange
            var httpClient = CreateMockHttpClient(JsonSerializer.Serialize(response));

            var service = new GoogleBookService(httpClient, Options.Create(new GoogleBooksOptions
            {
                ApiKey = "test_api_key",
                BaseUrl = "http://mockurl"
            }));

            //Act
            var result = await service.GetBookByTitleAndAuthor("Title", "Author");

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Book", result[0].Title);
            Assert.Equal("Test Author", result[0].Authors[0]);
        }


        [Fact]
        public async Task GetBookByTitleAndAuthor_InvalidJson_Return()
        {
            //Arrange
            var httpClient = CreateMockHttpClient("", HttpStatusCode.InternalServerError);

            var service = new GoogleBookService(httpClient, Options.Create(new GoogleBooksOptions
            {
                ApiKey = "test_api_key",
                BaseUrl = "http://mockurl"
            }));

            //Act
            var result = await service.GetBookByTitleAndAuthor("Title", "Author");

            //Assert
            Assert.Empty(result);
        }



        [Fact]
        public async Task GetBookByTitleAndAuthor_NullJson_ReturnMappedList()
        {
            //Arrange
            var httpClient = CreateMockHttpClient("null");

            var service = new GoogleBookService(httpClient, Options.Create(new GoogleBooksOptions
            {
                ApiKey = "test_api_key",
                BaseUrl = "http://mockurl"
            }));

            //Act
            var result = await service.GetBookByTitleAndAuthor("Title", "Author");

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

    }
}
