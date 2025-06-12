namespace Livraria.Domain.Dto
{
    public sealed class LivroFilter
    {
        public Guid? LivroId { get; }
        public Guid? AutorId { get; }
        public bool? Active { get; }
        public string? Search { get; }
        public int Page { get; }
        public int PageSize { get; }

        public LivroFilter(Guid? livroId, Guid? autorId, bool? active, string? search,
                           int page, int pageSize)
        {
            LivroId = livroId == Guid.Empty ? null : livroId;
            AutorId = autorId == Guid.Empty ? null : autorId;
            Active = active;
            Search = string.IsNullOrWhiteSpace(search) ? null : search.Trim();
            Page = page <= 0 ? 1 : page;
            PageSize = pageSize <= 0 ? 10 : pageSize;
        }
    }

}
