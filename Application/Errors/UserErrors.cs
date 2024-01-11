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
        public static ResultError UserAlredyExist => new ResultError("email_is_used", "This User already exists");
    }
}