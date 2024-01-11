using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Token
{
    public interface ITokenGeneratorDois
    {
        Task<string> GenerateJwtToken(User user);

        Task<List<Claim>> GetAllValidClaims(User user);
    }
}