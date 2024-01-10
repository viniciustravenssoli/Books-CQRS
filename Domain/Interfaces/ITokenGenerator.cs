using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateJwtToken(User user);

        Task<List<Claim>> GetAllValidClaims(User user);
    }
}