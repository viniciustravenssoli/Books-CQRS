using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Authors.CreateAuthor;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.Commands
{
    public class AuthorCommandTest
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;

        public AuthorCommandTest()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.SetupGet(uow => uow.Author).Returns(_mockAuthorRepository.Object);
        }

        [Fact]
        public async Task CreateAuthor_Executed_ReturnSuccess()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            int authorId = 1;
            string authorName = "john";

            var createAuthorCommand = new CreateAuthorCommand(authorId, authorName);

            var command = new CreateAuthorCommandHandler(_mockUnitOfWork.Object);
            var result = await command.Handle(createAuthorCommand, cancellationToken);

            _mockAuthorRepository.Verify(or => or.CreateAuthorAsync(It.IsAny<Author>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "Alice")]
        [InlineData(2, "Bob")]
        [InlineData(3, "Charlie")]
        public async Task Handle_CreateAuthor_Executed_ReturnSuccessResult(int authorId, string authorName)
        {
            // Arrange
            var createAuthorCommand = new CreateAuthorCommand(authorId, authorName);
            var cancellationToken = new CancellationToken();

            _mockAuthorRepository.Setup(ar => ar.CreateAuthorAsync(It.IsAny<Author>()));
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            var commandHandler = new CreateAuthorCommandHandler(_mockUnitOfWork.Object);

            // Act
            var result = await commandHandler.Handle(createAuthorCommand, new CancellationToken());

            // Assert
            _mockAuthorRepository.Verify(ar => ar.CreateAuthorAsync(It.IsAny<Author>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);

            Assert.True(result.IsSuccessful);
            Assert.Equal(authorId, result.Value);
        }
    }
}