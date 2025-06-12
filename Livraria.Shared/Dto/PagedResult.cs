namespace Livraria.Shared.Dto
{
    public sealed class PagedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PagedResult(IEnumerable<T> items, int total, int page, int pageSize)
        {
            Items = items.ToList().AsReadOnly();
            TotalItems = total;
            Page = page;
            PageSize = pageSize;
        }
    }
}
