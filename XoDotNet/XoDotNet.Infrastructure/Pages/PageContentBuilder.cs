namespace XoDotNet.Infrastructure.Pages;

public record PageContentBuilder<TContent>(int ElementsCount, int ElementsPerPage, int RequestedPage)
{
    public int CurrentPage => Math.Min(RequestedPage, MaxPage);

    public int MaxPage => ElementsCount / ElementsPerPage + (ElementsCount % ElementsPerPage > 0 ? 1 : 0);

    public int Offset => (CurrentPage - 1) * ElementsPerPage;

    public int Limit => ElementsPerPage;

    public PageContent<TContent> Build(TContent content)
    {
        return new PageContent<TContent>(CurrentPage, MaxPage, content);
    }
}