using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Comments.PostComment;
using Application.Errors;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using Moq;
using Xunit;

namespace Tests.Commands
{
    public class CommentCommandTest
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IGenreRepository> _mockGenreRepository;
        private Mock<ICommentRepository> _mockCommentRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public CommentCommandTest()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockGenreRepository = new Mock<IGenreRepository>();

            _mockUnitOfWork.SetupGet(uow => uow.Author).Returns(_mockAuthorRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.Genre).Returns(_mockGenreRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.Comment).Returns(_mockCommentRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.Book).Returns(_mockBookRepository.Object);
        }

        [Fact]
        public async Task Handle_CreateComment_Successfully_Returns_Datetime_Created()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var date = DateTime.UtcNow;
            var request = new CreateCommentCommand(1, "Comment Example", date, 1, "211521");

            _mockUnitOfWork.Setup(uow => uow.Book.GetBookById(It.IsAny<int>())).ReturnsAsync(new Book());
            _mockUnitOfWork.Setup(uow => uow.User.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());

            var commandHandler = new CreateCommentCommandHandler(_mockUnitOfWork.Object);

            // Act
            var result = await commandHandler.Handle(request, cancellationToken);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Comment.CreateCommentAsync(It.IsAny<Comment>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);

            Assert.True(result.IsSuccessful);
            Assert.Equal(request.CommentDate.ToString(), result.Value);
        }

        [Fact]
        public async Task Handle_CreateComment_Failure_NotFoundBook()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var date = DateTime.UtcNow;
            var request = new CreateCommentCommand(1, "Comment Example", date, 1, "211521");

            _mockUnitOfWork.Setup(uow => uow.Book.GetBookById(It.IsAny<int>())).ReturnsAsync((Book)null);
            _mockUnitOfWork.Setup(uow => uow.User.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());

            var commandHandler = new CreateCommentCommandHandler(_mockUnitOfWork.Object);

            // Act
            var result = await commandHandler.Handle(request, cancellationToken);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.BeginTransactionAsync(), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.Comment.CreateCommentAsync(It.IsAny<Comment>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Never);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ResultStatusCodeEnum.NotFound, result.StatusCode);

            Assert.NotNull(result.Errors);
            Assert.IsType<List<ResultError>>(result.Errors);

            var firstError = result.Errors.FirstOrDefault();

            Assert.NotNull(firstError);
            Assert.Equal(BookErrors.NotFoundBook.Key, firstError.Key);
            Assert.Equal(BookErrors.NotFoundBook.Description, firstError.Description);
        }
    }
}