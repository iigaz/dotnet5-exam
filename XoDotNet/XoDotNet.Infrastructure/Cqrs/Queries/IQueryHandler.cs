using XoDotNet.Infrastructure.Results;
using XoDotNet.Mediator;

namespace XoDotNet.Infrastructure.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}