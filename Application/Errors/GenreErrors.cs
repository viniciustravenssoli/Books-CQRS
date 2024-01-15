using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;

namespace Application.Errors
{
    public class GenreErrors
    {
        public static ResultError NotFoundGenre => new ResultError("not_found_genre", "This Genre was not found");
    }
}