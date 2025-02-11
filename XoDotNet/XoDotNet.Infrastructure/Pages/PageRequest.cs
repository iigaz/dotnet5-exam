namespace XoDotNet.Infrastructure.Pages;

public class PageRequest
{
    private int _page;
    private int _pageSize;

    public PageRequest(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int Page
    {
        get => _page;
        set => _page = Math.Max(value, 1);
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(Math.Max(value, 1), 500);
    }
}