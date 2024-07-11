﻿using VisitorRegistrationSystem.Common.Utility.Results.Abstract;
using VisitorRegistrationSystem.Common.Utility.Results.Types;

namespace VisitorRegistrationSystem.Common.Utility.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, T data, string message)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;

        }
        public DataResult(ResultStatus resultStatus, T data, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;
            Exception = exception;


        }

        public T Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
