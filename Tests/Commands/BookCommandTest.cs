using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Books;
using Application.Errors;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using Moq;
using Xunit;

namespace Tests.Commands
{
    public class BookCommandTest
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IGenreRepository> _mockGenreRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public BookCommandTest()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockGenreRepository = new Mock<IGenreRepository>();

            _mockUnitOfWork.SetupGet(uow => uow.Author).Returns(_mockAuthorRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.Genre).Returns(_mockGenreRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.Book).Returns(_mockBookRepository.Object);
        }

        [Fact]
        public async Task Handle_CreateBook_Successfully()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new CreateBookCommand(1, "Example Book", 1, 1);

            _mockUnitOfWork.Setup(uow => uow.Genre.GetGenreByIdAsync(It.IsAny<int>())).ReturnsAsync(new Genre());
            _mockUnitOfWork.Setup(uow => uow.Author.GetAuthorByIdAsync(It.IsAny<int>())).ReturnsAsync(new Author());

            var commandHandler = new CreateBookCommandHandler(_mockUnitOfWork.Object);

            // Act
            var result = await commandHandler.Handle(request, cancellationToken);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Book.CreateBookAsync(It.IsAny<Book>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);

            Assert.True(result.IsSuccessful);
            Assert.Equal(request.BookId, result.Value);
        }

        [Fact]
        public async Task Handle_CreateBook_Failure_GenreNotFound()
        {
            // Arrange
            var request = new CreateBookCommand(1, "Example Book", 1, 1);

            _mockUnitOfWork.Setup(uow => uow.Genre.GetGenreByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre)null);
            var commandHandler = new CreateBookCommandHandler(_mockUnitOfWork.Object);

            // Act
            var result = await commandHandler.Handle(request, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.Book.CreateBookAsync(It.IsAny<Book>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(new CancellationToken()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Never);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ResultStatusCodeEnum.NotFound, result.StatusCode);

            Assert.NotNull(result.Errors);
            Assert.IsType<List<ResultError>>(result.Errors);

            var firstError = result.Errors.FirstOrDefault(); 

            Assert.NotNull(firstError);
            Assert.Equal(BookErrors.NotFoundGenre.Key, firstError.Key);
        }

        [Fact]
        public async Task Handle_CreateBook_Failure_AuthorNotFound()
        {
            // Arrange
            var request = new CreateBookCommand(1, "Example Book", 1, 1);

            _mockUnitOfWork.Setup(uow => uow.Genre.GetGenreByIdAsync(It.IsAny<int>())).ReturnsAsync(new Genre());
            _mockUnitOfWork.Setup(uow => uow.Author.GetAuthorByIdAsync(It.IsAny<int>())).ReturnsAsync((Author)null);


            var commandHandler = new CreateBookCommandHandler(_mockUnitOfWork.Object);

            // Act
            var result = await commandHandler.Handle(request, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.Book.CreateBookAsync(It.IsAny<Book>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Never);


            Assert.False(result.IsSuccessful);
            Assert.Equal(ResultStatusCodeEnum.NotFound, result.StatusCode);

            Assert.NotNull(result.Errors);
            Assert.IsType<List<ResultError>>(result.Errors);

            var firstError = result.Errors.FirstOrDefault(); 

            Assert.NotNull(firstError);
            Assert.Equal(BookErrors.NotFoundAuthor.Key, firstError.Key);
        }
    }
}