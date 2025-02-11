namespace XoDotNet.Infrastructure.Pages;

public record PageContent<TContent>(int Page, int MaxPage, TContent Content)
{
}