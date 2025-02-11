using XoDotNet.Infrastructure.Results;
using XoDotNet.Mediator;

namespace XoDotNet.Infrastructure.Cqrs.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}