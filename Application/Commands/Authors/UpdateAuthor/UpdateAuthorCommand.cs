using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Result;
using MediatR;

namespace Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Result<string>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string NewName { get; set; }
    }
}