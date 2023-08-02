namespace StudentCard.Domain;

public class Result
{
    
}

public class OkResult : Result
{
    
}
public class OkResult<T> : Result
{
    public T Result { get; init; }
    public OkResult(T result)
    {
        Result = result;
    }
}

public class ErrorResult : Result
{
    public string Message { get; init; } = "";
    public Exception? Exception { get; set; }
    
    public ErrorResult(string message)
    {
        Message = message;
    }
    
    public ErrorResult(Exception exception)
    {
        Exception = exception;
    }
    
    public ErrorResult(string message, Exception exception)
    {
        Message = message;
        Exception = exception;
    }
}