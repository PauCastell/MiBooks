using Api.Controllers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBooks.Application.Dtos;
using MyBooks.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Tests.ControllersTest
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task SearchGoogleBooks_NoTitleAndAuthor_ReturnBadRequest()
        {
            //Arrange
            var mockBooksService = new Mock<IBookService>();
            var mockGoogleBooksService = new Mock<IGoogleBooksService>();
            var controller = new BooksController(mockBooksService.Object, mockGoogleBooksService.Object);

            //Act
            var result = await controller.SearchGoogleBooks("","");

            //Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Se debe proporcionar al menos un título o autor", badRequest.Value);
        }


        [Fact]
        public async Task SearchGoogleBooks_ValidRequest_ReturnOkWithBooks()
        {
            //Arrange
            var books = new List<GoogleBookDto>
            {
                new() { Title = "Clean Code" },
            };

            var mockBooksService = new Mock<IBookService>();
            var mockGoogleBooksService = new Mock<IGoogleBooksService>();
            mockGoogleBooksService
                .Setup(s => s.GetBookByTitleAndAuthor("Clean Code", "Robert Martin"))
                .ReturnsAsync(books);

            var controller = new BooksController(mockBooksService.Object, mockGoogleBooksService.Object);

            //Act
            var result = await controller.SearchGoogleBooks("Clean Code", "Robert Martin");

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedoBooks = Assert.IsType<List<GoogleBookDto>>(okResult.Value);

            Assert.Single(returnedoBooks);
        }


        [Fact]
        public async Task SearchGoogleBooks_NoBooksFound_ReturnOkWithEmptyList()
        {
            //Arrange

            var mockBooksService = new Mock<IBookService>();
            var mockGoogleBooksService = new Mock<IGoogleBooksService>();
            mockGoogleBooksService
                .Setup(s => s.GetBookByTitleAndAuthor("Unknown", ""))
                .ReturnsAsync(new List<GoogleBookDto>());

            var controller = new BooksController(mockBooksService.Object, mockGoogleBooksService.Object);

            //Act
            var result = await controller.SearchGoogleBooks("Unknown", "");

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedoBooks = Assert.IsType<List<GoogleBookDto>>(okResult.Value);

            Assert.Empty(returnedoBooks);
        }
    }
}
