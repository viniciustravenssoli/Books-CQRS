using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;

namespace Application.Errors
{
    public class BookErrors
    {
        
        public static ResultError NotFoundAuthor => new ResultError("not_found_author", "This Author was not found");
        public static ResultError NotFoundGenre => new ResultError("not_found_genre", "This Genre was not found");
    }
}