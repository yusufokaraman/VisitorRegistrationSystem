using VisitorRegistrationSystem.Common.Utility.Results.Types;

namespace VisitorRegistrationSystem.Common.Utility.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
