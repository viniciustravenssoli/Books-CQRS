using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Result
{
    public class Result<T>
    {
        private Result(T value)
        {
            IsSuccessful = true;
            Value = value;
            StatusCode = ResultStatusCodeEnum.Ok;
        }

        private Result(ResultError error, ResultStatusCodeEnum statusCode)
        {
            IsSuccessful = false;
            Error = error;
        }

        public bool IsSuccessful { get; private set; }
        public T Value { get; private set; }
        public ResultError Error { get; private set; }
        public ResultStatusCodeEnum StatusCode { get; private set; }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(ResultError error, ResultStatusCodeEnum statusCode = ResultStatusCodeEnum.BadRequest) => new(error, statusCode);
        public object GetFinalObject() => IsSuccessful ? Value : Error;
    }
}
