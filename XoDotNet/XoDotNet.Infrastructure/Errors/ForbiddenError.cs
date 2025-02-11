namespace XoDotNet.Infrastructure.Errors;

public record ForbiddenError(string Message) : Error(Message)
{
}