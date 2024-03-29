using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Authors.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<Result<int>>
    {
        public CreateAuthorCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}