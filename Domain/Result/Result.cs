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
        Errors = new List<ResultError> { error };
        StatusCode = statusCode;
    }

    private Result(List<ResultError> errors, ResultStatusCodeEnum statusCode)
    {
        IsSuccessful = false;
        Errors = errors;
        StatusCode = statusCode;
    }

    public bool IsSuccessful { get; private set; }
    public T Value { get; private set; }
    public List<ResultError> Errors { get; private set; }
    public ResultStatusCodeEnum StatusCode { get; private set; }
    public string Message { get; set; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Success(T value, string message) => new(value) { Message = message };
    public static Result<T> Failure(ResultError error, ResultStatusCodeEnum statusCode = ResultStatusCodeEnum.BadRequest) => new(error, statusCode);
    public static Result<T> Failure(List<ResultError> errors, ResultStatusCodeEnum statusCode = ResultStatusCodeEnum.BadRequest) => new(errors, statusCode);

    public object GetFinalObject() => IsSuccessful ? Value : Errors;
}
}
