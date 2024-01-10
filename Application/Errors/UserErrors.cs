using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Result;

namespace Application.Errors
{
    public class UserErrors
    {
         public static ResultError NotFoundUser => new ResultError("not_found_user", "This User was not found");
        public static ResultError NotFound => new ResultError("not_found_genre", "This Genre was not found");
    }
}