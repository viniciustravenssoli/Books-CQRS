using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Result
{
    public enum ResultStatusCodeEnum
    {
        Ok = 200,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
    }
}
