using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;

namespace Application.Errors
{
    public class AuthorErrors
    {
        public static ResultError NotFoundAuthor => new ResultError("not_found_author", "This Author was not found");
    }
}