using Moq;
using MyBooks.Application.Dtos;
using MyBooks.Application.Interfaces;
using MyBooks.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Test.Services
{
    public class GoogleBooksUseCaseTest
    {
        private readonly Mock<IGoogleBooksService> _googleBooksServiceMock;
        private readonly GoogleBooksUseCase _useCase;

        public GoogleBooksUseCaseTest()
        {
            _googleBooksServiceMock = new Mock<IGoogleBooksService>();
            _useCase = new GoogleBooksUseCase(_googleBooksServiceMock.Object);

        }

        public List<GoogleBookDto> expected =
            [
                new GoogleBookDto { Title = "Test Book"}
            ];


        [Fact]
        public async Task GetBookByTitleAndAuthor_TitleEmpty_ReturnEmptyList()
        {
            //Act
            var result = await _useCase.GetBookByTitleAndAuthor("", "");

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookByTitleAndAuthor_ValidData_CallService()
        {
            SetupServiceMock("Title", "Author", expected);

            //Act
            var result = await _useCase.GetBookByTitleAndAuthor("Title", "Author");

            //Assert
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal("Test Book", result[0].Title);

            _googleBooksServiceMock.Verify(
                s => s.GetBookByTitleAndAuthor("Title", "Author"),
                Times.Once);
        }

        [Fact]
        public async Task GetBookByTitleAndAuthor_AuthorEmpty_CallService()
        {
            SetupServiceMock("Title", "", expected);

            //Act
            var result = await _useCase.GetBookByTitleAndAuthor("Title", "");

            //Assert
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal("Test Book", result[0].Title);

            _googleBooksServiceMock.Verify(
            s => s.GetBookByTitleAndAuthor("Title", ""),
            Times.Once);
        }


        [Fact]
        public async Task GetBookByTitleAndAuthor_TitleEmpty_CallService()
        {
            SetupServiceMock("", "Author", expected);

            //Act
            var result = await _useCase.GetBookByTitleAndAuthor("", "Author");

            //Assert
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal("Test Book", result[0].Title);

            _googleBooksServiceMock.Verify(
            s => s.GetBookByTitleAndAuthor("", "Author"),
            Times.Once);
        }



        private void SetupServiceMock(string title, string author, List<GoogleBookDto> result)
        {
            _googleBooksServiceMock
                .Setup(s => s.GetBookByTitleAndAuthor(title, author))
                .ReturnsAsync(result);
        }
    }
}
