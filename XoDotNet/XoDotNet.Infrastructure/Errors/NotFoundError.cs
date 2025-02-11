namespace XoDotNet.Infrastructure.Errors;

public record NotFoundError(string Message) : Error(Message)
{
}