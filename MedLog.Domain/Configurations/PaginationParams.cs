public class PaginationParams
{
    private readonly int _minSize = 1;
    private int _pageSize = 1;
    private int _pageIndex = 1;
    private const int MaxPageSize = 10; // Static constant for MaxPageSize

    public PaginationParams()
    {
        // Default values for PageSize and PageIndex
        PageSize = 1;
        PageIndex = 1;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value < _minSize ? _minSize : value;
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 0 ? _pageIndex : value;
    }
}
