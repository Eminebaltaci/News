using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }

        public Result(bool isSuccess,string message) : this(isSuccess)
        {
            this.Message = message;
        }

        public Result(bool isSuccess)
        {
            Success = isSuccess;
        }

        public Result()
        {
            
        }
    }
}
