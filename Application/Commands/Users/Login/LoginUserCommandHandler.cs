using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using Application.Token;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Result;
using MediatR;

namespace Application.Commands.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorDois _tokenGeneratorDois;


        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenGeneratorDois tokenGeneratorDois)
        {
            _unitOfWork = unitOfWork;
            _tokenGeneratorDois = tokenGeneratorDois;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User existingUser = await _unitOfWork.User.GetUserByEmailAsync(request.Email);

            if (existingUser is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);
            
            var isCorrect = await _unitOfWork.User.CheckPasswordAsync(existingUser, request.Password);

            if (!isCorrect)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);

            var token = await _tokenGeneratorDois.GenerateJwtToken(existingUser);

            return Result<string>.Success(token);
        }
    }
}