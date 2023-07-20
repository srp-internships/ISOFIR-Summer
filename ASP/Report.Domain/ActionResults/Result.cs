namespace Report.Domain.ActionResults;

public class Result
{
    
}
public class Result<TData>:Result
{
}


public class OkResult:Result
{
    
}
    public class OkResult<TData>:Result<TData>
{
    public TData Result;

    public OkResult(TData result)
    {
        Result = result;
    }

    public override string ToString()
    {
        return Result+"";
    }
}

public class ErrorResult:Result
{
    public ErrorResult(Exception exception, string message="")
    {
        Exception = exception;
        Message = message;
    }
    public Exception Exception;
    public string Message;
}