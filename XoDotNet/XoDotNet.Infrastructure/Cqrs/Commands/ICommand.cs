using XoDotNet.Infrastructure.Results;
using XoDotNet.Mediator;

namespace XoDotNet.Infrastructure.Cqrs.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}