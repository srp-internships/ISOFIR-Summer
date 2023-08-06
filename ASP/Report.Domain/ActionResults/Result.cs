namespace Report.Domain.ActionResults;

public class Result
{
}



public class OkResult : Result
{
}

public class OkResult<TData> : Result
{
    public TData Result;

    public OkResult(TData result)
    {
        Result = result;
    }

    public override string ToString()
    {
        return Result + "";
    }
}

public class ErrorResult : Result
{
    public Exception Exception;
    public string Message;

    public ErrorResult(Exception exception, string message = "")
    {
        Exception = exception;
        Message = message;
    }

    public ErrorResult(string message)
    {
        Message = message;
    }
}