using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.User.GetUserByEmailAsync(request.Email);

            if (existingUser != null)
                return Result<string>.Failure(UserErrors.UserAlredyExist, ResultStatusCodeEnum.BadRequest);

            var newUser = new User() { Email = request.Email, UserName = request.Username };

            await _unitOfWork.BeginTransactionAsync();
            var isCreated = await _unitOfWork.User.CreateUserAsync(newUser, request.Password);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            if (!isCreated.Succeeded)
                return Result<string>.Failure(isCreated.Errors.Select(error => new ResultError(error.Code, error.Description)).ToList());

            return Result<string>.Success($"Usuario {newUser.Email}, Registrado com sucesso");
        }
    }
}